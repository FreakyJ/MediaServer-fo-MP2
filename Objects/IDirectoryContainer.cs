using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaPortal.Extensions.MediaServer.Objects
{
    /// <summary>
    /// This is a derived class of object used to represent containers e.g. a music album.
    /// </summary>
    public interface IDirectoryContainer : IDirectoryObject
    {
        /// <summary>
        /// Child count for the object. Applies to containers only.
        /// </summary>
        [DirectoryProperty("@childCount", Required = false)]
        int ChildCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DirectoryProperty("upnp:createClass", Required = false)]
        IList<IDirectoryCreateClass> CreateClass { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DirectoryProperty("upnp:searchClass", Required = false)]
        IList<IDirectorySearchClass> SearchClass { get; set; }

        /// <summary>
        /// When true, the ability to perform a Search() action under a container is enabled, otherwise a Search() under that container will return no results. The default value of this attribute when it is absent on a container is false
        /// </summary>
        [DirectoryProperty("@searchable", Required = false)]
        bool Searchable { get; set; }

    }
}
