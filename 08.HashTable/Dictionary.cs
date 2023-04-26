using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure
{
    internal class Dictionary<TKey, TValue> where TKey : IEquatable<TKey>
    {
        private const int DefaultCapacity = 1000;

        private struct Entry
        {
            public enum State { None, Using, Deleted}

            public State state;
            public int hashCode;

            public TKey key;
            public TValue value;
        }

        private Entry[] table;
        

        public Dictionary()
        {
            this.table = new Entry[DefaultCapacity];
        }
        
        public TValue this[TKey key]
        {
            get
            {
                // 1. key를 index로 해싱
                int index = Math.Abs(key.GetHashCode() % table.Length);

                // 2. key가 일치하는 데이터가 나올 때까지 다음으로 이동
                while (table[index].state == Entry.State.Using)
                {
                    if (key.Equals(table[index].key))
                    {
                        return table[index].value;
                    }
                    if(table[index].state == Entry.State.None)
                    {
                        break;
                    }

                    index = ++index % table.Length;
                }
                throw new InvalidOperationException();
            }
            set
            {
                // 1. key를 index로 해싱
                int index = Math.Abs(key.GetHashCode() % table.Length);

                // 2. key가 일치하는 데이터가 나올 때까지 다음으로 이동
                while (table[index].state == Entry.State.Using)
                {
                    if (key.Equals(table[index].key))
                    {
                        table[index].value = value;
                    }
                    if (table[index].state == Entry.State.None)
                    {
                        break;
                    }

                    index = index < table.Length - 1 ? index + 1 : 0;
                }
                throw new InvalidOperationException();
            }
        }

        public void Add(TKey key, TValue value)
        {
            // 1. key를 index로 해싱
            int index = Math.Abs(key.GetHashCode() % table.Length);

            // 2. 사용중이 아닌 index까지 다음으로 이동
            while (table[index].state == Entry.State.Using)
            {
                if (key.Equals(table[index].key))
                {
                    throw new InvalidOperationException();
                }
                index = index < table.Length - 1 ? index + 1 : 0;
            }

            // 3. 사용중이 아닌 index를 발견한 경우 그 위치에 저장
            table[index].hashCode = key.GetHashCode();
            table[index].key = key;
            table[index].value = value;
            table[index].state = Entry.State.Using;
        }

        public void Remove(TKey key)
        {
            // 1. key를 index로 해싱
            int index = Math.Abs(key.GetHashCode() % table.Length);

            // 2. key가 일치하는 데이터가 나올 때까지 다음으로 이동
            while (table[index].state == Entry.State.Using)
            {
                if (key.Equals(table[index].key))
                {
                    table[index].state = Entry.State.Deleted;
                }
                if (table[index].state != Entry.State.Using)
                {
                    break;
                }

                index = index < table.Length - 1 ? index + 1 : 0;
            }
            throw new InvalidOperationException();
        }
    }
    }
}
