using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediaPortal.Common.MediaManagement;

namespace MediaPortal.Extensions.MediaServer.Objects.MediaLibrary
{
    public class MediaLibraryImageItem : MediaLibraryItem, IDirectoryImageItem
    {
        public MediaLibraryImageItem(string baseKey, MediaItem item) : base(baseKey, item)
        {
            Publisher = new List<string>();
            Rights = new List<string>();
        }

        public override string Class
        {
            get { return "object.item.imageItem"; }
        }

        public string LongDescription { get; set; }

        public string StorageMedium { get; set; }

        public string Rating { get; set; }

        public string Description { get; set; }

        public IList<string> Publisher { get; set; }

        public string Date { get; set; }

        public IList<string> Rights { get; set; }
    }
}
