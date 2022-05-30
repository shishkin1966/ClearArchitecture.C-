using System;
using System.Text;

namespace ClearArchitecture.SL
{
    public class ExtError
    {
        private readonly StringBuilder errorText = new();
        private string sender;

        public string GetErrorText()
        {
            if (errorText.Length == 0)
            {
                return null;
            }
            else
            {
                return errorText.ToString();
            }
        }

        public ExtError AddError(string sender, string error)
        {
            if (string.IsNullOrEmpty(error)) return this;

            this.sender = sender;
            errorText.Append(error);
            return this;
        }

        public ExtError AddError(string sender, Exception e)  
        {
            if (e == null) return this;

            this.sender = sender;
            errorText.Append(e.ToString());
            return this;
        }

        public bool HasError()
        {
            return (errorText.Length > 0);
        }

        public string GetSender() 
        {
            return sender;
        }

        public ExtError SetSender(string sender)  
        {
            this.sender = sender;
            return this;
        }

    }
}
