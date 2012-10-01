using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using MediaPortal.Common;
using MediaPortal.Common.Logging;

namespace MediaPortal.Extensions.MediaServer.ResourceAccess
{

    public static class DlnaResourceAccessUtils
    {
        /// <summary>
        /// Base HTTP path for resource access, e.g. "/GetDlnaResource".
        /// </summary>
        public const string RESOURCE_ACCESS_PATH = "/GetDlnaResource";

        /// <summary>
        /// Argument name for the resource path argument, e.g. "MediaItem".
        /// </summary>
        public const string RESOURCE_PATH_ARGUMENT_NAME = "ResourcePath";


        public const string SYNTAX = RESOURCE_ACCESS_PATH + "/[media item guid]";


        public static string GetResourceUrl(Guid mediaItem)
        {
            return RESOURCE_ACCESS_PATH + "/" + mediaItem.ToString();
        }

        public static bool ParseMediaItem(Uri resourceUri, out Guid mediaItemGuid)
        {
            try
            {
                var r = Regex.Match(resourceUri.PathAndQuery, RESOURCE_ACCESS_PATH + @"\/([\w-]*)\/?");
                var mediaItem = r.Groups[1].Value;
                mediaItemGuid = new Guid(mediaItem);
            }
            catch (Exception e)
            {
                ServiceRegistration.Get<ILogger>().Warn("ParseMediaItem: Failed with input url {0}", e, resourceUri.OriginalString);
                mediaItemGuid = Guid.Empty; 
                return false;
            }
                        
            return true;
        }
    }
}
