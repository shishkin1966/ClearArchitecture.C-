using System.Collections.Generic;

namespace ClearArchitecture.SL
{
    public abstract class AbsPresenter : AbsProviderSubscriber, IPresenterSubscriber
    {
        private readonly LifecycleObserver lifecycle;
        private readonly List<IAction> actions = new();

        protected AbsPresenter(string name) : base(name)
        {
            lifecycle = new LifecycleObserver(this);
        }

        public abstract void OnStart();

        public override List<string> GetProviderSubscription()
        {
            List<string> list = new();
            list.Add(PresenterUnion.NAME); 
            list.Add(MessengerUnion.NAME);
            return list;
        }

        public bool OnAction(IAction action)
        {
            return true;
        }

        public int GetState()
        {
            return lifecycle.GetState();
        }

        public bool IsRegister()
        {
            return true;
        }
        new public bool IsValid()
        {
            return lifecycle.GetState() != Lifecycle.VIEW_DESTROY;
        }

        public void AddAction(IAction action)
        {
            switch (GetState()) 
            {
                case Lifecycle.VIEW_DESTROY:
                    return;

                case Lifecycle.VIEW_NOT_READY:
                case Lifecycle.VIEW_CREATE:  
                    if (!action.IsRun())
                    {
                            actions.Add(action);
                    }
                    return;

                default: 
                    if (!action.IsRun())
                    {
                        actions.Add(action);
                    }
                    DoActions();
                    return;
            }
        }

        protected void DoActions()
        {
            var deleted = new List<IAction>();
            for (int i=0;i < actions.Count;i++)
            {
                if (GetState() != Lifecycle.VIEW_READY)
                {
                    break;
                }
                if (!actions[i].IsRun())
                {
                    actions[i].SetRun();
                    OnAction(actions[i]);
                    deleted.Add(actions[i]);
                }
            }
            foreach (IAction action in deleted)
            {
                actions.Remove(action);
            }
        }

        public abstract void OnCreateView();

        public abstract void OnDestroyView();

        public abstract void OnReadyView();

        public abstract void Read(IMessage message);

        public void SetState(int state)
        {
            lifecycle.SetState(state);
        }
    }
}
