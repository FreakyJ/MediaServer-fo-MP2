using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediaPortal.Common;
using MediaPortal.Common.Logging;
using UPnP.Infrastructure.Common;
using UPnP.Infrastructure.Dv;
using UPnP.Infrastructure.Dv.DeviceTree;

namespace MediaPortal.Extensions.MediaServer
{
  public class UPnPConnectionManagerServiceImpl : DvService
  {
    public UPnPConnectionManagerServiceImpl()
      : base(
          UPnPMediaServerDevice.CONNECTION_MANAGER_SERVICE_TYPE, UPnPMediaServerDevice.CONNECTION_MANAGER_SERVICE_TYPE_VERSION,
          UPnPMediaServerDevice.CONNECTION_MANAGER_SERVICE_ID)
    {
      // Used for a boolean value
      DvStateVariable SourceProtocolInfo = new DvStateVariable("SourceProtocolInfo",
                                                                  new DvStandardDataType(
                                                                      UPnPStandardDataType.String))
                                                  {
                                                    SendEvents = false,
                                                  };
      AddStateVariable(SourceProtocolInfo);

      // Used for a boolean value
      DvStateVariable SinkProtocolInfo = new DvStateVariable("SinkProtocolInfo",
                                                                  new DvStandardDataType(
                                                                      UPnPStandardDataType.String))
                                                  {
                                                    SendEvents = false,
                                                  };
      AddStateVariable(SinkProtocolInfo);

      // Used for a boolean value
      DvStateVariable CurrentConnectionIDs = new DvStateVariable("CurrentConnectionIDs",
                                                                  new DvStandardDataType(
                                                                      UPnPStandardDataType.String))
                                                  {
                                                    SendEvents = false,
                                                  };
      AddStateVariable(CurrentConnectionIDs);

      // Used for a boolean value
      DvStateVariable A_ARG_TYPE_ConnectionStatus = new DvStateVariable("A_ARG_TYPE_ConnectionStatus",
                                                                  new DvStandardDataType(
                                                                      UPnPStandardDataType.String))
                                                  {
                                                    SendEvents = false,
                                                    AllowedValueList = new List<string>() { "OK", "ContentFormatMismatch", "InsufficientBandwidth", "UnreliableChannel", "Unknown" }
                                                  };
      AddStateVariable(A_ARG_TYPE_ConnectionStatus);

      // Used for a boolean value
      DvStateVariable A_ARG_TYPE_ConnectionManager = new DvStateVariable("A_ARG_TYPE_ConnectionManager",
                                                                  new DvStandardDataType(
                                                                      UPnPStandardDataType.String))
                                                  {
                                                    SendEvents = false,
                                                  };
      AddStateVariable(A_ARG_TYPE_ConnectionManager);

      // Used for a boolean value
      DvStateVariable A_ARG_TYPE_Direction = new DvStateVariable("A_ARG_TYPE_Direction",
                                                                  new DvStandardDataType(
                                                                      UPnPStandardDataType.String))
                                                  {
                                                    SendEvents = false,
                                                    AllowedValueList = new List<string>() { "Output", "Input" }
                                                  };
      AddStateVariable(A_ARG_TYPE_Direction);

      // Used for a boolean value
      DvStateVariable A_ARG_TYPE_ProtocolInfo = new DvStateVariable("A_ARG_TYPE_ProtocolInfo",
                                                                  new DvStandardDataType(
                                                                      UPnPStandardDataType.String))
                                                  {
                                                    SendEvents = false,
                                                  };
      AddStateVariable(A_ARG_TYPE_ProtocolInfo);

      // Used for a boolean value
      DvStateVariable A_ARG_TYPE_ConnectionID = new DvStateVariable("A_ARG_TYPE_ConnectionID",
                                                                  new DvStandardDataType(
                                                                      UPnPStandardDataType.I4))
                                                  {
                                                    SendEvents = false,
                                                  };
      AddStateVariable(A_ARG_TYPE_ConnectionID);

      // Used for a boolean value
      DvStateVariable A_ARG_TYPE_AVTransportID = new DvStateVariable("A_ARG_TYPE_AVTransportID",
                                                                  new DvStandardDataType(
                                                                      UPnPStandardDataType.I4))
                                                  {
                                                    SendEvents = false,
                                                  };
      AddStateVariable(A_ARG_TYPE_AVTransportID);

      // Used for a boolean value
      DvStateVariable A_ARG_TYPE_RcsID = new DvStateVariable("A_ARG_TYPE_RcsID",
                                                                  new DvStandardDataType(
                                                                      UPnPStandardDataType.I4))
                                                  {
                                                    SendEvents = false,
                                                  };
      AddStateVariable(A_ARG_TYPE_RcsID);


      DvAction getProtocolInfoAction = new DvAction("GetProtocolInfo", OnGetProtocolInfo,
                                    new DvArgument[]
                                                          {
                                                          },
                                    new DvArgument[]
                                                          {
                                                              new DvArgument("Source",
                                                                             SourceProtocolInfo,
                                                                             ArgumentDirection.Out),
                                                              new DvArgument("Sink",
                                                                             SinkProtocolInfo,
                                                                             ArgumentDirection.Out), 
                                                          });
      AddAction(getProtocolInfoAction);

      DvAction getCurrentConnectionIDsAction = new DvAction("GetCurrentConnectionIDs", OnGetCurrentConnectionIDs,
                                    new DvArgument[]
                                                          {
                                                          },
                                    new DvArgument[]
                                                          {
                                                              new DvArgument("ConnectionIDs",
                                                                             CurrentConnectionIDs,
                                                                             ArgumentDirection.Out),
                                                          });
      AddAction(getCurrentConnectionIDsAction);

      DvAction getCurrentConnectionInfoAction = new DvAction("GetCurrentConnectionInfo", OnGetCurrentConnectionInfo,
                                    new DvArgument[]
                                                          {
                                                              new DvArgument("ConnectionID",
                                                                             A_ARG_TYPE_ConnectionID,
                                                                             ArgumentDirection.In),
                                                          },
                                    new DvArgument[]
                                                          {
                                                              new DvArgument("RcsID",
                                                                             A_ARG_TYPE_RcsID,
                                                                             ArgumentDirection.Out),
                                                              new DvArgument("AVTransportID",
                                                                             A_ARG_TYPE_AVTransportID,
                                                                             ArgumentDirection.Out),
                                                              new DvArgument("ProtocolInfo",
                                                                             A_ARG_TYPE_ProtocolInfo,
                                                                             ArgumentDirection.Out),
                                                              new DvArgument("PeerConnectionManager",
                                                                             A_ARG_TYPE_ConnectionManager,
                                                                             ArgumentDirection.Out),
                                                              new DvArgument("PeerConnectionID",
                                                                             A_ARG_TYPE_ConnectionID,
                                                                             ArgumentDirection.Out),
                                                              new DvArgument("Direction",
                                                                             A_ARG_TYPE_Direction,
                                                                             ArgumentDirection.Out),
                                                              new DvArgument("Status",
                                                                             A_ARG_TYPE_ConnectionStatus,
                                                                             ArgumentDirection.Out),

                                                          });
      AddAction(getCurrentConnectionInfoAction);

    }

    static UPnPError OnGetProtocolInfo(DvAction action, IList<object> inParams, out IList<object> outParams, CallContext context)
    {
        string source = "";
        string sink = "";

        source =
            "http-get:*:audio/L16:*,http-get:*:audio/wav:*,http-get:*:audio/mpeg:*,http-get:*:audio/x-ms-wma:*,http-get:*:audio/L8:*,http-get:*:video/avi:*,http-get:*:video/mpeg:*,http-get:*:video/x-ms-wmv:*,http-get:*:video/x-ms-asf:*,http-get:*:video/x-ms-dvr:*,http-get:*:image/bmp:*,http-get:*:image/gif:*,http-get:*:image/jpeg:*,http-get:*:image/png:*,http-get:*:image/tiff:*,http-get:*:image/x-ycbcr-yuv420:*";
        outParams = new List<object>() {source, sink};
        return null;
    }

    static UPnPError OnGetCurrentConnectionIDs(DvAction action, IList<object> inParams, out IList<object> outParams, CallContext context)
    {
      outParams = null;
      return null;
    }

    static UPnPError OnGetCurrentConnectionInfo(DvAction action, IList<object> inParams, out IList<object> outParams, CallContext context)
    {
      outParams = null;
      return null;
    }

    internal static ILogger Logger
    {
        get { return ServiceRegistration.Get<ILogger>(); }
    }
  }
}
