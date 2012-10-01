using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaPortal.Extensions.MediaServer.Objects
{
    public interface IDirectoryResource
    {
        [DirectoryProperty("")]
        string Uri { get; set; }

        [DirectoryProperty("@size", Required = false)]
        ulong Size { get; set; }

        [DirectoryProperty("@duration", Required = false)]
        string Duration { get; set; }

        [DirectoryProperty("@bitrate", Required = false)]
        uint BitRate { get; set; }

        [DirectoryProperty("@sampleFrequency", Required = false)]
        uint SampleFrequency { get; set; }

        [DirectoryProperty("@bitsPerSample", Required = false)]
        uint BitsPerSample { get; set; }

        [DirectoryProperty("@nrAudioChannels", Required = false)]
        uint NumberOfAudioChannels { get; set; }

        [DirectoryProperty("@resolution", Required = false)]
        string Resolution { get; set; }

        [DirectoryProperty("@colorDepth", Required = false)]
        uint ColorDepth { get; set; }

        [DirectoryProperty("@protocolInfo")]
        string ProtocolInfo { get; set; }

        [DirectoryProperty("@protection", Required = false)]
        string Protection { get; set; }

        [DirectoryProperty("@importUri", Required = false)]
        string ImportUri { get; set; }

        [DirectoryProperty("@dlna:ifoFileURI", Required = false)]
        string DlnaIfoFileUrl { get; set; }
    }
}
