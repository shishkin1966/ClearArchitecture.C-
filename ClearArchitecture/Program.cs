using ConsoleApp1.App;
using System;
using System.Threading;

namespace ConsoleApp1
{
    public static class Program 
    {
        public const string NAME = "Application";

        private readonly static ServiceLocator sl = new(ServiceLocator.NAME);

        public static ServiceLocator SL
        {
            get
            {
                return sl;
            }
        }

        static void Main(string[] args)
        {
            TestSubscriber ms = new(TestSubscriber.NAME);
            TestObservable obs = new(TestObservable.NAME);
            var pool = new TestPool();
            Console.WriteLine(pool.ToString());

            var v = pool.Get(4);
            Console.WriteLine(v.Count.ToString());

            pool.Release(v);
            pool.Release(v);
            pool.Release(v);
            Console.WriteLine(pool.ToString());

            SL.Start();

            SL.RegisterSubscriber(ms);

            SL.Observable.RegisterObservable(obs);
            SL.Observable.RegisterSubscriber(new TestObservableSubscriber(TestObservableSubscriber.NAME));

            SL.Executor.PutRequest(new GetRequest(NAME, ms.GetName(), 1));

            Thread.Sleep(1000); 

            SL.Messenger.AddComment("Все хорошо");

            SL.Observable.OnChangeObservable(TestObservable.NAME,"Change 1");

            Console.WriteLine(SL.Messenger.GetComment());

            SL.Stop();
        }
    }
}
