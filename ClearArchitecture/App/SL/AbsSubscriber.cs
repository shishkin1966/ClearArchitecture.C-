namespace ClearArchitecture.SL
{
    public abstract class AbsSubscriber : ISubscriber
    {
        public abstract string GetName();

        public bool IsValid()
        {
            return true;
        }

        public bool IsBusy()
        {
            return false;
        }

    }
}
