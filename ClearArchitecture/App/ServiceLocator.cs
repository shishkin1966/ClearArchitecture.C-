using ClearArchitecture.SL;

namespace ConsoleApp1.App
{
    public class ServiceLocator : AbsServiceLocator, IServiceLocator
    {
        public const string NAME = "ServiceLocator";

        public IOutProvider Out
        {
            get
            {
                return (IOutProvider)GetProvider(OutProvider.NAME);
            }
        }

        public IApplicationProvider App
        {
            get
            {
                return (IApplicationProvider)GetProvider(ApplicationProvider.NAME);
            }
        }

        public IMessengerUnion<IMessengerSubscriber> Messenger
        {
            get
            {
                return (IMessengerUnion<IMessengerSubscriber>)GetProvider(MessengerUnion<IMessengerSubscriber>.NAME);
            }
        }

        public IObservableUnion<IObservableSubscriber> Observable
        {
            get
            {
                return (IObservableUnion<IObservableSubscriber>)GetProvider(ObservableUnion<IObservableSubscriber>.NAME);
            }
        }

        public IExecutor Executor
        {
            get
            {
                return (IExecutor)GetProvider(ExecutorProvider.NAME);
            }
        }

        public override void Start()
        {
            RegisterProvider(ApplicationProvider.NAME);
            RegisterProvider(MessengerUnion<IMessengerSubscriber>.NAME);
            RegisterProvider(ObservableUnion<IObservableSubscriber>.NAME);
            RegisterProvider(ExecutorProvider.NAME);
            RegisterProvider(OutProvider.NAME); 
        }

        new public void Stop()
        {
            base.Stop();

            App.SetExit();
        }

        public override IProviderFactory GetProviderFactory()
        {
            return new ProviderFactory();
        }

        public override string GetName()
        {
            return NAME;
        }
    }
}
