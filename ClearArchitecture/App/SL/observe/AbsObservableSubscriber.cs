using System.Collections.Generic;

namespace ClearArchitecture.SL
{
    public abstract class AbsObservableSubscriber : AbsProviderSubscriber, IObservableSubscriber
    {
        protected AbsObservableSubscriber(string name) : base(name)
        {
        }

        public abstract List<string> GetObservable();

        public int GetState()
        {
            return Lifecycle.VIEW_READY;
        }

        public abstract void OnChangeObservable(string name, object obj);
        
        public void SetState(int state)
        {
            //
        }
    }
}
