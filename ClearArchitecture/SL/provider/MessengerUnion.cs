using App.Metrics.Concurrency;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace ClearArchitecture.SL
{
    public class MessengerUnion<T> : AbsSmallUnion<T>, IMessengerUnion<T> where T : IMessengerSubscriber
    {
        public const string NAME = "MessengerUnion";

        private ConcurrentDictionary<int, IMessage> messages = new();
        private readonly Secretary<List<string>> messagingList = new();
        private AtomicInteger atomicId = new(0);

        private List<string> GetAddresses(string address)  
        {
            List<string> addresses = new();
            if (messagingList.ContainsKey(address)) {
                List<string> list = messagingList.GetValue(address);
                if (list != null) 
                {
                    foreach (string adr in list) 
                    {
                        addresses.AddRange(GetAddresses(adr));
                    }
                }
            } 
            else
            {
                addresses.Add(address);
            }
            return addresses;
        }

        private void RemoveDublicate(IMessage message)
        {
            foreach (IMessage tmpMessage in messages.Values)
            {
                if (message.GetSubj() == tmpMessage.GetSubj() && message.GetAddress() == tmpMessage.GetAddress())
                {
                    RemoveMessage(tmpMessage);
                }
            }
        }

        private void CheckAndReadMessagesSubscriber(string address)
        {
            T subscriber = GetSubscriber(address);
            if (subscriber != null && address == subscriber.GetName())
            {
                ReadMessages(subscriber);
            }
        }

        private T CheckSubscriber(string address) 
        {
            T subscriber = GetSubscriber(address);
            if (subscriber != null && address == subscriber.GetName()) 
            {
                int state = subscriber.GetState();
                if (state == Lifecycle.VIEW_READY || state == Lifecycle.VIEW_NOT_READY) {
                    return subscriber;
                }
            }
            return default;
        }


        public void AddMessage(IMessage message)
        {
            if (message == null) return;

            List<string> list = new();
            list.AddRange(message.GetCopyTo());
            if (!string.IsNullOrEmpty(message.GetAddress()))
            {
                list.Add(message.GetAddress());
            }
            List<string> addresses = new();
            foreach (string address in list)
            {
                addresses.AddRange(GetAddresses(address));
            }
            foreach (string address in addresses)
            {
                int id = atomicId.Increment();
                IMessage newMessage = message.Copy();
                newMessage.SetMessageId(id);
                newMessage.SetAddress(address);
                newMessage.SetCopyTo(new List<string>());
    
                if (!message.IsCheckDublicate())
                {
                    messages[id] = newMessage;
                }
                else
                {
                    RemoveDublicate(newMessage);
                    messages[id] = newMessage;
                }

                CheckAndReadMessagesSubscriber(address);
            }
        }

        public void AddMessagingList(string name, List<string> addresses)
        {
            if (string.IsNullOrEmpty(name) || addresses == null) return;

            messagingList.Put(name, addresses);
        }

        public void AddNotMandatoryMessage(IMessage message)
        {
            List<string> list = new();
            list.AddRange(message.GetCopyTo());
            if (!string.IsNullOrEmpty(message.GetAddress()))
            {
                list.Add(message.GetAddress());
            }
            List<string> addresses = new();
            foreach (string address in list)
            {
                addresses.AddRange(GetAddresses(address));
            }
            foreach (string address in addresses)
            {
                T subscriber = CheckSubscriber(address);
                if (subscriber != null)
                {
                    message.Read(subscriber);
                }
            }
        }

        public void ClearMessages()
        {
            throw new System.NotImplementedException();
        }

        public void ClearMessages(string subscriber)
        {
            throw new System.NotImplementedException();
        }

        public override int CompareTo(IProvider other)
        {
            if (other is IMessengerUnion<T>)
            { return 0; }
            else 
            { return 1; }
        }

        public List<IMessage> GetMessages(IMessengerSubscriber subscriber)
        {
            throw new System.NotImplementedException();
        }

        public List<string> GetMessagingList(string name)
        {
            throw new System.NotImplementedException();
        }

        public override string GetName()
        {
            return NAME;
        }

        public void ReadMessages(IMessengerSubscriber subscriber)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveMessage(IMessage message)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveMessagingList(string name)
        {
            throw new System.NotImplementedException();
        }

        public void ReplaceMessage(IMessage message)
        {
            throw new System.NotImplementedException();
        }
    }
}
