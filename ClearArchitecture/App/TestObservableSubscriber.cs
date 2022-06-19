using ClearArchitecture.SL;
using System;
using System.Collections.Generic;

namespace ConsoleApp1.App
{
    public class TestObservableSubscriber : AbsObservableSubscriber
    {
        public TestObservableSubscriber(string name) : base(name)
        {
        }

        public override List<string> GetObservable()
        {
            List<string> list = new();
            list.Add("TestObservable");
            return list;
        }

        public override List<string> GetProviderSubscription()
        {
            List<string> list = new();
            list.Add(ObservableUnion.NAME);
            return list;
        }

        public override void OnChangeObservable(string name, object obj)
        {
            Console.WriteLine(DateTime.Now.ToString("G") + ": " + "OnChangeObservable TestObservable");
        }
    }
}
