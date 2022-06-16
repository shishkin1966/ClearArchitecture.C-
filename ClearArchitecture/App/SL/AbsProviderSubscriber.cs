
using System.Collections.Generic;

namespace ClearArchitecture.SL
{
    public abstract class AbsProviderSubscriber : IProviderSubscriber
    {
        public abstract string GetName();
        public abstract List<string> GetProviderSubscription();

        public bool IsValid()
        {
            return true;
        }

        public void OnStopProvider(IProvider provider)
        {
            //
        }

        public void Stop()
        {
            //
        }

        public bool IsBusy()
        {
            return false;
        }

    }
}
