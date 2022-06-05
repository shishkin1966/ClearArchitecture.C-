using System.Threading;

namespace ClearArchitecture.SL
{
    public class ExecutorProvider : AbsProvider, IExecutorProvider
    {
        public const string NAME = "ExecutorProvider";
        public const int ACTION_NOTHING = -1;
        public const int ACTION_DELETE = 0;
        public const int ACTION_IGNORE = 1;

        private readonly Secretary<IRequest> requests = new();

        new public void OnRegister()
        {
            base.OnRegister();

            ThreadPool.SetMaxThreads(8, 4);
        }

        public void CancelAll()
        {
            foreach(IRequest request in requests.Values())
            {
                request.SetCanceled();
            }
            requests.Clear();
        }

        public void CancelRequests(string sender)
        {
            if (string.IsNullOrEmpty(sender)) return;

            foreach (IRequest request in requests.Values())
            {
                if (request.GetSender() == sender)
                {
                    request.SetCanceled();
                    requests.Remove(request.GetName());
                }
            }
        }

        public void CancelRequest(string sender, string requestName)
        {
            if (string.IsNullOrEmpty(sender)) return;

            if (string.IsNullOrEmpty(requestName)) return;

            foreach (IRequest request in requests.Values())
            {
                if (request.GetSender() == sender && request.GetName() == requestName)
                {
                    request.SetCanceled();
                    requests.Remove(request.GetName());
                }
            }
        }

        public override int CompareTo(IProvider other)
        {
            if (other is IExecutorProvider)
            { return 0; }
            else
            { return 1; }
        }

        public override string GetName()
        {
            return NAME;
        }

        new public bool IsValid()
        {
            return true;
        }

        public void PutRequest(IRequest request)
        {
            if (request.IsSingle() && requests.ContainsKey(request.GetName()))
            {
                return;
            }

            if (request.IsDistinct() && requests.ContainsKey(request.GetName()))
            {
                foreach(IRequest oldRequest in requests.Values())
                {
                    if (oldRequest.GetName() == request.GetName())
                    {
                        var action = request.GetAction(oldRequest);
                        if (action == ACTION_DELETE)
                        {
                            oldRequest.SetCanceled();
                        }
                    }
                }
            }
            request.SetExecutor(this);
            requests.Put(request.GetName(), request);
            ExecuteRequest(request);
        }

        public void RemoveRequest(IRequest request)
        {
            if (request == null) return;

            requests.Remove(request.GetName());
        }

        static private void ExecuteRequest(IRequest request)
        {
            if (request == null) return;

            ThreadPool.QueueUserWorkItem(request.Execute, request);
        }

        new public void Stop()
        {
            base.Stop();

            CancelAll();
        }
    }
}
