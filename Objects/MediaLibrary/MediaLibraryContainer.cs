using System;
using System.Collections.Generic;
using System.Linq;
using MediaPortal.Backend.MediaLibrary;
using MediaPortal.Common;
using MediaPortal.Common.MediaManagement;
using MediaPortal.Common.MediaManagement.DefaultItemAspects;
using MediaPortal.Extensions.MediaServer.Objects.Basic;

namespace MediaPortal.Extensions.MediaServer.Objects.MediaLibrary
{
    public class MediaLibraryContainer : BasicContainer
    {
        public MediaItem Item { get; protected set; }

        public MediaLibraryContainer(string baseKey, MediaItem item)
            : base(baseKey + ":" + item.MediaItemId)
        {
            Item = item;
        }

        public override int ChildCount { get; set; }

        public override void Initialise()
        {
            Title = Item.Aspects[MediaAspect.ASPECT_ID].GetAttributeValue(MediaAspect.ATTR_TITLE).ToString();
            ChildCount = MediaLibraryBrowse().Count;
        }

        private ICollection<MediaItem> MediaLibraryBrowse()
        {
            var necessaryMIATypeIDs = new Guid[]
            {
                ProviderResourceAspect.ASPECT_ID,
                MediaAspect.ASPECT_ID,
            };
            var optionalMIATypeIDs = new Guid[]
            {
                DirectoryAspect.ASPECT_ID,
                VideoAspect.ASPECT_ID,
                AudioAspect.ASPECT_ID,
                ImageAspect.ASPECT_ID
            };

            var library = ServiceRegistration.Get<IMediaLibrary>();
            return library.Browse(Item.MediaItemId, necessaryMIATypeIDs, optionalMIATypeIDs);
        }

        public override List<IDirectoryObject> Search(string filter, string sortCriteria)
        {
            
            var result = (from item in MediaLibraryBrowse()
                          select MediaLibraryHelper.InstansiateMediaLibraryObject(item, MediaLibraryHelper.GetBaseKey(Key), this)).ToList();

            return result;
        }
    }
}
