using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaPortal.Extensions.MediaServer.Objects
{
    /// <summary>
    /// An ‘imageItem’ instance represents a piece of content that, when rendered, generates some still image. It is atomic in the sense that it does not contain other objects in the ContentDirectory.
    /// </summary>
    public interface IDirectoryImageItem : IDirectoryItem
    {
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
        /// Rating of the object’s resource, for ‘parental control’ filtering purposes, such as “R”, “PG-13”, “X”, etc.,.
        /// </summary>
        [DirectoryProperty("upnp:rating", Required = false)]
        string Rating { get; set; }

        /// <summary>
        /// An account of the resource.
        /// </summary>
        [DirectoryProperty("dc:description", Required = false)]
        string Description { get; set; }

        /// <summary>
        /// An entity responsible for making the resource available.
        /// </summary>
        [DirectoryProperty("dc:publisher", Required = false)]
        IList<string> Publisher { get; set; }

        /// <summary>
        /// ISO 8601, of the form "YYYY-MM-DD",
        /// </summary>
        [DirectoryProperty("dc:date", Required = false)]
        string Date { get; set; }

        /// <summary>
        /// Information about rights held in and over the resource.
        /// </summary>
        [DirectoryProperty("dc:rights", Required = false)]
        IList<string> Rights { get; set; }
    }
}
