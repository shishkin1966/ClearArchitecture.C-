using System;
using System.Collections.Generic;

namespace ClearArchitecture.SL
{
    public abstract class AbsUnion<T> : AbsSmallUnion<T>, IUnion<T> where T : IProviderSubscriber
    {
        private WeakReference currentSubscriber;

        public new bool Register(T subscriber)
        {
            if (subscriber == null)
            {
                return false;
            }

            if (base.Register(subscriber)) {
                if (currentSubscriber != null)
                {
                    if (currentSubscriber.IsAlive)
                    {
                        T oldSubscriber = (T)currentSubscriber.Target;
                        if (subscriber.GetName() == (oldSubscriber.GetName())) 
                        {
                            currentSubscriber = new WeakReference(subscriber, false);
                        }
                    }
                }
                return true;
            }
            return false;
        }

        public new void Unregister(T subscriber)
        {
            if (subscriber == null) return;

            base.Unregister(subscriber);

            if (currentSubscriber != null)
            {
                if (currentSubscriber.IsAlive)
                {
                    T oldSubscriber = (T)currentSubscriber.Target;
                    if (subscriber.GetName() == (oldSubscriber.GetName()))
                    {
                        currentSubscriber.Target = null;
                    }
                }
            }
        }

        public bool SetCurrentSubscriber(T subscriber) 
        {
            if (subscriber == null) return false;

            if (!subscriber.IsValid()) return false;

            if (!Contains(subscriber)) {
                Register(subscriber);
            }
            
            if (!Contains(subscriber)) {
                return false;
            }

            currentSubscriber = new WeakReference(subscriber, false);

            return true;
        }

        public T GetCurrentSubscriber() 
        {
            if (currentSubscriber != null && currentSubscriber.IsAlive)
            {
                return (T)currentSubscriber.Target;
            }
            return default;
        }
    }
}
