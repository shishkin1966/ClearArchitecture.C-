using System;
using System.Collections.Generic;
using System.Text;

namespace ClearArchitecture.SL
{
    public class Secretary<T> : ISecretary<T>
    {
        private ConcurrentDictionary<String, T> subscribers = new ConcurrentDictionary<String, T>();

        override void remove(String key)
        {
            subscribers.Remove(key)
        }

        override int size()
        {
            return subscribers.Count
        }

        override void put(String key, T value)
        {
            subscribers.Remove(key)
            subscribers.Add(key, value)
        }

        override Boolean containsKey(String key)
        {
            return subscribers.ContainsKey(key)
        }

        override T? getValue(String key)
        {
            T value = null
            if (containsKey(key))
            {
                subscribers.TryGetValue(key, value)
            }
            return value
        }

        override List<T> values()
        {
            return subscribers.Values.ToList()
        }

        override Boolean isEmpty()
        {
            return (subscribers.Count == 0)
        }

        override void clear()
        {
            subscribers.Clear()
        }

        override List<String> keys()
        {
            return subscribers.Keys.ToList()
        }
    }
}