using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure
{

    public class LinkedListNode<T>
    {
        internal LinkedList<T>? list;
        internal LinkedListNode<T>? prev;
        internal LinkedListNode<T>? next;
        internal T item;

        public LinkedListNode<T> Prev { get { return prev; } }
        public LinkedListNode<T> Next { get { return next; } }

        public T Value { get { return item; } set { item = value; } }

        public LinkedListNode(T value)
        {
            this.list = null;
            this.prev = null;
            this.next = null;
            this.item = value;
        }
        public LinkedListNode(LinkedList<T> list, T value)
        {
            this.list = list;
            this.prev = null;
            this.next = null;
            this.item = value;
        }
        public LinkedListNode(LinkedList<T> list, LinkedListNode<T> prev, LinkedListNode<T> next, T value)
        {
            this.list = list;
            this.prev = prev;
            this.next = next;
            this.item = value;
        }
    }
    public class LinkedList<T>
    {
        private LinkedListNode<T>? head;
        private LinkedListNode<T>? tail;
        private int count;

        public LinkedList()
        {
            head = null;
            tail = null;
            count = 0;
        }

        public LinkedListNode<T> First { get { return head; } }
        public LinkedListNode<T> Last { get { return tail; } }
        public T Item { get { return Item; } }
        public int Count { get { return count; } }

        public LinkedListNode<T> AddFirst(T value)
        {
            LinkedListNode<T> newNode = new LinkedListNode<T>(this, value);
            if(head != null)
            {
                newNode.next = head;
                head.prev = newNode;
            }
            else
            {
                head = newNode;
                tail = newNode;
            }
            head = newNode;
            count++;
            return newNode;
        }

        public LinkedListNode<T> AddLast(T value)
        {
            LinkedListNode<T> newNode = new LinkedListNode<T> (this, value);
            if(tail != null)
            {
                tail.next = newNode;
                newNode.prev = tail;
            }
            else
            {
                head = newNode;
                tail = newNode;
            }
            tail = newNode;
            count++;
            return newNode;
        }

        public void Remove(LinkedListNode<T> node)
        {
            if(node.list != this) throw new InvalidOperationException();
            if(node == null) throw new ArgumentNullException(nameof(node));

            if (head == node)
                head = node.next;
            if (tail == node)
                tail = node.prev;

            if(node.prev != null)
            {
                node.prev.next = node.next;
            }
            if(node.next != null)
            {
                node.next.prev = node.prev;
            }
            count--;
        }

        public bool Remove(T value)
        {
            LinkedListNode<T> findNode;
            findNode = Find(value);
            if (findNode != null)
            {
                Remove(findNode);
                return true;
            }
            else
            {
                return false;
            }
        }

        public LinkedListNode<T> Find(T value)
        {
            LinkedListNode<T> target = head;
            EqualityComparer<T> comparer = EqualityComparer<T>.Default;

            while(target != null)
            {
                if (comparer.Equals(target.Value, value))
                {
                    return target;
                }
                else
                {
                    target = target.next;
                }
            }

            return null;
        }

        public LinkedListNode<T> AddBefore(LinkedListNode<T> node, T value)
        {
            if (node.list != this) throw new InvalidOperationException();
            if (node == null) throw new ArgumentNullException(nameof(node));

            LinkedListNode<T> newNode = new LinkedListNode<T>(this, value);

            newNode.next = node;
            newNode.prev = node.prev;
            node.prev = newNode;
            if(node.prev != null)
            {
                node.prev.next = newNode;
            }
            else
            {
                head = newNode;
            }
            count++;

            return newNode;
        }

    }
}
