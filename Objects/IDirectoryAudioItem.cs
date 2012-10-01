using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaPortal.Extensions.MediaServer.Objects
{
    public interface IDirectoryAudioItem : IDirectoryItem
    {
        /// <summary>
        /// Name of the genre to which an object belongs
        /// </summary>
        [DirectoryProperty("upnp:genre", Required = false)]
        IList<string> Genre { get; set; }

        /// <summary>
        /// An account of the resource.
        /// </summary>
        [DirectoryProperty("dc:description", Required = false)]
        string Description { get; set; }

        /// <summary>
        /// A few lines of description of the content item (longer than DublinCore’s description element
        /// </summary>
        [DirectoryProperty("upnp:longDescription", Required = false)]
        string LongDescription { get; set; }

        /// <summary>
        /// An entity responsible for making the resource available.
        /// </summary>
        [DirectoryProperty("dc:publisher", Required = false)]
        IList<string> Publisher { get; set; }

        /// <summary>
        /// A language of the resource.
        /// </summary>
        [DirectoryProperty("dc:language", Required = false)]
        string Language { get; set; }

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
