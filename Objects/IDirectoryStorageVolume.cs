﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaPortal.Extensions.MediaServer.Objects
{
    /// <summary>
    /// A ‘storageVolume’ instance represents all, or a partition of, some physical storage unit of a single type (as indicated by the ‘storageMedium’ property). The storage volume may be writable or not, indicating whether new items can be created as children of the volume. A storageVolume may contain other object, except a ‘storageSystem’ or another ‘storageVolume’. A ‘storageVolume’ may only be a child of the root container or a ‘storageSystem’ container. Examples of ‘storageVolume’ instances are
    ///   a Hard Disk Drive
    ///   a partition on a Hard Disk Drive
    ///   a CD-Audio disc
    ///   a Flash memory card
    /// </summary>
    public interface IDirectoryStorageVolume : IDirectoryContainer
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
        /// Indicates the type of storage medium used for the content. Potentially useful for user-interface purposes.
        /// </summary>
        [DirectoryProperty("upnp:storageMedium")]
        string StorageMedium { get; set; }
    }
}
