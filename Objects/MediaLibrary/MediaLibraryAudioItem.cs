using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediaPortal.Common.MediaManagement;
using MediaPortal.Common.MediaManagement.DefaultItemAspects;

namespace MediaPortal.Extensions.MediaServer.Objects.MediaLibrary
{
    public class MediaLibraryAudioItem : MediaLibraryItem, IDirectoryAudioItem
    {
        public MediaLibraryAudioItem(string baseKey, MediaItem item) : base(baseKey, item)
        {
            Genre = new List<string>();
            Publisher = new List<string>();
            Rights = new List<string>();

            var audioAspect = item.Aspects[AudioAspect.ASPECT_ID];
            var genreObj = audioAspect.GetCollectionAttribute(AudioAspect.ATTR_GENRES);            
            if (genreObj != null) Genre.Add(genreObj.ToString());

            var resource = new MediaLibraryResource(item);
            resource.Initialise();
            Resources.Add(resource);
        }

        public override string Class
        {
            get { return "object.item.audioItem"; }
        }

        public IList<string> Genre { get; set; }

        public string Description { get; set; }

        public string LongDescription { get; set; }

        public IList<string> Publisher { get; set; }

        public string Language { get; set; }

        public string Relation { get; set; }

        public IList<string> Rights { get; set; }
    }
}
