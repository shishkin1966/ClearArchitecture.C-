using System.Collections.Generic;

namespace ClearArchitecture.SL
{
    public abstract class AbsMessage : AbsAction, IMessage
    {
        private int id = -1;
        private string address;
        private readonly List<string> copyTo = new();
        private long keepAliveTime = -1L;
        private string subj;

        protected AbsMessage() 
        {
        }

        protected AbsMessage (string address) : this()
        {
            this.address = address;
        }

        protected AbsMessage(List<string> address) : this()
        {
            if (address != null && address.Count > 0)
            {
                this.address = address[0];
                var copy = new List<string>();
                for (int i = 1; i < address.Count; i++)
                {
                    copy.Add(address[i]);
                }
                copyTo.AddRange(copy);
            }
        }

        protected AbsMessage(IMessage message) : this(message.GetAddress())
        {
            copyTo.AddRange(message.GetCopyTo());
            id = message.GetMessageId();
            keepAliveTime = message.GetEndTime();
            subj = message.GetSubj();
        }

        public abstract IMessage Copy();
        public abstract void Read(IMessengerSubscriber subscriber);

        public bool ContainsAddress(string address)
        {
            if (string.IsNullOrEmpty(address))
            {
                return false;
            }

            if (address == this.address)
            {
                return true;
            }

            foreach (string s in copyTo)
            {
                if (s == address)
                {
                    return true;
                }
            }
            return false;
        }

        public string GetAddress()
        {
            return address;
        }

        public List<string> GetCopyTo()
        {
            return copyTo;
        }

        public long GetEndTime()
        {
            return keepAliveTime;
        }

        public int GetMessageId()
        {
            return id;
        }

        public string GetSubj()
        {
            return subj;
        }

        public bool IsCheckDublicate()
        {
            return false;
        }

        public bool IsSticky()
        {
            return false;
        }

        public IMessage SetAddress(string address)
        {
            this.address = address;
            return this;
        }

        public IMessage SetCopyTo(List<string> copyTo)
        {
            this.copyTo.Clear();
            this.copyTo.AddRange(copyTo);
            return this;
        }

        public IMessage SetEndTime(long keepAliveTime)
        {
            this.keepAliveTime = keepAliveTime;
            return this;
        }

        public IMessage SetMessageId(int id)
        {
            this.id = id;
            return this;
        }

        public IMessage SetSubj(string subj)
        {
            this.subj = subj;
            return this;
        }
    }
}
