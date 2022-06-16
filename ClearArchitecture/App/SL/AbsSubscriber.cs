namespace ClearArchitecture.SL
{
    public abstract class AbsSubscriber : ISubscriber
    {
        private bool isBusy = false;

        public abstract string GetName();

        public bool IsValid()
        {
            return true;
        }

        public bool IsBusy()
        {
            return isBusy;
        }

        public void SetBusy()
        {
            isBusy = true;
        }

        public void SetUnBusy()
        {
            isBusy = false;
        }
    }
}
