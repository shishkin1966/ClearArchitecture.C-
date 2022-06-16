
using System.Collections.Generic;

namespace ClearArchitecture.SL
{
    public abstract class AbsProviderSubscriber : AbsSubscriber, IProviderSubscriber
    {
        private readonly Secretary<IProvider> providers = new();

        public abstract List<string> GetProviderSubscription();

        public void OnStopProvider(IProvider provider)
        {
            //
        }

        public void Stop()
        {
            //
        }

        public List<IProvider> GetProviders()
        {
            return providers.Values();
        }

        public void SetProvider(IProvider provider)
        {
            if (provider == default) return;

            providers.Put(provider.GetName(), provider);
        }

        public void RemoveProvider(IProvider provider)
        {
            if (provider == default) return;

            providers.Remove(provider.GetName());
        }

    }
}
