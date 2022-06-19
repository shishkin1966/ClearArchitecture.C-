using ClearArchitecture.SL;
using ConsoleApp1.App;
using System;
using System.Threading;

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
            TestSubscriber ms = new();
            TestObservable obs = new("TestObservable");

            SL.Start();

            SL.RegisterSubscriber(ms);

            SL.Observable.RegisterObservable(obs);
            SL.Observable.RegisterSubscriber(new TestObservableSubscriber("TestObservableSubscriber"));

            SL.Executor.PutRequest(new GetRequest(NAME, ms.GetName(), 1));

            Thread.Sleep(500); 

            SL.Messenger.AddComment("Все хорошо");

            SL.Observable.OnChangeObservable("TestObservable","Change");

            Console.WriteLine(SL.Messenger.GetComment());

            SL.Stop();
        }
    }
}
