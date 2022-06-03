namespace ClearArchitecture.SL
{
    public abstract class AbsRequest : IRequest
    {
        private object obj;
        private string sender;
        private string receiver;
        private int id = -1;
        private bool isCancelled = false;
        private IExecutor executor;

        protected AbsRequest(string sender, string receiver, object obj)
        {
            this.sender = sender;
            this.receiver = receiver;
            this.obj = obj;
        }

        public abstract void Execute(object obj);
        public abstract string GetName();

        public int GetAction(IRequest oldRequest)
        {
            return ExecutorProvider.ACTION_DELETE;
        }
        
        public object GetData()
        {
            return obj;
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

        public bool IsValid()
        {
            return true;
        }

        public IRequest SetCanceled()
        {
            isCancelled = true;
            return this;
        }

        public IRequest SetExecutor(IExecutor executor)
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

        public string GetReceiver()
        {
            return receiver;
        }
    }
}
