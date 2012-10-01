using System.Collections.Generic;

namespace MediaPortal.Extensions.MediaServer.Objects
{
    /// <summary>
    /// An ‘audioBook’ instance is a discrete piece of audio that should be interpreted as a book (as opposed to, for example, a news broadcast or a song).
    /// </summary>
    public interface IDirectoryAudioBook : IDirectoryAudioItem
    {
        /// <summary>
        /// Indicates the type of storage medium used for the content.
        /// </summary>
        [DirectoryProperty("upnp:storageMedium", Required = false)]
        string StorageMedium { get; set; }

        /// <summary>
        /// Name of producer of e.g., a movie or CD
        /// </summary>
        [DirectoryProperty("upnp:producer")]
        IList<string> Producer { get; set; }

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
    }
}
