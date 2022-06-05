using ConsoleApp1.App;

namespace ConsoleApp1
{
    public static class Program 
    {
        public const string NAME = "Application";

        private readonly static ServiceLocator sl = new();

        public static ServiceLocator SL
        {
            get
            {
                return sl;
            }
        }

        static void Main(string[] args)
        {

            SL.Start();

            SL.Out.WriteLine("Test");

            SL.Executor.PutRequest(new GetRequest(NAME,null,1));

            SL.Stop();
        }
    }
}
