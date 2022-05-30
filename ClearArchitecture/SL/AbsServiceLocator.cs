namespace ClearArchitecture.SL
{
    public abstract class AbsServiceLocator : IServiceLocator
    {
        public const string NAME = "AbsServiceLocator";

        private readonly Secretary<IProvider> secretary = new();

        public bool ExistsProvider(string name)
        {
            if (string.IsNullOrEmpty(name)) return false;

            return secretary.ContainsKey(name);
        }


    }
}
