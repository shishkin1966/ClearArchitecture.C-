using ClearArchitecture.SL;

namespace ConsoleApp1.App
{
    public abstract class BasePresenter : AbsPresenter
    {
        protected BasePresenter(string name) : base(name)
        {
        }

        public override void OnCreateView()
        {
            //
        }

        public override void OnDestroyView()
        {
            Program.SL.UnRegisterSubscriber(this);
        }

        public override void OnReadyView()
        {
            Program.SL.RegisterSubscriber(this);

            DoActions();

            Program.SL.Messenger.ReadMessages(this);

            OnStart();
        }
    }
}