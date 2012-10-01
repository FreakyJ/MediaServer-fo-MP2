using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediaPortal.Extensions.MediaServer.DIDL;
using MediaPortal.Extensions.MediaServer.Tree;
using MediaPortal.Utilities.Exceptions;

namespace MediaPortal.Extensions.MediaServer.Objects.Basic
{
    public class BasicContainer : BasicItem, IDirectoryContainer
    {
        public BasicContainer(string id) : base(id)
        {
            Restricted = true;
            Searchable = false;
            SearchClass = new List<IDirectorySearchClass>();
            CreateClass = new List<IDirectoryCreateClass>();
        }

        public override string Class
        {
            get { return "object.container"; }
        }

        public override void Initialise()
        {
            
        }

        public void InitialiseAll()
        {
            Initialise();
            foreach (var treeNode in Children.OfType<BasicContainer>())
            {
                (treeNode).InitialiseAll();
            }
        }

        public virtual IList<IDirectorySearchClass> SearchClass { get; set; }

        public virtual bool Searchable { get; set; }

        public virtual int ChildCount
        {
            get { return Children.Count; }
            set { throw new IllegalCallException("Meaningless in this implementation"); }
        }

        public virtual IList<IDirectoryCreateClass> CreateClass { get; set; }

        

    }
}
