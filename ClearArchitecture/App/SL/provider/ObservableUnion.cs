using System.Collections.Generic;
using System.Linq;

namespace ClearArchitecture.SL
{
    public class ObservableUnion : AbsSmallUnion, IObservableUnion
    {
        public const string NAME = "ObservableUnion";

        private readonly Secretary<IObservable> secretary = new();

        public ObservableUnion(string name) : base(name)
        {
        }

        public override int CompareTo(IProvider other)
        {
            if (other is IObservableUnion)
            { return 0; }
            else
            { return 1; }
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

        public void UnRegisterSubscriber(IObservableSubscriber subscriber)
        {
            if (subscriber == null) return;

            base.UnRegisterSubscriber(subscriber);

            List<string> list = subscriber.GetObservable();
            foreach (var observable in from IObservable observable in GetObservables()
                                       where list.Contains(observable.GetName())
                                       select observable)
            {
                observable.RemoveObserver(subscriber);
            }
        }

        public bool RegisterSubscriber(IObservableSubscriber subscriber)
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
