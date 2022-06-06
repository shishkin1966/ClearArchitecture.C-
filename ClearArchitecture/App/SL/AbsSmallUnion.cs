﻿
using System.Collections.Generic;
using System.Linq;

namespace ClearArchitecture.SL
{
    public abstract class AbsSmallUnion : AbsProvider, ISmallUnion 
    {
        private readonly ISecretary<IProviderSubscriber> secretary = CreateSecretary();

        public override abstract int CompareTo(IProvider other);

        public override abstract string GetName();

        public static ISecretary<IProviderSubscriber> CreateSecretary()
        {
            return new Secretary<IProviderSubscriber>();
        }

        public bool ContainsSubscriber(IProviderSubscriber subscriber)
        {
            if (subscriber == null)
            {
                return false;
            }

            return secretary.ContainsKey(subscriber.GetName());
        }

        public List<IProviderSubscriber> GetReadySubscribers()
        {
            List<IProviderSubscriber> subscribers = new();
            foreach (IProviderSubscriber subscriber in GetSubscribers())
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
        
        public IProviderSubscriber GetSubscriber(string name)
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

        public List<IProviderSubscriber> GetSubscribers()
        {
            return secretary.Values();
        }

        public List<IProviderSubscriber> GetValidatedSubscribers()
        {
            List<IProviderSubscriber> subscribers = new();
            subscribers.AddRange(from IProviderSubscriber subscriber in GetSubscribers()
                                 where subscriber.IsValid()
                                 select subscriber);
            return subscribers;
        }

        public bool HasSubscriber(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return false;
            }

            return secretary.ContainsKey(name);
        }

        public bool HasSubscribers()
        {
            return !secretary.IsEmpty();

        }

        public void OnAddSubscriber(IProviderSubscriber subscriber)
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

        public bool RegisterSubscriber(IProviderSubscriber subscriber) 
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

        public void UnRegisterSubscriber(IProviderSubscriber subscriber)
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

        public void UnRegisterSubscriber(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return;
            }

            if (HasSubscriber(name))
            {
                IProviderSubscriber subscriber = GetSubscriber(name);
                UnRegisterSubscriber(subscriber);
            }
        }

         new public void Stop()
         {
            base.Stop();

            foreach (IProviderSubscriber subscriber in GetSubscribers())
            {
                UnRegisterSubscriber(subscriber);
                subscriber.OnStopProvider(this);
            }
            secretary.Clear();
         }
    }
}