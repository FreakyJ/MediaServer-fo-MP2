using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaPortal.Extensions.MediaServer.Objects
{
    /// <summary>
    /// An ‘audioBroadcast’ instance is a continuus stream of audio that should be interpreted as an audio broadcast (as opposed to, for example, a song or an audio book).
    /// </summary>
    public interface IDirectoryAudioBroadcast : IDirectoryAudioItem
    {
        /// <summary>
        /// Some identification of the region, associated with the ‘source’ of the object, e.g. “US”, “Latin America”, “Seattlle”.
        /// </summary>
        [DirectoryProperty("upnp:region", Required = false)]
        string Region { get; set; }

        /// <summary>
        /// Radio station call sign, e.g. “KSJO”
        /// </summary>
        [DirectoryProperty("upnp:radioCallSign", Required = false)]
        string RadioCallSign { get; set; }

        /// <summary>
        /// Some identification, e.g. “107.7”, broadcast frequency of the radio station
        /// </summary>
        [DirectoryProperty("upnp:radioStationID", Required = false)]
        string RadioStationId { get; set; }

        /// <summary>
        /// Radio station frequency band. Recommended values are “AM”, “FM”, “Shortwave“, “Internet”, “Satellite”. Vendor’s may extend this list.
        /// </summary>
        [DirectoryProperty("upnp:radioBand", Required = false)]
        string RadioBand { get; set; }

        /// <summary>
        /// Used for identification of tuner channels themselves, or information associated with a piece of recorded content
        /// </summary>
        [DirectoryProperty("upnp:channelNr", Required = false)]
        int ChannelNr { get; set; }


    }
}
