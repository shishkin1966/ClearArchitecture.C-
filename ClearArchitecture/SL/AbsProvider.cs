
namespace ClearArchitecture.SL
{
    public abstract class AbsProvider : IProvider
    {
        public abstract int CompareTo(IProvider other);
        public abstract string GetName();

        public bool IsPersistent()
        {
            return false;
        }

        bool IValidated.IsValid()
        {
            return true;
        }

        void IProvider.OnRegister()
        {
            // Method intentionally left empty.
        }

        void IProvider.OnUnRegister()
        {
            // Method intentionally left empty.
        }

        void IProvider.Stop()
        {
            // Method intentionally left empty.
        }
    }
}
