using System;
using System.Collections.Generic;
using MediaPortal.Backend.MediaLibrary;
using MediaPortal.Common.MediaManagement;

namespace MediaPortal.Extensions.MediaServer.Objects.MediaLibrary
{
    public static class Extensions
    {
        public static MediaItem GetMediaItem(this IMediaLibrary mediaLibrary, Guid mediaItemId, IEnumerable<Guid> necessaryMIATypes, IEnumerable<Guid> optionalMIATypes)
        {
            var items = mediaLibrary.LoadCustomPlaylist(new List<Guid>() {mediaItemId}, necessaryMIATypes,
                                                        optionalMIATypes);
            return items.Count > 0 ? items[0] : null;
        }
    }
}
