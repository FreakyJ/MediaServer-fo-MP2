using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediaPortal.Common.MediaManagement;
using MediaPortal.Common.MediaManagement.DefaultItemAspects;
using MediaPortal.Extensions.MediaServer.Objects.Basic;

namespace MediaPortal.Extensions.MediaServer.Objects.MediaLibrary
{
    public class MediaLibraryItem : BasicItem
    {
        public MediaItem Item { get; protected set; }

        public MediaLibraryItem(string baseKey, MediaItem item)
            : base(baseKey + ":" + item.MediaItemId)
        {
            Item = item;
        }

        public override void Initialise()
        {
            Title = Item.Aspects[MediaAspect.ASPECT_ID].GetAttributeValue(MediaAspect.ATTR_TITLE).ToString();
        }
    }
}
