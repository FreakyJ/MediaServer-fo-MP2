using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaPortal.Extensions.MediaServer.Objects
{
    /// <summary>
    /// A ‘videoItem’ instance represents a piece of content that, when rendered, generates some video. It is atomic in the sense that it does not contain other objects in the ContentDirectory.
    /// </summary>
    public interface IDirectoryVideoItem : IDirectoryItem
    {
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
        /// Name of producer of e.g., a movie or CD
        /// </summary>
        [DirectoryProperty("upnp:producer", Required = false)]
        IList<string> Producer { get; set; }

        /// <summary>
        /// Rating of the object’s resource, for ‘parental control’ filtering purposes, such as “R”, “PG-13”, “X”, etc.,.
        /// </summary>
        [DirectoryProperty("upnp:rating", Required = false)]
        string Rating { get; set; }

        /// <summary>
        /// Name of an actor appearing in a video item
        /// </summary>
        [DirectoryProperty("upnp:actor", Required = false)]
        IList<string> Actor { get; set; }

        /// <summary>
        /// Name of the director of the video content item (e.g., the movie)
        /// </summary>
        [DirectoryProperty("upnp:director", Required = false)]
        IList<string> Director { get; set; }

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
        /// A language of the resource.
        /// </summary>
        [DirectoryProperty("dc:language", Required = false)]
        string Language { get; set; }

        /// <summary>
        /// A related resource.
        /// </summary>
        [DirectoryProperty("dc:relation", Required = false)]
        string Relation { get; set; }
    }
}
