using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using MediaPortal.Common;
using MediaPortal.Common.MediaManagement;
using MediaPortal.Common.MediaManagement.DefaultItemAspects;
using MediaPortal.Common.ResourceAccess;
using MediaPortal.Common.Services.ResourceAccess;
using MediaPortal.Extensions.MediaServer.DLNA;
using MediaPortal.Extensions.MediaServer.ResourceAccess;
using UPnP.Infrastructure.Utils;

namespace MediaPortal.Extensions.MediaServer.Objects.MediaLibrary
{
    public class MediaLibraryResource : IDirectoryResource
    {
        private MediaItem Item { get; set; }

        public MediaLibraryResource(MediaItem item)
        {
            Item = item;            
        }

        public string GetLocalIp()
        {            
            var localIp = Dns.GetHostName();
            var host = Dns.GetHostEntry(localIp);
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    localIp = ip.ToString();
                }
            }
            return localIp;
        }

        public void Initialise()
        {
            var rs = ServiceRegistration.Get<IResourceServer>();

            var ipaddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList[0];

            var baseUrl = "http://" + GetLocalIp() + ":" + rs.PortIPv4;
            var url = baseUrl + DlnaResourceAccessUtils.GetResourceUrl(Item.MediaItemId);

            ProtocolInfo = DlnaProtocolInfoFactory.GetProfileInfo(Item).ToString();           

            if (Item.Aspects.ContainsKey(VideoAspect.ASPECT_ID))
            {
                // Load Video Specific items
                var videoAspect = Item.Aspects[VideoAspect.ASPECT_ID];

                Resolution = videoAspect.GetAttributeValue(VideoAspect.ATTR_WIDTH) 
                             + "x" 
                             + videoAspect.GetAttributeValue(VideoAspect.ATTR_HEIGHT);

                var vidBitRate = Convert.ToInt32(videoAspect.GetAttributeValue(VideoAspect.ATTR_VIDEOBITRATE));
                var audBitRate = Convert.ToInt32(videoAspect.GetAttributeValue(VideoAspect.ATTR_AUDIOBITRATE));
                BitRate = (uint)(vidBitRate + audBitRate);
            }

            Uri = url;
        }

        public string Uri { get; set; }

        public ulong Size { get; set; }

        public string Duration { get; set; }

        public uint BitRate { get; set; }

        public uint SampleFrequency { get; set; }

        public uint BitsPerSample { get; set; }

        public uint NumberOfAudioChannels { get; set; }

        public string Resolution { get; set; }

        public uint ColorDepth { get; set; }

        public string ProtocolInfo { get; set; }

        public string Protection { get; set; }

        public string ImportUri { get; set; }

        public string DlnaIfoFileUrl { get; set; }
    }
}
