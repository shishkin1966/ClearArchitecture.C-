using ClearArchitecture.SL;

namespace ConsoleApp1.App
{
    public class ProviderFactory : IProviderFactory, INamed
    {
        public const string NAME = "ProviderFactory";

        public IProvider Create(string name)
        {
            if (string.IsNullOrEmpty(name)) return default;

            try
            {
                switch (name)
                {
                    case OutProvider.NAME:
                        return new OutProvider();
                    case ExecutorProvider.NAME:
                        return new ExecutorProvider();
                    case ObservableUnion<IObservableSubscriber>.NAME:
                        return new ObservableUnion<IObservableSubscriber>();
                    case MessengerUnion<IMessengerSubscriber>.NAME:
                        return new MessengerUnion<IMessengerSubscriber>();
                    default:
                        return default;
                }
            }
            catch
            {
                return default;
            }
        }

        public string GetName()
        {
            return NAME;
        }
    }
}
