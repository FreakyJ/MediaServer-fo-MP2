using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaPortal.Extensions.MediaServer.Objects
{
    /// <summary>
    /// A ‘person’ instance represents an unordered collection of ‘objects’ that “belong” to the people, in a loose sense.
    /// </summary>
    public interface IDirectoryPerson : IDirectoryContainer
    {
        /// <summary>
        /// A language of the resource.
        /// </summary>
        [DirectoryProperty("dc:language", Required = false)]
        IList<string> Language { get; set; }
    }

    /// <summary>
    /// A ‘musicArtist’ instance is a ‘person’ which should be interpreted as a music artist. A ‘musicArtist’ container can contain objects of class ‘musicAlbum’, ‘musicTrack’ or ‘musicVideoClip’.
    /// </summary>
    public interface IDirectoryMusicArtist : IDirectoryPerson
    {
        
    }
}
