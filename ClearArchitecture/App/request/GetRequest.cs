using ClearArchitecture.SL;
using System;

namespace ConsoleApp1.App
{
    public class GetRequest : AbsRequest, IRequest
    {
        public const string NAME = "GetRequest";

        public GetRequest(string sender, string receiver, object obj) : base(sender, receiver, obj)
        {
        }

        public override void Execute(object obj)
        {
            Program.SL.Observable.OnChangeObservable(TestObservable.NAME, "Change 0");

            try
            {
                if (obj is IRequest requst)
                {
                    SetResult(new ExtResult(requst.GetData()));
                    Console.WriteLine("Запрос: " + requst.ToString() + ":" + requst.GetResult().GetData().ToString());
                    SendResult();
                }
            }
            catch (Exception e)
            {
                Program.SL.Log.AddError(new ExtError().AddError(GetName(), e));
            }
            finally
            {
                RemoveRequest();
            }
        }

        public override string GetName()
        {
            return NAME;
        }

        public override void SendResult()
        {
            Program.SL.Messenger.AddNotMandatoryMessage(new ResultMessage(GetReceiver(),GetResult()));
        }
    }
}
