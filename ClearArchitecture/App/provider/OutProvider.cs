using ClearArchitecture.SL;
using System;

namespace ConsoleApp1.App
{
    public class OutProvider : AbsProvider, IOutProvider
    {
        public const string NAME = "OutProvider";

        public OutProvider(string name) : base(name)
        {
        }

        public override int CompareTo(IProvider other)
        {
            if (other is IOutProvider)
            { 
                return 0; 
            } 
            else
            { 
                return 1; 
            }
        }

        public void WriteLine(string line)
        {
            if (string.IsNullOrEmpty(line)) return;

            Console.WriteLine(line);
        }
    }
}
