using System;

namespace ClearArchitecture.SL
{
    public class LifecycleObserver : ILifecycle
    {
        private int state = Lifecycle.VIEW_CREATE;
        private readonly WeakReference listener;

        public LifecycleObserver(ILifecycleListener listener)
        {
            if (listener != null)
            {
                this.listener = new WeakReference(listener, false);
            }
            SetState(Lifecycle.VIEW_CREATE);

        }

        /**
        * Получить состояние объекта
        *
        * @return состояние объекта
        */
        public int GetState()
        {
            return state;
        }

        /**
        * Установить состояние объекта
        *
        * @param state состояние объекта
        */
        public void SetState(int state)
        {
            this.state = state;

            switch(state) 
            {
                case Lifecycle.VIEW_CREATE:
                    if (listener.IsAlive)
                    {
                        ILifecycleListener _listener = listener.Target as ILifecycleListener;
                        _listener.OnCreateView();
                    }
                    break;
                case Lifecycle.VIEW_READY:
                    if (listener.IsAlive)
                    {
                        ILifecycleListener _listener = listener.Target as ILifecycleListener;
                        _listener.OnReadyView();
                    }
                    break;
                case Lifecycle.VIEW_DESTROY:
                    if (listener.IsAlive)
                    {
                        ILifecycleListener _listener = listener.Target as ILifecycleListener;
                        _listener.OnDestroyView();
                    }
                    break;
            }
        }
    }
}
