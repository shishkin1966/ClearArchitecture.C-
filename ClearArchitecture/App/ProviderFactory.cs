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
                        return new ApplicationProvider();
                    case LogProvider.NAME:
                        return new LogProvider();
                    case OutProvider.NAME:
                        return new OutProvider();
                    case ExecutorProvider.NAME:
                        return new ExecutorProvider();
                    case ObservableUnion.NAME:
                        return new ObservableUnion();
                    case MessengerUnion.NAME:
                        return new MessengerUnion();
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
