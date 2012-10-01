using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaPortal.Extensions.MediaServer.Objects
{
    /// <summary>
    /// A ‘playlistItem’ instance represents a playable sequence of resources. It is different from ‘musicAlbum’ in the sense that a ‘playlistItem’ may contain a mix of audio, video and images and is typically created by a user, while an ‘album’ is typically a fixed published sequence of songs (e.g., an audio CD).
    /// </summary>
    public interface IDirectoryPlaylistItem : IDirectoryItem
    {
        /// <summary>
        /// Name of an artist
        /// </summary>
        [DirectoryProperty("upnp:artist", Required = false)]
        IList<string> Artist { get; set; }

        /// <summary>
        /// Name of the genre to which an object belongs
        /// </summary>
        [DirectoryProperty("upnp:genre", Required = false)]
        IList<string> Genre { get; set; }

        /// <summary>
        /// A few lines of description of the content item (longer than DublinCore’s description element
        /// </summary>
        [DirectoryProperty("upnp:longDescription", Required = false)]
        string LongDescription { get; set; }

        /// <summary>
        /// Indicates the type of storage medium used for the content.
        /// </summary>
        [DirectoryProperty("upnp:storageMedium", Required = false)]
        string StorageMedium { get; set; }

        /// <summary>
        /// An account of the resource.
        /// </summary>
        [DirectoryProperty("dc:description", Required = false)]
        string Description { get; set; }

        /// <summary>
        /// ISO 8601, of the form "YYYY-MM-DD",
        /// </summary>
        [DirectoryProperty("dc:date", Required = false)]
        string Date { get; set; }

        /// <summary>
        /// A language of the resource.
        /// </summary>
        [DirectoryProperty("dc:language", Required = false)]
        string Language { get; set; }
        
    }
}
