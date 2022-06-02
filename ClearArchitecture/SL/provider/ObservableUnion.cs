using System.Collections.Generic;

namespace ClearArchitecture.SL
{
    public class ObservableUnion<T> : AbsSmallUnion<T>, IObservableUnion<T> where T : IObservableSubscriber
    {
        public const string NAME = "ObservableUnion";

        private readonly Secretary<IObservable> secretary = new();

        public override int CompareTo(IProvider other)
        {
            if (other is IObservableUnion<T>)
            { return 0; }
            else
            { return 1; }
        }

        public override string GetName()
        {
            return NAME;
        }

        public IObservable GetObservable(string name)
        {
            return secretary.GetValue(name);
        }

        public List<IObservable> GetObservables()
        {
            return secretary.Values();
        }

        public void OnChangeObservable(string name, object obj)
        {
            if (string.IsNullOrEmpty(name)) return;

            IObservable observable = GetObservable(name);
            if (observable != null)
            {
                observable.OnChangeObservable(obj);
            }
        }

        public bool RegisterObservable(IObservable observable)
        {
            if (observable == null) return true;

            secretary.Put(observable.GetName(), observable);
            return true;
        }

        public bool UnRegisterObservable(IObservable observable)
        {
            if (observable == null) return true;

            if (secretary.ContainsKey(observable.GetName()))
            {
                if ( observable == secretary.GetValue(observable.GetName()))
                {
                    secretary.Remove(observable.GetName());
                    return true;
                }
            }
            return false;
        }

        new public void Stop()
        {
            base.Stop();
    
            foreach (IObservable observable in GetObservables())
            {
                observable.Stop();
            }
        }

        new public void UnRegisterSubscriber(T subscriber)
        {
            if (subscriber == null) return;

            base.UnRegisterSubscriber(subscriber);

            List<string> list = subscriber.GetObservable();
            foreach (IObservable observable in GetObservables())
            {
                if (list.Contains(observable.GetName()))
                {
                    observable.RemoveObserver(subscriber);
                }
            }
        }

        new public bool RegisterSubscriber(T subscriber)
        {
            if (subscriber == null) return true;

            if (!base.RegisterSubscriber(subscriber)) return false;

            List<string> list = subscriber.GetObservable();
            foreach (IObservable observable in GetObservables()) 
            {
                string name = observable.GetName();
                if (list.Contains(name)) {
                    observable.AddObserver(subscriber);
                }
            }
            return true;
        }

        new public void OnUnRegister()
        {
            base.OnUnRegister();

            foreach (IObservable observable in GetObservables())
            {
                observable.Stop();
            }
        }


    }
}
