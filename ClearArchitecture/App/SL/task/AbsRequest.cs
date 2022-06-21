using System.Collections.Generic;

namespace ClearArchitecture.SL
{
    public abstract class AbsRequest : IRequest
    {
        private readonly object _data = null;
        private readonly string _sender;
        private readonly List<string> _receiver = new();
        private int id = -1;
        private bool isCancelled = false;
        private IExecutorProvider executor = default;
        private ExtResult result = default;

        protected AbsRequest(string sender, string receiver, object data)
        {
            _sender = sender;
            if (!string.IsNullOrEmpty(receiver))
            {
                _receiver.Add(receiver);
            }
            _data = data;
        }

        protected AbsRequest(string sender, List<string> receivers, object data)
        {
            _sender = sender;
            if (receivers != null)
            {
                _receiver.AddRange (receivers);
            }
            this._data = data;
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
            return _sender;
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
            return _receiver;
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
            return _data;
        }

        public IRequest AddReceiver(List<string> receiver)
        {
            if (receiver == null) return this;

            foreach (string rec in receiver)
            {
                if (!this._receiver.Contains(rec))
                {
                    this._receiver.Add(rec);
                }
            }
            return this;
        }
    }
}
