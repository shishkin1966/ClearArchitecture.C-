using System;

namespace ClearArchitecture.SL
{
    public abstract class AbsUnion : AbsSmallUnion, IUnion
    {
        private WeakReference currentSubscriber;

        protected AbsUnion(string name) : base(name)
        {

        }

        public new bool RegisterSubscriber(IProviderSubscriber subscriber)
        {
            if (subscriber == null)
            {
                return false;
            }

            if (base.RegisterSubscriber(subscriber)) {
                if (currentSubscriber != null && currentSubscriber.IsAlive)
                {
                    IProviderSubscriber oldSubscriber = (IProviderSubscriber)currentSubscriber.Target;
                    if (subscriber.GetName() == (oldSubscriber.GetName()))
                    {
                        currentSubscriber = new WeakReference(subscriber, false);
                    }
                }
                return true;
            }
            return false;
        }

        public new void UnRegisterSubscriber(IProviderSubscriber subscriber)
        {
            if (subscriber == null) return;

            base.UnRegisterSubscriber(subscriber);

            if (currentSubscriber != null && currentSubscriber.IsAlive)
            {
                IProviderSubscriber oldSubscriber = (IProviderSubscriber)currentSubscriber.Target;
                if (subscriber.GetName() == (oldSubscriber.GetName()))
                {
                    currentSubscriber.Target = null;
                }
            }
        }

        public bool SetCurrentSubscriber(IProviderSubscriber subscriber) 
        {
            if (subscriber == null) return false;

            if (!subscriber.IsValid()) return false;

            if (!ContainsSubscriber(subscriber)) {
                RegisterSubscriber(subscriber);
            }
            
            if (!ContainsSubscriber(subscriber)) {
                return false;
            }

            currentSubscriber = new WeakReference(subscriber, false);

            return true;
        }

        public IProviderSubscriber GetCurrentSubscriber() 
        {
            if (currentSubscriber != null && currentSubscriber.IsAlive)
            {
                return (IProviderSubscriber)currentSubscriber.Target;
            }
            return default;
        }

    }
}
