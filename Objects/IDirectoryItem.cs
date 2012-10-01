using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaPortal.Extensions.MediaServer.Objects
{
    /// <summary>
    /// This is a derived class of object used to represent “atomic” content objects, i.e., object that don’t contain other objects, for example, a music track on an audio CD.
    /// </summary>
    public interface IDirectoryItem : IDirectoryObject
    {
        /// <summary>
        /// id property of the item being referred to.
        /// </summary>
        [DirectoryProperty("@refID", Required = false)]
        string RefId { get; set; }
    }
}
