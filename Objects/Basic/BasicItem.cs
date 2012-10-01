using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediaPortal.Extensions.MediaServer.Tree;

namespace MediaPortal.Extensions.MediaServer.Objects.Basic
{
    public class BasicItem : BasicObject, IDirectoryItem
    {
        public BasicItem(string key) : base(key)
        {

        }
        
        public override string Class
        {
            get { return "object.item"; }
        }

        public virtual string RefId { get; set; }

        public override void Initialise()
        {
            
        }
    }
}
