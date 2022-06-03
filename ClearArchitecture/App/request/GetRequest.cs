using ClearArchitecture.SL;
using System;

namespace ConsoleApp1.App
{
    public class GetRequest : AbsRequest, IRequest
    {
        public const string NAME = "GetRequest";

        public GetRequest(string sender, string receiver, object obj) : base(sender,receiver,obj)
        {
        }

        public override void Execute(object obj)
        {
            Console.WriteLine("Запрос: "+obj.ToString()+":"+(obj as IRequest).GetData().ToString());
            RemoveRequest();
        }

        public override string GetName()
        {
            return NAME;
        }
    }
}
