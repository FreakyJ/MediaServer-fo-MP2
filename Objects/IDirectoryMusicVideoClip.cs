﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaPortal.Extensions.MediaServer.Objects
{
    /// <summary>
    /// A ‘musicVideoClip’ instance is a discrete piece of video that should be interpreted as a clip supporting a song (as opposed to, for example, a continuus TV broadcast or a movie).
    /// </summary>
    public interface IDirectoryMusicVideoClip : IDirectoryVideoItem
    {
        /// <summary>
        /// Name of an artist
        /// </summary>
        [DirectoryProperty("upnp:artist", Required = false)]
        IList<string> Artist { get; set; }

        /// <summary>
        /// Indicates the type of storage medium used for the content.
        /// </summary>
        [DirectoryProperty("upnp:storageMedium", Required = false)]
        string StorageMedium { get; set; }

        /// <summary>
        /// Title of the album to which the item belongs.
        /// </summary>
        [DirectoryProperty("upnp:album", Required = false)]
        IList<string> Album { get; set; }

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
