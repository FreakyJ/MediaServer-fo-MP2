using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaPortal.Extensions.MediaServer.Objects
{
    /// <summary>
    /// A ‘genre’ instance represents an unordered collection of ‘objects’ that “belong” to the genre, in a loose sense.
    /// </summary>
    public interface IDirectoryGenre : IDirectoryContainer
    {
        /// <summary>
        /// A few lines of description of the content item (longer than DublinCore’s description element
        /// </summary>
        [DirectoryProperty("upnp:longDescription", Required = false)]
        string LongDescription { get; set; }

        /// <summary>
        /// An account of the resource.
        /// </summary>
        [DirectoryProperty("dc:description", Required = false)]
        string Description { get; set; }
    }

    /// <summary>
    /// A ‘musicGenre’ instance is a ‘genre’ which should be interpreted as a “style of music”. A ‘musicGenre’ container can contain objects of class ‘musicArtist, ‘musicAlbum’, ‘audioItem’ or “sub”-music genres of the same class (e.g. ‘Rock’ contains ‘Alternative Rock’).
    /// </summary>
    public interface IDirectoryMusicGenre : IDirectoryGenre
    {
        
    }

    /// <summary>
    /// A ‘movieGenre’ instance is a ‘genre’ which should be interpreted as a “style of movies”. A ‘movieGenre’ container can contain objects of class ‘people’, ‘videoItem’ or “sub”-movie genres of the same class (e.g. ‘Western’ contains ‘Spaghetti Western’).
    /// </summary>
    public interface IDirectoryMovieGenre : IDirectoryGenre
    {
        
    }
}
