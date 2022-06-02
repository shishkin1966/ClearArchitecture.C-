using ConsoleApp1.App;

namespace ConsoleApp1
{
    static class Program
    {
        static void Main(string[] args)
        {
            ServiceLocator sl = new();

            sl.Start();

            sl.Out.WriteLine("Test");

            sl.Stop();
        }
    }
}
