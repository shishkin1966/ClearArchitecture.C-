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
                    case ApplicationProvider.NAME:
                        return new ApplicationProvider(ApplicationProvider.NAME);
                    case LogProvider.NAME:
                        return new LogProvider(LogProvider.NAME);
                    case OutProvider.NAME:
                        return new OutProvider(OutProvider.NAME);
                    case ExecutorProvider.NAME:
                        return new ExecutorProvider(ExecutorProvider.NAME);
                    case ObservableUnion.NAME:
                        return new ObservableUnion(ObservableUnion.NAME);
                    case MessengerUnion.NAME:
                        return new MessengerUnion(MessengerUnion.NAME);
                    case PresenterUnion.NAME:
                        return new PresenterUnion(PresenterUnion.NAME);
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
