
using System;

namespace ClearArchitecture.SL
{
    public abstract class AbsProvider : AbsSubscriber, IProvider
    {
        private readonly string name;

        protected AbsProvider(string name)
        {
            this.name = name;
        }

        public abstract int CompareTo(IProvider other);

        public bool IsPersistent()
        {
            return false;
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

        public override string GetName()
        {
            return name;
        }

    }
}
