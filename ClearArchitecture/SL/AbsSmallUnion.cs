
using System;
using System.Collections.Generic;

namespace ClearArchitecture.SL
{
    public abstract class AbsSmallUnion<T> : AbsProvider, ISmallUnion<T> where T : IProviderSubscriber
    {
        private readonly ISecretary<T> secretary = CreateSecretary();

        public override abstract int CompareTo(IProvider other);

        public override abstract string GetName();

        public static ISecretary<T> CreateSecretary()
        {
            return new Secretary<T>();
        }

        public bool Contains(T subscriber)
        {
            if (subscriber == null)
            {
                return false;
            }

            return secretary.ContainsKey(subscriber.GetName());
        }

        public List<T> GetReadySubscribers()
        {
            List<T> subscribers = new();
            foreach (T subscriber in GetSubscribers())
            {
                if (subscriber.IsValid() && subscriber is ILifecycle)
                {
                    int state = (subscriber as ILifecycle).GetState();
                    if (state == Lifecycle.VIEW_READY)
                    {
                        subscribers.Add(subscriber);
                    }
                }
            }
            return subscribers;
        }
        
        public T GetSubscriber(string name)
        {
            if (!secretary.ContainsKey(name))
            {
                return default;
            }
            else
            {
                return secretary.GetValue(name);
            }
        }

        public List<T> GetSubscribers()
        {
            return secretary.Values();
        }

        public List<T> GetValidatedSubscribers()
        {
            List<T> subscribers = new();
            foreach(T subscriber in GetSubscribers())
            {
                if (subscriber.IsValid())
                {
                    subscribers.Add(subscriber);
                }
            }
            return subscribers;
        }

        public bool HasSubscriber(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                return false;
            }

            return secretary.ContainsKey(name);
        }

        public bool HasSubscribers()
        {
            return !secretary.IsEmpty();

        }

        public void OnAddSubscriber(T subscriber)
        {
            // Method intentionally left empty.
        }

        public void OnRegisterFirstSubscriber()
        {
            // Method intentionally left empty.
        }
        public void OnUnRegisterLastSubscriber()
        {
            // Method intentionally left empty.
        }

        public bool Register(T subscriber) 
        {
            if (subscriber == null)
            {
                return false;
            }

            if (!subscriber.IsValid())
            {
                return false;
            }

            int cnt = secretary.Size();

            secretary.Put(subscriber.GetName(), subscriber);

            if (cnt == 0 && secretary.Size() == 1)
            {
                OnRegisterFirstSubscriber();
            }

            OnAddSubscriber(subscriber);

            return true;
        }

        public void Unregister(T subscriber)
        {
            if (subscriber == null)
            {
                return;
            }

            int cnt = secretary.Size();
            if (secretary.ContainsKey(subscriber.GetName()) && (subscriber.GetType() == secretary.GetValue(subscriber.GetName()).GetType()))
            {
                secretary.Remove(subscriber.GetName());
            }

            if (cnt == 1 && secretary.Size() == 0)
            {
                OnUnRegisterLastSubscriber();
            }
        }

        public void Unregister(String name)
        {
            if (String.IsNullOrEmpty(name))
            {
                return;
            }

            if (HasSubscriber(name))
            {
                T subscriber = GetSubscriber(name);
                Unregister(subscriber);
            }
        }

         public void Stop()
         {
            foreach (T subscriber in GetSubscribers())
            {
                Unregister(subscriber);
                subscriber.OnStopProvider(this);
            }
            secretary.Clear();
         }
    }
}
