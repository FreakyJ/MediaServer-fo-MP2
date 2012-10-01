using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaPortal.Extensions.MediaServer.Objects
{
    /// <summary>
    /// A ‘movie’ instance is a discrete piece of video that should be interpreted as a movie (as opposed to, for example, a continuus TV broadcast or a music video clip).
    /// </summary>
    public interface IDirectoryMovie : IDirectoryVideoItem
    {
        /// <summary>
        /// Indicates the type of storage medium used for the content. Potentially useful for user-interface purposes.
        /// </summary>
        [DirectoryProperty("upnp.storageMedium", Required = false)]
        string StorageMedium { get; set; }

        /// <summary>
        /// Region code of the DVD disc
        /// </summary>
        [DirectoryProperty("upnp:DVDRegionCode", Required = false)]
        int DvdRegionCode { get; set; }

        /// <summary>
        /// Used for identification of channels themselves, or information associated with a piece of recorded content
        /// </summary>
        [DirectoryProperty("upnp:channelName", Required = false)]
        string ChannelName { get; set; }

        /// <summary>
        /// ISO 8601, of the form " yyyy-mm-ddThh:mm:ss". Used to indicate the start time of a schedule program, indented for use by tuners
        /// </summary>
        [DirectoryProperty("upnp:scheduledStartTime", Required = false)]
        string ScheduledStartTime { get; set; }

        /// <summary>
        /// ISO 8601, of the form " yyyy-mm-ddThh:mm:ss". Used to indicate the end time of a scheduled program, indented for use by tuners
        /// </summary>
        [DirectoryProperty("upnp:scheduledEndTime", Required = false)]
        string ScheduledEndTime { get; set; }
    }
}
