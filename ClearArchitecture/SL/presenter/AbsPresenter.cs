using System.Collections.Generic;

namespace ClearArchitecture.SL
{
    public abstract class AbsPresenter : IPresenter
    {
        private readonly LifecycleObserver lifecycle;

        protected AbsPresenter()
        {
            lifecycle = new LifecycleObserver(this);
        }

        public abstract string GetName();
        public abstract List<string> GetProviderSubscription();

        public abstract void AddAction(IAction action);

        public int GetState()
        {
            return lifecycle.GetState();
        }

        public bool IsRegister()
        {
            return true;
        }
        public bool IsValid()
        {
            return lifecycle.GetState() != Lifecycle.VIEW_DESTROY;
        }

        public abstract bool OnAction(IAction action);
        public void OnCreateView()
        {
            //
        }
        public void OnDestroyView()
        {
            //
        }
        public void OnReadyView()
        {
            //
        }

        public void OnStopProvider(IProvider provider)
        {
            //
        }

        public abstract void Read(IMessage message);
        public void SetState(int state)
        {
            lifecycle.SetState(state);
        }

        public void Stop()
        {
            //
        }
    }
}
