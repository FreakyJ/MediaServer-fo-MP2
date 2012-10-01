using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaPortal.Extensions.MediaServer.Objects
{
    /// <summary>
    /// A ‘musicTrack’ instance is a discrete piece of audio that should be interpreted as music (as opposed to, for example, a news broadcast or an audio book).
    /// </summary>
    public interface IDirectoryMusicTrack : IDirectoryAudioItem
    {
        /// <summary>
        /// Name of an artist
        /// </summary>
        [DirectoryProperty("upnp:artist", Required = false)]
        IList<string> Artist { get; set; }
        
        /// <summary>
        /// Title of the album to which the item belongs.
        /// </summary>
        [DirectoryProperty("upnp:album", Required = false)]
        IList<string> Album { get; set; }

        /// <summary>
        /// Original track number on an audio CD or other medium
        /// </summary>
        [DirectoryProperty("upnp:originalTrackNumber", Required = false)]
        int OriginalTrackNumber { get; set; }

        /// <summary>
        /// Name of a playlist to which the item belongs
        /// </summary>
        [DirectoryProperty("upnp:playlist", Required = false)]
        IList<string> Playlist { get; set; }

        /// <summary>
        /// Indicates the type of storage medium used for the content.
        /// </summary>
        [DirectoryProperty("upnp:storageMedium", Required = false)]
        string StorageMedium { get; set; }

        /// <summary>
        /// It is recommended that contributor includes the name of the primary content creator or owner (DublinCore ‘creator’ property)
        /// </summary>
        [DirectoryProperty("dc:contributor", Required = false)]
        IList<string> Contributor { get; set; }

        /// <summary>
        /// ISO 8601, of the form "YYYY-MM-DD",
        /// </summary>
        [DirectoryProperty("dc:date", Required = false)]
        string Date { get; set; }
    }
}
