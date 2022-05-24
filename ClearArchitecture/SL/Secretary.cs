using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace ClearArchitecture.SL
{
    public class Secretary<T> : ISecretary<T>
    {
        private readonly ConcurrentDictionary<String, T> subscribers = new ConcurrentDictionary<String, T>();

        public void Remove(String key)
        {
            if (String.IsNullOrEmpty(key))
            {
                return;
            }

            subscribers.Remove(key, out T old);
        }

        public int Size()
        {
            return subscribers.Count;
        }

        public void Put(String key, T value)
        {
            if (String.IsNullOrEmpty(key))
            {
                return;
            }

            if (value == null)
            {
                return;
            }

            subscribers.AddOrUpdate(key, value, (key, oldValue) => oldValue);
        }

        public Boolean ContainsKey(String key)
        {
            if (String.IsNullOrEmpty(key))
            {
                return false;
            }

            return subscribers.ContainsKey(key);
        }

        public T GetValue(String key)
        {
            T value = default(T);
            if (ContainsKey(key))
            {
                subscribers.TryGetValue(key, out value);
            }
            return value;
        }

        public List<T> Values()
        {
            return subscribers.Values.ToList();
        }

        public Boolean IsEmpty()
        {
            return (subscribers.Count == 0);
        }

        public void Clear()
        {
            subscribers.Clear();
        }

        public List<String> Keys()
        {
            return subscribers.Keys.ToList();
        }
    }
}