using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaPortal.Extensions.MediaServer.Objects
{
    /// <summary>
    /// A ‘photo’ instance is an image that should be interpreted as a photo (as opposed to, for example, an icon).
    /// </summary>
    public interface IDirectoryPhoto : IDirectoryImageItem
    {
        /// <summary>
        /// Title of the album to which the item belongs.
        /// </summary>
        [DirectoryProperty("upnp:album", Required = false)]
        IList<string> Album { get; set; }        
    }
}
