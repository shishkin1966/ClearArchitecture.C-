using ClearArchitecture.SL;
using System;

namespace ConsoleApp1.App
{
    public class OutProvider : IOutProvider
    {
        public const string NAME = "OutProvider";

        public int CompareTo(IProvider other)
        {
            if (other is OutProvider)
            { 
                return 0; 
            } 
            else
            { 
                return 1; 
            }
        }

        public string GetName()
        {
            return NAME;
        }

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
            Console.WriteLine("OnRegister " + NAME);
        }

        public void OnUnRegister()
        {
            Console.WriteLine("OnUnRegister " + NAME);
        }

        public void Stop()
        {
            Console.WriteLine("Stop " + NAME);
        }

        public void WriteLine(string line)
        {
            if (string.IsNullOrEmpty(line)) return;

            Console.WriteLine(line);
        }
    }
}
