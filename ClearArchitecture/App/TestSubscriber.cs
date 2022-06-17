using ClearArchitecture.SL;
using System;
using System.Collections.Generic;

namespace ConsoleApp1.App
{
    public class TestSubscriber : AbsProviderSubscriber, IMessengerSubscriber, IResponseListener
    {
        public const string NAME = "TestSubscriber";

        public TestSubscriber() : base(NAME)
        {
        }

        public override List<string> GetProviderSubscription()
        {
            List<string> list = new();
            list.Add(Program.SL.Messenger.GetName());
            return list;
        }

        public int GetState()
        {
            return Lifecycle.VIEW_READY;
        }

        public void Read(IMessage message)
        {
            //
        }

        public void Response(ExtResult result)
        {
            Console.WriteLine("Test #"+ result.GetData().ToString());
        }

        public void SetState(int state)
        {
            //
        }

    }
}
