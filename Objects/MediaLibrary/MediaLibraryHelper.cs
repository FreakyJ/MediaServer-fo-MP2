using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediaPortal.Backend.MediaLibrary;
using MediaPortal.Common;
using MediaPortal.Common.Logging;
using MediaPortal.Common.MediaManagement;
using MediaPortal.Common.MediaManagement.DefaultItemAspects;
using MediaPortal.Extensions.MediaServer.Objects.Basic;
using MediaPortal.Extensions.MediaServer.Tree;

namespace MediaPortal.Extensions.MediaServer.Objects.MediaLibrary
{
    static class MediaLibraryHelper
    {
        public const string MIMETYPE_AUDIO = "audio/mpeg";
        public const string MIMETYPE_VIDEO = "video/mpeg";
        public const string MIMETYPE_IMAGE = "image/jpeg";

        public static Guid GetObjectId(string key)
        {
            var split = key.IndexOf(':');
            return split > 0 ? new Guid(key.Substring(split + 1)) : Guid.Empty;
        }

        public static string GetBaseKey(string key)
        {
            var split = key.IndexOf(':');
            return split > 0 ? key.Substring(0, split) : key;
        }

        public static string GetOrGuessMimeType(MediaItem item)
        {
          string mediaMimeType = item.Aspects[MediaAspect.ASPECT_ID].GetAttributeValue(MediaAspect.ATTR_MIME_TYPE) as String;
          if (!string.IsNullOrEmpty(mediaMimeType))
            return mediaMimeType;
          if (item.Aspects.ContainsKey(ImageAspect.ASPECT_ID))
            return MIMETYPE_IMAGE;
          if (item.Aspects.ContainsKey(VideoAspect.ASPECT_ID))
            return MIMETYPE_VIDEO;
          if (item.Aspects.ContainsKey(AudioAspect.ASPECT_ID))
            return MIMETYPE_AUDIO;
          return null;
        }

        public static MediaItem GetMediaItem(Guid id)
        {
            var library = ServiceRegistration.Get<IMediaLibrary>();

            var necessaryMIATypeIDs = new Guid[]
            {
                ProviderResourceAspect.ASPECT_ID,
                MediaAspect.ASPECT_ID,
            };
            var optionalMIATypeIDs = new Guid[]
            {
                DirectoryAspect.ASPECT_ID,
                VideoAspect.ASPECT_ID,
                // Morpheus_xx: also request image and audio information
                ImageAspect.ASPECT_ID,
                AudioAspect.ASPECT_ID
            };

            return library.GetMediaItem(id, necessaryMIATypeIDs, optionalMIATypeIDs);
        }

        public static IDirectoryObject InstansiateMediaLibraryObject(MediaItem item, string baseKey, BasicContainer parent)
        {
            return InstansiateMediaLibraryObject(item, baseKey, parent, null);
        }

        public static IDirectoryObject InstansiateMediaLibraryObject(MediaItem item, string baseKey, BasicContainer parent, string title)
        {
            IDirectoryObject obj;
            // Choose the appropiate MediaLibrary* object for the media item
            if (item.Aspects.ContainsKey(DirectoryAspect.ASPECT_ID))
            {
                obj = new MediaLibraryContainer(baseKey, item);
            }
            else if (item.Aspects.ContainsKey(AudioAspect.ASPECT_ID))
            {
                obj = new MediaLibraryMusicTrack(baseKey, item);
            }
            else if (item.Aspects.ContainsKey(ImageAspect.ASPECT_ID))
            {
                obj = new MediaLibraryImageItem(baseKey, item);
            }
            else if (item.Aspects.ContainsKey(VideoAspect.ASPECT_ID))
            {
                obj = new MediaLibraryVideoItem(baseKey, item);
            }
            else
            {
                obj = null;
            }

            // Assign the parent
            if (parent != null && obj != null)
            {
                parent.Add((TreeNode<object>)obj);
            }

            // Initialise the object
            if (obj is MediaLibraryContainer)
            {
                ((MediaLibraryContainer)obj).Initialise();                
            }
            else if (obj is MediaLibraryItem)
            {
                ((MediaLibraryItem)obj).Initialise();
            }
            if (obj != null)
            {
                ServiceRegistration.Get<ILogger>().Info("Created object of type {0} for MediaItem {1}", obj.GetType().Name, item.MediaItemId);
                if (title != null)
                {
                    obj.Title = title;
                }
            }      
      
            return obj;
        }

    }
}
