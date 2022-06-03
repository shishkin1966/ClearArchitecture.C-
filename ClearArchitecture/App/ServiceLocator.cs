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
            RegisterProvider(MessengerUnion<IMessengerSubscriber>.NAME);
            RegisterProvider(ObservableUnion<IObservableSubscriber>.NAME);
            RegisterProvider(ExecutorProvider.NAME);

            RegisterProvider(OutProvider.NAME); 
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
