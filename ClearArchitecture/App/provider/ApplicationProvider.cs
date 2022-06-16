using ClearArchitecture.SL;
using System;

namespace ConsoleApp1.App
{
    public class ApplicationProvider : AbsProvider, IApplicationProvider
    {
        public const string NAME = "ApplicationProvider";
        private bool isExit = false;

        public ApplicationProvider(string name) : base(name)
        {
        }

        public override int CompareTo(IProvider other)
        {
            if (other is IApplicationProvider)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

        public bool IsExit()
        {
            return isExit;
        }

        new public bool IsPersistent()
        {
            return true;    
        }

        public void OnExit()
        {
            Console.WriteLine("Application exit");
        }

        public void SetExit()
        {
            isExit = true;

            OnExit();
        }

    }
}
