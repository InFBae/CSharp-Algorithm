using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03.Iterator
{ 
    internal class List<T> : IEnumerable<T>
    {
        private const int DefaultCapacity = 4;
        private T[] items;
        private int size;
        
        public int Count { get { return size; } }
        public int Capacity { get { return items.Length; } }

        public List()
        {
            this.items = new T[DefaultCapacity];
            this.size = 0;
        }

        public T this[int index]
        {
            get 
            {
                if (index < 0 || index >= size)
                {
                    throw new IndexOutOfRangeException();
                }
                return items[index]; }
            set 
            {
                if (index < 0 || index >= size)
                {
                    throw new IndexOutOfRangeException();
                }
                items[index] = value; 
            }
        }

        public void Add(T item)
        {
            if(size < items.Length) 
            {
                items[this.size++] = item;
            }
            else
            {
                Grow();
                items[this.size++] = item;
            }
            
        }

        public bool Remove(T item)
        {
            int index = IndexOf(item);
            if(index >= 0)
            {
                RemoveAt(index);
                return true;
            }
            else { return  false; }
        }

        public void RemoveAt(int index)
        {
            if(index < 0 || index >= size)
                throw new IndexOutOfRangeException();

            size--;
            Array.Copy(items, index + 1, items, index, size - index);
        }
        public int IndexOf(T item)
        {
            return Array.IndexOf(items, item, 0, size);
        }

        public T? Find(Predicate<T> match)
        {
            if(match == null)
                throw new ArgumentNullException("match");

            for(int i =0; i < size; i++)
            {
                if (match(items[i]))
                    return items[i];
            }
            return default(T);
        }

        public int FindIndex(Predicate<T> match)
        {
            for(int i = 0; i < size; i++)
            {
                if (match(items[i]))
                    return i;
            }
            return -1;
        }
        private void Grow()
        {
            int newCapacity = items.Length * 2;
            T[] newItems = new T[newCapacity];
            Array.Copy(items, 0, newItems, 0, size);
            items = newItems;
        }
        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enumerator(this);
        }

        public struct Enumerator : IEnumerator<T>
        {
            private List<T> list;
            private int index;
            private T current;

            internal Enumerator(List<T> list)
            {
                this.list = list;
                this.index = 0;
                this.current = default(T);
            }

            public T Current { get { return current; } }

            object IEnumerator.Current
            {
                get
                {
                    if (index < 0 || index >= list.Count)
                        throw new InvalidOperationException();
                    return Current;
                }
            }

            public void Dispose() { }

            public bool MoveNext()
            {
                if (index < list.Count)
                {
                    current = list[index++];
                    return true;
                }
                else
                {
                    current = default(T);
                    index = list.Count;
                    return false;
                }
            }

            public void Reset()
            {
                index = 0;
                current = default(T);
            }
        }
    }
}