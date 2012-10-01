using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaPortal.Extensions.MediaServer.Objects
{
    /// <summary>
    /// A ‘storageSystem’ instance represents a potentially heterogeneous collection of storage media. A ‘storageSystem’ may contain other objects, including all types of storage containers. A ‘storageSystem’ may only be a child of the root container or another ‘storageSystem’ container. Examples of ‘storageSystem’ instances are
    /// a CD Jukebox
    /// a Hard Disk Drive plus a CD in a combo device
    /// a single CD
    /// </summary>
    public interface IDirectoryStorageSystem : IDirectoryContainer
    {
        /// <summary>
        /// Total capacity, in bytes, of the storage represented by the container. Value –1 is reserved to indicate that the capacity is ‘unknown’.
        /// </summary>
        [DirectoryProperty("upnp:storageTotal")]
        long StorageTotal { get; set; }

        /// <summary>
        /// Combined space, in bytes, used by all the objects held in the storage represented by the container. Value –1 is reserved to indicate that the space is ‘unknown’.
        /// </summary>
        [DirectoryProperty("upnp:storageUsed")]
        long StorageUsed { get; set; }

        /// <summary>
        /// Total free capacity, in bytes, of the storage represented by the containe. Value –1 is reserved to indicate that the capacity is ‘unknown’.
        /// </summary>
        [DirectoryProperty("upnp:storageFree")]
        long StorageFree { get; set; }

        /// <summary>
        /// Largest amount of space, in bytes, available for storing a single resource in the container. Value –1 is reserved to indicate that the amount of space is ‘unknown’.
        /// </summary>
        [DirectoryProperty("upnp:storageMaxPartition")]
        long StorageMaxPartition { get; set; }

        /// <summary>
        /// Indicates the type of storage medium used for the content. Potentially useful for user-interface purposes.
        /// </summary>
        [DirectoryProperty("upnp:storageMedium")]
        string StorageMedium { get; set; }
    }
}
