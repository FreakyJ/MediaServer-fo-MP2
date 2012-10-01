using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaPortal.Extensions.MediaServer.Tree
{
    public class TreeNodeList<T> : List<TreeNode<T>>
    {        
        public TreeNode<T> FindNode(string key)
        {
            return this.Select(node => node.FindNode(key)).FirstOrDefault(n => n != null);
        }        
    }
}
