namespace ClearArchitecture.SL
{
    public class DataMessage : AbsMessage
    {
        private readonly object data;


        public DataMessage(string address, object data) : base(address)
        {
            this.data = data;
        }

        public override IMessage Copy()
        {
            return new DataMessage(GetAddress(), data);
        }

        public override void Read(IMessengerSubscriber subscriber)
        {
            if (subscriber == null)
            {
                return;
            }

            subscriber.Read(this);
        }
    }
}
