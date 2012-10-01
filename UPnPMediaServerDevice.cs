using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml;
using MediaPortal.Backend.Services.ClientCommunication;
using MediaPortal.Common;
using MediaPortal.Common.Logging;
using UPnP.Infrastructure.Dv;
using UPnP.Infrastructure.Dv.DeviceTree;

namespace MediaPortal.Extensions.MediaServer
{
    public class UPnPMediaServerDevice : DvDevice
    {
        public const string MEDIASERVER_DEVICE_TYPE = "schemas-upnp-org:device:MediaServer";
        public const int MEDIASERVER_DEVICE_VERSION = 1;

        public const string CONTENT_DIRECTORY_SERVICE_TYPE = "schemas-upnp-org:service:ContentDirectory";
        public const int CONTENT_DIRECTORY_SERVICE_TYPE_VERSION = 1;
        public const string CONTENT_DIRECTORY_SERVICE_ID = "upnp-org:serviceId:ContentDirectory";

        public const string CONNECTION_MANAGER_SERVICE_TYPE = "schemas-upnp-org:service:ConnectionManager";
        public const int CONNECTION_MANAGER_SERVICE_TYPE_VERSION = 1;
        public const string CONNECTION_MANAGER_SERVICE_ID = "upnp-org:serviceId:ConnectionManager";


        public UPnPMediaServerDevice(string deviceUuid)
            : base(MEDIASERVER_DEVICE_TYPE, MEDIASERVER_DEVICE_VERSION, deviceUuid,
            new LocalizedUPnPDeviceInformation())
        {
            DescriptionGenerateHook += GenerateDescriptionFunc;
            AddService(new UPnPContentDirectoryServiceImpl());
            AddService(new UPnPConnectionManagerServiceImpl());
        }

        private static void GenerateDescriptionFunc(XmlWriter writer, GenerationPosition pos, EndpointConfiguration config, CultureInfo culture)
        {
            if (pos == GenerationPosition.AfterDeviceList)
            {
                writer.WriteElementString("dlna", "X_DLNADOC", "urn:schemas-dlna-org:device-1-0", "DMS-1.50");   
            }
        }

        internal static ILogger Logger
        {
            get { return ServiceRegistration.Get<ILogger>(); }
        }
    }
}
