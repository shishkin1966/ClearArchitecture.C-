namespace ClearArchitecture.SL
{
    public class ResultMessage : AbsMessage
    {
        private readonly ExtResult result; 


        private ResultMessage(string address) : base(address)
        {
        }

        protected ResultMessage(string address, ExtResult result) : this(address)
        {
            this.result = result;
        }

        private ResultMessage(ResultMessage message) : base(message)
        {
        }

        protected ResultMessage(ResultMessage message, ExtResult result) : this(message)
        {
            this.result = result;
        }

        public override IMessage Copy()
        {
            return new ResultMessage(this, result);
        }

        public override void Read(IMessengerSubscriber subscriber)
        {
            if (subscriber == null) return;

            if (subscriber is IResponseListener listener)
            {
                listener.Response(result);
            }
        }
    }
}
