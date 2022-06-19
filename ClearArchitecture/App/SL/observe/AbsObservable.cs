using System;
using System.Collections.Generic;

namespace ClearArchitecture.SL
{
    public abstract class AbsObservable : IObservable
    {
        private readonly Secretary<IObservableSubscriber> secretary = new();
        private readonly string name;

        protected AbsObservable(string name)
        {
            this.name = name;
        }

        public string GetName()
        {
            return name;
        }

        public void AddObserver(IObservableSubscriber subscriber)
        {
            secretary.Put(subscriber.GetName(), subscriber);
    
            if (secretary.Size() == 1)
            {
                OnRegisterFirstObserver();
            }
        }

        public IObservableSubscriber GetObserver(string name)
        {
            return secretary.GetValue(name);
        }

        public List<IObservableSubscriber> GetObservers()
        {
            return secretary.Values();

        }

        public void OnChangeObservable(object obj)
        {
            foreach (IObservableSubscriber subscriber in secretary.Values())
            {
                if (subscriber.IsValid())
                {
                    subscriber.OnChangeObservable(GetName(), obj);
                }
            }

        }

        public void RemoveObserver(IObservableSubscriber subscriber)
        {
            if (secretary.ContainsKey(subscriber.GetName()))
            {
                if (subscriber == secretary.GetValue(subscriber.GetName()))
                {
                    secretary.Remove(subscriber.GetName());
                }

                if (secretary.IsEmpty())
                {
                    OnUnRegisterLastObserver();
                }
            }
        }

        public void OnRegisterFirstObserver()
        {
            //
        }

        public void OnUnRegisterLastObserver()
        {
            //
        }

        public void Stop()
        {
            Console.WriteLine(DateTime.Now.ToString("G") + ": " + "Stop "+GetName());
        }

    }
}
