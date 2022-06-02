using ClearArchitecture.SL;
using ConsoleApp1.App;

namespace ConsoleApp1
{
    public static class Program
    {
        private readonly static ServiceLocator sl = new();

        public static ServiceLocator ServiceLocator
        {
            get
            {
                return sl;
            }
        }

        static void Main(string[] args)
        {

            ServiceLocator.Start();

            ServiceLocator.Out.WriteLine("Test");

            ServiceLocator.Stop();
        }
    }
}
