using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure
{
    internal class BinarySearchTree<T> where T : IComparable<T>
    {
        private Node root;

        public BinarySearchTree()
        {
            this.root = null;
        }

        public void Add(T item)
        {
            Node newNode = new Node(item, null, null, null);

            if (root == null)
            {
                root = newNode;
                return;
            }
            Node current = root;
            while (current != null)
            {
                if (item.CompareTo(current.item) < 0)
                {
                    if (current.left != null)
                    {
                        current = current.left;
                    }
                    else
                    {
                        current.left = newNode;
                        newNode.parent = current;
                        return;
                    }
                }
                else if (item.CompareTo(current.item) > 0)
                {
                    if (current.right != null)
                    {
                        current = current.right;
                    }
                    else
                    {
                        current.right = newNode;
                        newNode.parent = current;
                        return;
                    }
                }
                else
                {
                    return;
                }
            }
        }

        public bool TryGetValue(T item, out T value)
        {
            Node findNode = FindNode(item);
            if (findNode == default)
            {
                value = default(T);
                return false;
            }
            else
            {
                value = findNode.item;
                return true;
            }
        }
        public Node FindNode(T item)
        {
            if (root == null)
            {
                return default;
            }
            Node current = root;

            while (current != null)
            {
                if (item.CompareTo(current.item) < 0)
                {
                    if (current.left == null)
                    {
                        return default;
                    }
                    else
                    {
                        current = current.left;
                    }
                }
                else if (item.CompareTo(current.item) > 0)
                {
                    if (current.right == null)
                    {
                        return default;
                    }
                    else
                    {
                        current = current.right;
                    }
                }
                else
                {
                    return current;
                }
            }
            return default;
        }
    
        public bool Remove(T item)
        {
            Node findNode = FindNode(item);
            if(findNode == default)
            {
                return false;
            }
            EraseNode(findNode);
            return true;
        }

        private void EraseNode(Node node)
        {
            // 자식이 없을 떄
            if (node.HasNoChild)     
            {
                if (node.IsLeftChild)
                {
                    node.parent.left = null;
                }
                else if(node.IsRightChild)
                {
                    node.parent.right = null;
                }
                else
                {
                    root = null;
                }
            }
            // 자식을 하나만 가질 때
            else if (node.HasLeftChild || node.HasRightChild)    
            {
                Node parent = node.parent;
                Node child = node.HasLeftChild ? node.left : node.right;

                if (node.IsLeftChild)
                {
                    parent.left = child;
                    child = parent.left;
                }else if(node.IsRightChild)
                {
                    parent.right = child;
                    child.parent = parent;
                }
                else
                {
                    root = child;
                    child.parent = null;
                }
            }
            // 자식을 둘 모두 가질 때
            else
            {
                Node replaceNode = node.left;
                while(replaceNode.right != null)
                {
                    replaceNode = replaceNode.right;
                }
                node.item = replaceNode.item;
                EraseNode(replaceNode);
            }
        }
   

        public void Print()
        {
            Print(root);
        }
        public void Print(Node node)
        {
            if(node.left != null)
            {
                Print(node.left);
            }
            Console.WriteLine(node.item);
            if (node.right != null)
            {
                Print(node.right);
            }
                     
        }
        public class Node
        {
            internal T item;
            internal Node parent;
            internal Node left;
            internal Node right;

            public Node(T item)
            {
                this.item = item;
            }
            public Node(T item, Node parent, Node left, Node right) 
            { 
                this.item = item;
                this.parent = parent;
                this.left = left;
                this.right = right;
            }

            public bool IsRootNode { get { return parent == null; } }
            public bool IsLeftChild { get { return parent != null && parent.left == this; } }
            public bool IsRightChild { get { return parent != null && parent.right == this; } }

            public bool HasNoChild { get { return left == null && right == null; } }
            public bool HasLeftChild { get { return left != null && right == null; } }
            public bool HasRightChild { get { return left == null && right != null; } }
            public bool HasBothChild { get { return left != null && right != null; } }

        }

    }

}
