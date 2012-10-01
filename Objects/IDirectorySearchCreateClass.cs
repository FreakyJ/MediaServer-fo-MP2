using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaPortal.Extensions.MediaServer.Objects
{
    public interface IDirectorySearchClass
    {
        [DirectoryProperty("")]
        string Class { get; set; }

        [DirectoryProperty("@includeDerived")]
        bool IncludeDerived { get; set; }

        [DirectoryProperty("@name", Required = false)]
        string Name { get; set; }
    }

    public interface IDirectoryCreateClass : IDirectorySearchClass
    {
        
    }
}
