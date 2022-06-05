using ClearArchitecture.SL;
using System;
using System.Collections.Generic;

namespace ConsoleApp1.App
{
    public class MessengerSubscriber : IMessengerSubscriber, IResponseListener
    {
        public const string NAME = "MessengerSubscriber";
        public string GetName()
        {
            return NAME;
        }

        public List<string> GetProviderSubscription()
        {
            List<string> list = new();
            list.Add(Program.SL.Messenger.GetName());
            return list;
        }

        public int GetState()
        {
            return Lifecycle.VIEW_READY;
        }

        public bool IsValid()
        {
            return true;
        }

        public void OnStopProvider(IProvider provider)
        {
            //
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

        public void Stop()
        {
            //
        }
    }
}
