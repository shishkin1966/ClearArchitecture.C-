using ClearArchitecture.SL;
using System;
using System.Collections.Generic;

namespace ConsoleApp1.App
{
    public class ApplicationProvider : AbsProvider, IApplicationProvider
    {
        public const string NAME = "ApplicationProvider";
        private bool isExit = false;

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

        public override string GetName()
        {
            return NAME;
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
