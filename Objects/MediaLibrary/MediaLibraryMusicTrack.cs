using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediaPortal.Common.MediaManagement;
using MediaPortal.Common.MediaManagement.DefaultItemAspects;

namespace MediaPortal.Extensions.MediaServer.Objects.MediaLibrary
{
    public class MediaLibraryMusicTrack : MediaLibraryAudioItem, IDirectoryMusicTrack
    {
        public MediaLibraryMusicTrack(string baseKey, MediaItem item) : base(baseKey, item)
        {
            Artist = new List<string>();
            Album = new List<string>();
            Playlist = new List<string>();
            Contributor = new List<string>();

            var audioAspect = item.Aspects[AudioAspect.ASPECT_ID];
            var album = audioAspect.GetAttributeValue(AudioAspect.ATTR_ALBUM);
            if (album != null) Album.Add(album.ToString());

            var artists = audioAspect.GetCollectionAttribute(AudioAspect.ATTR_ARTISTS);
            if (artists != null) Artist = new List<string>(artists.Cast<string>());

            var originalTrack = audioAspect.GetAttributeValue(AudioAspect.ATTR_TRACK);
            if (originalTrack != null) OriginalTrackNumber = Convert.ToInt32(originalTrack.ToString());
            
        }

        public override string Class
        {
            get { return "object.item.audioItem.musicTrack"; }
        }

        public IList<string> Artist { get; set; }

        public IList<string> Album { get; set; }
        
        public int OriginalTrackNumber { get; set; }
        
        public IList<string> Playlist { get; set; }
        
        public string StorageMedium { get; set; }
        
        public IList<string> Contributor { get; set; }
        
        public string Date { get; set; }
    }
}
