using System.Collections.Generic;

namespace ClearArchitecture.SL
{
    public abstract class AbsRequest : IRequest
    {
        private readonly object data;
        private readonly string sender;
        private readonly List<string> receiver = new();
        private int id = -1;
        private bool isCancelled = false;
        private IExecutorProvider executor;
        private ExtResult result;

        protected AbsRequest(string sender, string receiver, object data)
        {
            this.sender = sender;
            if (!string.IsNullOrEmpty(receiver))
            {
                this.receiver.Add(receiver);
            }
            this.data = data;
        }

        protected AbsRequest(string sender, List<string> receivers, object data)
        {
            this.sender = sender;
            if (receivers != null)
            {
                this.receiver.AddRange (receivers);
            }
            this.data = data;
        }

        public abstract void Execute(object obj);
        public abstract string GetName();
        public abstract void SendResult();

        public int GetAction(IRequest oldRequest)
        {
            return ExecutorProvider.ACTION_DELETE;
        }
        
        public int GetId()
        {
            return id;
        }

        public string GetSender()
        {
            return sender;
        }
        public bool IsCancelled()
        {
            return isCancelled;
        }

        public bool IsDistinct()
        {
            return true;
        }

        public bool IsSingle()
        {
            return true;
        }

        public bool IsValid()
        {
            return true;
        }

        public IRequest SetCanceled()
        {
            isCancelled = true;
            return this;
        }

        public IRequest SetExecutor(IExecutorProvider executor)
        {
            this.executor = executor;
            return this;
        }

        public IRequest SetId(int id)
        {
            this.id = id;
            return this;
        }

        public void RemoveRequest()
        {
            if (executor != null)
            {
                executor.RemoveRequest(this);
            }
        }

        public List<string> GetReceiver()
        {
            return receiver;
        }

        public IRequest SetResult(ExtResult result)
        {
            this.result = result;
            return this;
        }

        public ExtResult GetResult()
        {
            return result;   
        }

        public object GetData()
        {
            return data;
        }

    }
}
