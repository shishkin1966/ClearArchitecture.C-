
using System;

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

        public bool IsValid()
        {
            return true;
        }

        public void OnRegister()
        {
            Console.WriteLine( "OnRegister " + GetName());
        }

        public void OnUnRegister()
        {
            Console.WriteLine("OnUnRegister " + GetName());
        }

        public void Stop()
        {
            Console.WriteLine("Stop " + GetName());
        }

    }
}
