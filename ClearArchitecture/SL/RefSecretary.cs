using ClearArchitecture.SL;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace ClearArchitecture.SL
{
    class RefSecretary<T> : ISecretary<T>
    {
        private readonly ConcurrentDictionary<String, WeakReference> subscribers = new() ;

        public void Clear()
        {
            subscribers.Clear();
        }

        public bool ContainsKey(string key)
        {
            if (String.IsNullOrEmpty(key))
            {
                return false;
            }

            return subscribers.ContainsKey(key);
        }

        public T GetValue(string key)
        {
            T val = default(T);

            if (String.IsNullOrEmpty(key))
            {
                return val;
            }

            if (ContainsKey(key))
            {
                bool ret = subscribers.TryGetValue(key, out WeakReference value);
                if (ret && value.IsAlive)
                {
                    val = (T)value.Target;
                }
            }
            return val;
        }

        public bool IsEmpty()
        {
            return subscribers.Count == 0;
        }

        public List<string> Keys()
        {
            return subscribers.Keys.ToList();
        }

        public void Put(string key, T value)
        {
            if (String.IsNullOrEmpty(key))
            {
                return;
            }

            if (value == null)
            {
                return;
            }

            subscribers.AddOrUpdate(key, new WeakReference(value, false), (key, oldValue) => oldValue);
        }

        public void Remove(string key)
        {
            if (String.IsNullOrEmpty(key))
            {
                return;
            }

            subscribers.Remove(key, out WeakReference old);
        }

        public int Size()
        {
            return subscribers.Count;
        }

        public List<T> Values()
        {
            List<T> list = new();
            foreach (WeakReference r in subscribers.Values)
            {
                if (r.IsAlive)
                {
                    list.Add((T)r.Target);
                }
            }
            return list;
        }
    }
}
