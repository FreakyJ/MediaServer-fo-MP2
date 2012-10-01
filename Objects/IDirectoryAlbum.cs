using System.Collections.Generic;

namespace MediaPortal.Extensions.MediaServer.Objects
{
    /// <summary>
    /// An ‘album’ instance represents an ordered collection of ‘objects’.
    /// </summary>
    public interface IDirectoryAlbum : IDirectoryContainer
    {
        /// <summary>
        /// Indicates the type of storage medium used for the content.
        /// </summary>
        [DirectoryProperty("upnp:storageMedium", Required = false)]
        string StorageMedium { get; set; }

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

        /// <summary>
        /// An entity responsible for making the resource available.
        /// </summary>
        [DirectoryProperty("dc:publisher", Required = false)]
        IList<string> Publisher { get; set; }

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

        /// <summary>
        /// A related resource.
        /// </summary>
        [DirectoryProperty("dc:relation", Required = false)]
        string Relation { get; set; }

        /// <summary>
        /// Information about rights held in and over the resource.
        /// </summary>
        [DirectoryProperty("dc:rights", Required = false)]
        IList<string> Rights { get; set; }
    }
}
