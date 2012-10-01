using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediaPortal.Extensions.MediaServer.Objects;

namespace MediaPortal.Extensions.MediaServer.Objects
{
    public interface IDirectoryPhotoAlbum : IDirectoryAlbum
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
        /// Name of producer of e.g., a movie or CD
        /// </summary>
        [DirectoryProperty("dc:producer", Required = false)]
        IList<string> Producer { get; set; }

        /// <summary>
        /// Reference to album art. Values must be properly escaped URIs as described in [RFC 2396].
        /// </summary>
        [DirectoryProperty("upnp:albumArtURI", Required = false)]
        string AlbumArtUrl { get; set; }

        /// <summary>
        /// Identifier of an audio CD.
        /// </summary>
        [DirectoryProperty("upnp:toc", Required = false)]
        string Toc { get; set; }
    }
}
