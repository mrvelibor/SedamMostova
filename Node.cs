using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenBridgesTree
{
    class Node
    {
        public Node Parent { get; set; }

        private Node _left;
        public Node Left
        {
            get { return _left; }
            set
            {
                _left = value;
                if (_left != null)
                {
                    _left.Parent = this;
                }
            }
        }

        private Node _right;
        public Node Right
        {
            get { return _right; }
            set
            {
                _right = value;
                if (_right != null)
                {
                    _right.Parent = this;
                }
            }
        }

        public int Value { get; set; }

        public bool IsLeaf
        {
            get { return _left == null && _right == null; }
        }

        public bool IsFull
        {
            get { return _left != null && _right != null; }
        }

        public Node(int value, Node parent = null)
        {
            Value = value;
            parent?.Add(this);
        }

        private void Add(Node node)
        {
            if (Left == null)
            {
                Left = node;
            }
            else if (Right == null)
            {
                Right = node;
            }
        }

        public void PrintLeaves()
        {
            if (IsLeaf)
            {
                Console.Write(Value + " ");
            }
            else
            {
                Left?.PrintLeaves();
                Right?.PrintLeaves();
            }
        }
    }
}
