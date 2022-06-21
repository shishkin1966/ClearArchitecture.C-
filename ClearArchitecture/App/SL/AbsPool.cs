using System.Collections.Concurrent;
using System.Collections.Generic;

namespace ClearArchitecture.SL
{
    public abstract class AbsPool<T> : AbsProvider, IPool<T> where T : new()
    {
        private readonly ConcurrentBag<T> items = new();
        private readonly int capacity;

        public int Capacity
        {
            get
            {
                return capacity;
            }
        }

        public int Count
        {
            get
            {
                return items.Count;
            }
        }


        protected AbsPool(string name, int capacity) : base(name)
        {
            this.capacity = capacity;
        }

        public abstract T ObjectFactory();

        public List<T> Get(int count)
        {
            List<T> list = new();
            if (count < 1) return list;

            do
            {
                if (items.TryTake(out T item))
                {
                    list.Add(item);
                }
                else
                {
                    if (items.Count < capacity)
                    { 
                        Release(ObjectFactory());
                    }
                    else
                    {
                        break;
                    }
                }

            } while (list.Count < count);
            return list;
        }

        public void Release(T item)
        {
            if (items.Count < capacity)
            {
                items.Add(item);
            }
        }

        public void Release(List<T> items)
        {
            int i = 0;
            while  (this.items.Count < capacity) { 
                if (i < items.Count)
                {
                    this.items.Add(items[i]);
                    i++;
                }
                else
                {
                    break;
                }
            }
        }
    }
}
