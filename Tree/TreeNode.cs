using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaPortal.Extensions.MediaServer.Tree
{
    public class TreeNode<T>
    {
        public string Key { get; protected set; }
        public T Value { get; protected set; }
        public TreeNode<T> Parent { get; protected set; }

        private List<TreeNode<T>> _children;

        public List<TreeNode<T>> Children 
        { 
            get { return _children ?? (_children = new List<TreeNode<T>>()); }
        }

        public TreeNode(string key, T value)
        {
            Key = key;
            Value = value;
            Parent = null;            
        }

        public void Add(TreeNode<T> node)
        {
            node.Parent = this;
            if (!Children.Contains(node))
            {
                Children.Add(node);
            }
        }

 //       public TreeNode<T> FindNode(string key)
 //       {
 //           return Key == key ? this : Children.FindNode(key);
 //       }

        public virtual TreeNode<T> FindNode(string key)
        {
            return Key == key ? this : Children.Select(node => node.FindNode(key)).FirstOrDefault(n => n != null);                
        }      
    }
}
