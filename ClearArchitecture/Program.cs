﻿using ConsoleApp1.App;
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
            MessengerSubscriber ms = new();

            SL.Start();

            SL.RegisterSubscriber(ms);

            SL.Executor.PutRequest(new GetRequest(NAME, ms.GetName(), 1));

            Thread.Sleep(500); 

            SL.UnRegisterSubscriber(ms);

            SL.Stop();
        }
    }
}
