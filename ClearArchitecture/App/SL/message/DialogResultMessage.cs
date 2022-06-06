namespace ClearArchitecture.SL
{
    public class DialogResultMessage : AbsMessage
    {
        private readonly DialogResultAction action;
        public const string SUBJ = "DialogResultMessage";

        public DialogResultMessage(string address, DialogResultAction action) : base(address)
        {
            this.action = action;
        }

        public DialogResultMessage(DialogResultMessage message, DialogResultAction action) : base(message)
        {
            this.action = action;
        }

        public new string GetSubj()
        {
            return SUBJ;
        }

        public override IMessage Copy()
        {
            return new DialogResultMessage(this, action);
        }

        public override void Read(IMessengerSubscriber subscriber)
        {
            if (subscriber == null)
            {
                return;
            }

            if (subscriber is IDialogResultListener listener)
            {
                listener.OnDialogResult(action);
            }
        }

    }
}
