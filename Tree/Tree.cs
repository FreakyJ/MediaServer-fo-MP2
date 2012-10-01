using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaPortal.Extensions.MediaServer.Tree
{
    public class Tree<T> : TreeNode<T>
    {
        public Tree(string key, T value) : base(key, value)
        {
            
        }
    }
}
