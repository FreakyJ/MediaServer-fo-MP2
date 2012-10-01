using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediaPortal.Extensions.MediaServer.Objects.Basic;

namespace MediaPortal.Extensions.MediaServer.Objects.MediaLibrary
{
    class MediaLibraryGenreContainer : BasicContainer
    {
        protected Guid ObjectId { get; set; }
        protected string BaseKey { get; set; }

        public MediaLibraryGenreContainer(string id) : base(id)
        {
            var split = id.IndexOf(':');
            BaseKey = MediaLibraryHelper.GetBaseKey(id);
            ObjectId = MediaLibraryHelper.GetObjectId(id);
        }



    }
}
