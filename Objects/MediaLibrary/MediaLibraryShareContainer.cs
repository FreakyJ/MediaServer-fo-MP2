using System;
using System.Collections.Generic;
using System.Linq;
using MediaPortal.Backend.MediaLibrary;
using MediaPortal.Common;
using MediaPortal.Common.MediaManagement;
using MediaPortal.Common.MediaManagement.DefaultItemAspects;
using MediaPortal.Extensions.MediaServer.Objects.Basic;
using MediaPortal.Extensions.MediaServer.Tree;

namespace MediaPortal.Extensions.MediaServer.Objects.MediaLibrary
{
    public class MediaLibraryShareContainer : BasicContainer
    {
        protected Guid ObjectId { get; set; }
        protected string BaseKey { get; set; }

        public MediaLibraryShareContainer(string id) : base(id)
        {
            var split = id.IndexOf(':');
            BaseKey = MediaLibraryHelper.GetBaseKey(id);
            ObjectId = MediaLibraryHelper.GetObjectId(id);
        }

        public override int ChildCount
        {
            get
            {
                // This is some what inefficient
                return ChildCount = MediaLibraryShares().Count;
            }
            set { }
        }

        public override TreeNode<object> FindNode(string key)
        {
            if (!key.StartsWith(Key)) return null;
            if (key == Key) return this;

            var item = MediaLibraryHelper.GetMediaItem(MediaLibraryHelper.GetObjectId(key));
            var parentId = new Guid(item.Aspects[ProviderResourceAspect.ASPECT_ID].GetAttributeValue(
                ProviderResourceAspect.ATTR_PARENT_DIRECTORY_ID).ToString());

            BasicContainer parent;
            if (parentId == Guid.Empty)
            {
                parent = new BasicContainer(MediaLibraryHelper.GetBaseKey(key));
            }
            else
            {
                parent = new BasicContainer(MediaLibraryHelper.GetBaseKey(key) + ":" + parentId.ToString());                
            }

            return (TreeNode<object>)MediaLibraryHelper.InstansiateMediaLibraryObject(item, MediaLibraryHelper.GetBaseKey(key), parent);
        }

        private IDictionary<Guid, Share> MediaLibraryShares()
        {
            var library = ServiceRegistration.Get<IMediaLibrary>();
            return library.GetShares(null);
        }

        public override List<IDirectoryObject> Search(string filter, string sortCriteria)
        {            
            var shares = MediaLibraryShares();

            var necessaryMiaTypeIDs = new Guid[]
            {
                ProviderResourceAspect.ASPECT_ID,
                MediaAspect.ASPECT_ID,
            };
            var optionalMiaTypeIDs = new Guid[]
            {
                DirectoryAspect.ASPECT_ID,
            };

            var library = ServiceRegistration.Get<IMediaLibrary>();

            var parent = new BasicContainer(Id);
            var result = (from share in shares
                          let item = library.LoadItem(share.Value.SystemId, share.Value.BaseResourcePath, necessaryMiaTypeIDs, optionalMiaTypeIDs)                          
                          select MediaLibraryHelper.InstansiateMediaLibraryObject(item, Key, parent, share.Value.Name)).ToList();
            return result;
        }
    
    }
}
