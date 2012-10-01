using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediaPortal.Common.MediaManagement;
using MediaPortal.Common.MediaManagement.DefaultItemAspects;

namespace MediaPortal.Extensions.MediaServer.Objects.MediaLibrary
{
    public class MediaLibraryVideoItem : MediaLibraryItem, IDirectoryVideoItem
    {
        public MediaLibraryVideoItem(string baseKey, MediaItem item) : base(baseKey, item)
        {
            Genre = new List<string>();
            Producer = new List<string>();
            Actor = new List<string>();
            Director = new List<string>();
            Publisher = new List<string>();

            var videoAspect = item.Aspects[VideoAspect.ASPECT_ID];
            IEnumerable genreObj = videoAspect.GetCollectionAttribute(VideoAspect.ATTR_GENRES);
            if (genreObj != null)
                foreach (string genre in genreObj)
                    Genre.Add(genre);

            var resource = new MediaLibraryResource(item);
            resource.Initialise();
            Resources.Add(resource);
        }

        public override string Class
        {
            get { return "object.item.videoItem"; }
        }

        public IList<string> Genre { get; set; }

        public string LongDescription { get; set; }

        public IList<string> Producer { get; set; }

        public string Rating { get; set; }

        public IList<string> Actor { get; set; }

        public IList<string> Director { get; set; }

        public string Description { get; set; }

        public IList<string> Publisher { get; set; }

        public string Language { get; set; }

        public string Relation { get; set; }
    }
}
