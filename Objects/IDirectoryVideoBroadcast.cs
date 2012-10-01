using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaPortal.Extensions.MediaServer.Objects
{
    /// <summary>
    /// A ‘videoBroadcast’ instance is a continuus stream of video that should be interpreted as a broadcast (e.g., a convential TV channel or a Webcast).
    /// A tvStation represents an (Internet or conventional) tv station, and is derived from the cdsItemContainer base class. A tv channel can contain other items representing the broadcast schedule of the channel, or it can be present as an atomatic item, for example when no schedule information is known.
    /// </summary>
    public interface IDirectoryVideoBroadcast : IDirectoryVideoItem
    {
        /// <summary>
        /// Some icon that a control point can use in its UI to display the content, e.g. a CNN logo for a Tuner channel. Recommend same format as the icon element in the UPnP device description document schema. (PNG). Values must be properly escaped URIs as described in [RFC 2396].
        /// </summary>
        [DirectoryProperty("upnp:icon", Required = false)]
        string Icon { get; set; }

        /// <summary>
        /// Some identification of the region, associated with the ‘source’ of the object, e.g. “US”, “Latin America”, “Seattlle”.
        /// </summary>
        [DirectoryProperty("upnp:region", Required = false)]
        string Region { get; set; }

        /// <summary>
        /// Used for identification of tuner channels themselves, or information associated with a piece of recorded content
        /// </summary>
        [DirectoryProperty("upnp:channelNr", Required = false)]
        int ChannelNr { get; set; }
    }
}
