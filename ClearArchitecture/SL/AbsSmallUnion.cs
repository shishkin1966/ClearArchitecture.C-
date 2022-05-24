
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

        public abstract List<T> GetReadySubscribers();
        public abstract T GetSubscriber(string name);
        public abstract List<T> GetSubscribers();
        public abstract List<T> GetValidatedSubscribers();
        public abstract bool HasSubscriber(string name);
        public abstract bool HasSubscribers();
        public abstract void OnAddSubscriber(T subscriber);
        public abstract void OnRegisterFirstSubscriber();
        public abstract void OnUnRegisterLastSubscriber();

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
    }
}
