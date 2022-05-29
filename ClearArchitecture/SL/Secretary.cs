using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace ClearArchitecture.SL
{
    public class Secretary<T> : ISecretary<T>
    {
        private readonly ConcurrentDictionary<string, T> subscribers = new();

        public void Remove(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return;
            }

            subscribers.Remove(key, out T old);
        }

        public int Size()
        {
            return subscribers.Count;
        }

        public void Put(string key, T value)
        {
            if (string.IsNullOrEmpty(key))
            {
                return;
            }

            if (value == null)
            {
                return;
            }

            subscribers.AddOrUpdate(key, value, (key, oldValue) => oldValue);
        }

        public bool ContainsKey(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return false;
            }

            return subscribers.ContainsKey(key);
        }

        public T GetValue(string key)
        {
            T value = default;

            if (string.IsNullOrEmpty(key))
            {
                return value;
            }

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

        public bool IsEmpty()
        {
            return (subscribers.Count == 0);
        }

        public void Clear()
        {
            subscribers.Clear();
        }

        public List<string> Keys()
        {
            return subscribers.Keys.ToList();
        }
    }
}