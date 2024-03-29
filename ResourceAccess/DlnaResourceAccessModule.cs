﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using HttpServer;
using HttpServer.Exceptions;
using HttpServer.HttpModules;
using HttpServer.Sessions;
using MediaPortal.Common;
using MediaPortal.Common.Logging;
using MediaPortal.Common.MediaManagement.DefaultItemAspects;
using MediaPortal.Common.ResourceAccess;
using MediaPortal.Common.Threading;
using MediaPortal.Extensions.MediaServer.DLNA;
using MediaPortal.Extensions.MediaServer.Objects.MediaLibrary;

namespace MediaPortal.Extensions.MediaServer.ResourceAccess
{
    public class DlnaResourceAccessModule : HttpModule, IDisposable
    {
        protected readonly IDictionary<string, CachedResource> _resourceAccessorCache = new Dictionary<string, CachedResource>(10);
        protected IntervalWork _tidyUpCacheWork;
        protected readonly object _syncObj = new object();

        public static TimeSpan RESOURCE_CACHE_TIME = TimeSpan.FromMinutes(5);
        public static TimeSpan CACHE_CLEANUP_INTERVAL = TimeSpan.FromMinutes(1);

        public DlnaResourceAccessModule()
        {
        }

        protected class CachedResource : IDisposable
        {
            private IResourceAccessor _resourceAccessor;

            public CachedResource(IResourceAccessor resourceAccessor)
            {
                LastTimeUsed = DateTime.Now;
                _resourceAccessor = resourceAccessor;
            }

            public void Dispose()
            {
                _resourceAccessor.Dispose();
                _resourceAccessor = null;
            }

            public DateTime LastTimeUsed { get; private set; }

            public IResourceAccessor ResourceAccessor
            {
                get
                {
                    LastTimeUsed = DateTime.Now;
                    return _resourceAccessor;
                }
            }
        }

        protected class Range
        {
            protected long _from;
            protected long _to;

            public Range(long from, long to)
            {
                _from = from;
                _to = to;
            }

            public long From
            {
                get { return _from; }
            }

            public long To
            {
                get { return _to; }
            }

            public long Length
            {
                get { return _to - _from + 1; }
            }
        }

        protected void TidyUpResourceAccessorCache()
        {
            lock (_syncObj)
            {
                DateTime threshold = DateTime.Now - RESOURCE_CACHE_TIME;
                ICollection<string> removedResources = new List<string>(_resourceAccessorCache.Count);
                foreach (KeyValuePair<string, CachedResource> entry in _resourceAccessorCache)
                {
                    CachedResource resource = entry.Value;
                    if (resource.LastTimeUsed > threshold)
                        continue;
                    try
                    {
                        resource.Dispose();
                    }
                    catch (Exception e)
                    {
                        ServiceRegistration.Get<ILogger>().Warn("ResourceAccessModule: Error disposing resource accessor '{0}'", e, entry.Key);
                    }
                    removedResources.Add(entry.Key);
                }
                foreach (string resourcePathStr in removedResources)
                    _resourceAccessorCache.Remove(resourcePathStr);
            }
        }

        protected void ClearResourceAccessorCache()
        {
            lock (_syncObj)
            {
                foreach (KeyValuePair<string, CachedResource> entry in _resourceAccessorCache)
                {
                    CachedResource resource = entry.Value;
                    try
                    {
                        resource.Dispose();
                    }
                    catch (Exception e)
                    {
                        ServiceRegistration.Get<ILogger>().Warn("ResourceAccessModule: Error disposing resource accessor '{0}'", e, entry.Key);
                    }
                }
                _resourceAccessorCache.Clear();
            }
        }

        protected IResourceAccessor GetResourceAccessor(ResourcePath resourcePath)
        {
            lock (_syncObj)
            {
                CachedResource resource;
                string resourcePathStr = resourcePath.Serialize();
                if (_resourceAccessorCache.TryGetValue(resourcePathStr, out resource))
                    return resource.ResourceAccessor;
                // TODO: Security check. Only deliver resources which are located inside local shares.
                ServiceRegistration.Get<ILogger>().Debug("ResourceAccessModule: Access of resource '{0}'", resourcePathStr);
                IResourceAccessor result;
                if(!resourcePath.TryCreateLocalResourceAccessor(out result))
                    ServiceRegistration.Get<ILogger>().Debug("ResourceAccessModule: Unable to access resource path '{0}'", resourcePathStr);
                _resourceAccessorCache[resourcePathStr] = new CachedResource(result);
                return result;
            }
        }

        protected IList<Range> ParseRanges(string byteRangesSpecifier, long size)
        {
            if (string.IsNullOrEmpty(byteRangesSpecifier) || size == 0)
                return null;
            IList<Range> result = new List<Range>();
            try
            {
                string[] tokens = byteRangesSpecifier.Split(new char[] { '=' });
                if (tokens.Length == 2 && tokens[0].Trim() == "bytes")
                    foreach (string rangeSpec in tokens[1].Split(new char[] { ',' }))
                    {
                        tokens = rangeSpec.Split(new char[] { '-' });
                        if (tokens.Length != 2)
                            return new Range[] { };
                        if (!string.IsNullOrEmpty(tokens[0]))
                            if (!string.IsNullOrEmpty(tokens[1]))
                                result.Add(new Range(long.Parse(tokens[0]), long.Parse(tokens[1])));
                            else
                                result.Add(new Range(long.Parse(tokens[0]), size - 1));
                        else
                            result.Add(new Range(Math.Max(0, size - long.Parse(tokens[1])), size - 1));
                    }
            }
            catch (Exception e)
            {
                ServiceRegistration.Get<ILogger>().Debug("ResourceAccessModule: Received illegal Range header", e);
                // As specified in RFC2616, section 14.35.1, ignore invalid range header
            }
            return result;
        }

        public override bool Process(IHttpRequest request, IHttpResponse response, IHttpSession session)
        {
            var uri = request.Uri;

            // Check the request path to see if it's for us.
            if (!uri.AbsolutePath.StartsWith(DlnaResourceAccessUtils.RESOURCE_ACCESS_PATH))
                return false;          

            // Grab the media item given in the request.
            Guid mediaItemGuid;
            if (!DlnaResourceAccessUtils.ParseMediaItem(uri, out mediaItemGuid))
                throw new BadRequestException(string.Format("Illegal request syntax. Correct syntax is '{0}'", DlnaResourceAccessUtils.SYNTAX));

            try
            {
                // Attempt to grab the media item from the database.
                var item = MediaLibraryHelper.GetMediaItem(mediaItemGuid);
                if (item == null) 
                    throw new BadRequestException(string.Format("Media item '{0}' not found.", mediaItemGuid));

                // Grab the mimetype from the media item and set the Content Type header.
                var mimeType = MediaLibraryHelper.GetOrGuessMimeType(item);
                if (mimeType == null) 
                    throw new InternalServerException("Media item has bad mime type, re-import media item");
                response.ContentType = mimeType;

                // Grab the resource path for the media item.
                var resourcePathStr =
                    item.Aspects[ProviderResourceAspect.ASPECT_ID].GetAttributeValue(
                        ProviderResourceAspect.ATTR_RESOURCE_ACCESSOR_PATH);
                var resourcePath = ResourcePath.Deserialize(resourcePathStr.ToString());

                var ra = GetResourceAccessor(resourcePath);
                using (var resourceStream = ra.OpenRead())
                {
                    // HTTP/1.1 RFC2616 section 14.25 'If-Modified-Since'
                    if (!string.IsNullOrEmpty(request.Headers["If-Modified-Since"]))
                    {
                        DateTime lastRequest = DateTime.Parse(request.Headers["If-Modified-Since"]);
                        if (lastRequest.CompareTo(ra.LastChanged) <= 0)
                            response.Status = HttpStatusCode.NotModified;
                    }

                    // HTTP/1.1 RFC2616 section 14.29 'Last-Modified'
                    response.AddHeader("Last-Modified", ra.LastChanged.ToUniversalTime().ToString("r"));

                    // DLNA Requirement: [7.4.26.1-6]
                    // Since the DLNA spec allows contentFeatures.dlna.org with any request, we'll put it in.
                    if (!string.IsNullOrEmpty(request.Headers["getcontentFeatures.dlna.org"]))
                    {
                        if (request.Headers["getcontentFeatures.dlna.org"] != "1")
                        {
                            // DLNA Requirement [7.4.26.5]
                            throw new BadRequestException("Illegal value for getcontentFeatures.dlna.org");
                        }
                    }
                    var dlnaString = DlnaProtocolInfoFactory.GetProfileInfo(item).ToString();
                    response.AddHeader("contentFeatures.dlna.org", dlnaString);
  
                    // DLNA Requirement: [7.4.55-57]
                    // TODO: Bad implementation of requirement
                    if (!string.IsNullOrEmpty(request.Headers["transferMode.dlna.org"]))
                    {
                        if (request.Headers["transferMode.dlna.org"] == "Streaming")
                        {
                            response.AddHeader("transferMode.dlna.org", "Streaming");
                        }
                        if (request.Headers["transferMode.dlna.org"] == "Interactive")
                        {
                            response.AddHeader("transferMode.dlna.org", "Interactive");
                        }
                        if (request.Headers["transferMode.dlna.org"] == "Background")
                        {
                            response.AddHeader("transferMode.dlna.org", "Background");
                        }
                    }
  
                    string byteRangesSpecifier = request.Headers["Range"];
                    IList<Range> ranges = ParseRanges(byteRangesSpecifier, resourceStream.Length);
                    bool onlyHeaders = request.Method == Method.Header || response.Status == HttpStatusCode.NotModified;
                    if (ranges != null && ranges.Count == 1)
                        // We only support one range
                        SendRange(response, resourceStream, ranges[0], onlyHeaders);
                    else
                        SendWholeFile(response, resourceStream, onlyHeaders);
                }
            }
            catch (FileNotFoundException ex)
            {
                throw new InternalServerException(string.Format("Failed to proccess media item '{0}'", mediaItemGuid), ex);
            }

            return true;
        }

        protected void SendRange(IHttpResponse response, Stream resourceStream, Range range, bool onlyHeaders)
        {
            if (range.From > resourceStream.Length)
            {
                response.Status = HttpStatusCode.RequestedRangeNotSatisfiable;
                response.SendHeaders();
                return;
            }
            response.Status = HttpStatusCode.PartialContent;
            response.ContentLength = range.Length;
            response.AddHeader("Content-Range", string.Format("bytes {0}-{1}/{2}", range.From, range.To, resourceStream.Length));
            response.SendHeaders();

            if (onlyHeaders)
                return;

            resourceStream.Seek(range.From, SeekOrigin.Begin);
            Send(response, resourceStream, range.Length);
        }

        protected void SendWholeFile(IHttpResponse response, Stream resourceStream, bool onlyHeaders)
        {
            response.Status = HttpStatusCode.OK;
            response.ContentLength = resourceStream.Length;
            response.SendHeaders();

            if (onlyHeaders)
                return;

            Send(response, resourceStream, resourceStream.Length);
        }

        protected void Send(IHttpResponse response, Stream resourceStream, long length)
        {
            const int BUF_LEN = 8192;
            byte[] buffer = new byte[BUF_LEN];
            int bytesRead;
            while ((bytesRead = resourceStream.Read(buffer, 0, length > BUF_LEN ? BUF_LEN : (int)length)) > 0) // Don't use Math.Min since (int) length is negative for length > Int32.MaxValue
            {
                length -= bytesRead;
                response.SendBody(buffer, 0, bytesRead);
            }
        }

        public void Dispose()
        {
            if (_tidyUpCacheWork != null)
            {
                IThreadPool threadPool = ServiceRegistration.Get<IThreadPool>();
                threadPool.RemoveIntervalWork(_tidyUpCacheWork);
                _tidyUpCacheWork = null;
            }
            ClearResourceAccessorCache();
        }
    }
}
