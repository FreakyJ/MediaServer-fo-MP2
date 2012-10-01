using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediaPortal.Common.MediaManagement;
using MediaPortal.Common.MediaManagement.DefaultItemAspects;
using MediaPortal.Extensions.MediaServer.Objects.MediaLibrary;

namespace MediaPortal.Extensions.MediaServer.DLNA
{
    public class DlnaProtocolInfoFactory
    {
        public DlnaProtocolInfo GetProtocolInfo(MediaItem item)
        {
            var info = new DlnaProtocolInfo
            {
                Protocol = "http-get",
                Network = "*",
                MediaType = MediaLibraryHelper.GetOrGuessMimeType(item),
                AdditionalInfo = new DlnaForthField()
            };
            
            ConfigureProfile(info.AdditionalInfo, item, info.MediaType);
            return info;
        }
      
        private static string ConfigureProfile(DlnaForthField dlnaField, MediaItem item, string mediaType)
        {
            //TODO: much better type resolution        
            switch(mediaType)
            {
                case MediaLibraryHelper.MIMETYPE_AUDIO:
                    dlnaField.ProfileParameter.ProfileName = DlnaProfiles.Mp3;
                    dlnaField.FlagsParameter.StreamingMode = true;
                    dlnaField.FlagsParameter.InteractiveMode = false;
                    dlnaField.FlagsParameter.BackgroundMode = true;
                    break;
                case MediaLibraryHelper.MIMETYPE_VIDEO:
                    dlnaField.ProfileParameter.ProfileName = DlnaProfiles.MpegPsPal;
                    dlnaField.FlagsParameter.StreamingMode = true;
                    dlnaField.FlagsParameter.InteractiveMode = false;
                    dlnaField.FlagsParameter.BackgroundMode = true;
                    break;
                case MediaLibraryHelper.MIMETYPE_IMAGE:
                    dlnaField.ProfileParameter.ProfileName = DlnaProfiles.JpegLarge;
                    dlnaField.FlagsParameter.StreamingMode = false;
                    dlnaField.FlagsParameter.InteractiveMode = true;
                    dlnaField.FlagsParameter.BackgroundMode = true;
                    break;
            }
            return null;
        }

        public static DlnaProtocolInfo GetProfileInfo(MediaItem item)
        {
            var factory = new DlnaProtocolInfoFactory();
            return factory.GetProtocolInfo(item);
        }
    }
}
