using System;

namespace ClearArchitecture.SL
{
    public class ExtResult : IValidated
    {
        public const int NOT_SEND = -1;
        public const int LAST = -2;

        private object data = null;
        private ExtError error = new();
        private int order = NOT_SEND;
        private string name;
        private int id = 0;

        protected ExtResult(object data)
        {
            this.data = data;
        }

        public object GetData() 
        {
            return data;
        }

        public ExtResult SetData(object data) 
        {
            this.data = data;
            return this;
        }

        public ExtError GetError() {
            return error;
        }

        public ExtResult SetError(ExtError error)
        {
            this.error = error;
            return this;
        }

        public ExtResult SetError(string sender, string error)  
        {
            if (string.IsNullOrEmpty(error)) return this;

            this.error.AddError(sender, error);
            return this;
        }

        public ExtResult SetError(string sender, Exception e)  
        {
            if (e == null) return this;

            error.AddError(sender, e);
            return this;
        }

        public string GetErrorText() 
        {
            return error.GetErrorText();
        }
        public string GetSender() 
        {
            return error.GetSender();
        }

        public bool IsValid()
        {
            return data != null && !error.HasError();
        }

        public bool IsEmpty() 
        {
            return data == null;
        }

        public int GetOrder() 
        {
            return order;
        }

        public ExtResult SetOrder(int order) 
        {
            this.order = order;
            return this;
        }

        public bool HasError()
        {
            return !error.HasError();
        }

        public string GetName() 
        {
            return name;
        }

        public ExtResult SetName(string name)  
        {
            this.name = name;
            return this;
        }
        public int GetId() 
        {
            return id;
        }

        public ExtResult SetId(int id)  
        {
            this.id = id;
            return this;
        }


}
}
