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
            return errorText.ToString();
        }

        public ExtError AddError(string sender, string error)
        {
            if (string.IsNullOrEmpty(error)) return this;

            this.sender = sender;
            errorText.Append(DateTime.Now.ToString("G") + ": " + error);
            return this;
        }

        public ExtError AddError(string sender, Exception e)  
        {
            if (e == null) return this;

            this.sender = sender;
            errorText.Append(DateTime.Now.ToString("G") + ": " + e.Message);
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
