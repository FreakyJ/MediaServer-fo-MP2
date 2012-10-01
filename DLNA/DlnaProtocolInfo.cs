using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaPortal.Extensions.MediaServer.DLNA
{
    public class DlnaProtocolInfo
    {
        public DlnaProtocolInfo()
        {
            Protocol = "http-get";
            Network = "*";            
        }

        public string Protocol { get; set; }
        
        public string Network { get; set; }
        
        public string MediaType { get; set; }

        public DlnaForthField AdditionalInfo { get; set; }

        public override string ToString()
        {
            return Protocol + ":" + Network + ":" + MediaType + ":" + AdditionalInfo;
        }
    }
}
