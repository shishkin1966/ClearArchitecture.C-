using System;
using System.Text;

namespace ClearArchitecture.SL
{
    public abstract class AbsSubscriber : ISubscriber
    {
        private bool isBusy = false;
        private string name;
        private readonly StringBuilder comment = new();

        protected AbsSubscriber()
        {
        }

        protected AbsSubscriber(string name) : base()
        {
            this.name = name;
        }

        public string GetName()
        {
            return name;
        }

        public void SetName(string name)
        {
            this.name = name;
        }

        public bool IsValid()
        {
            return true;
        }

        public bool IsBusy()
        {
            return isBusy;
        }

        public void SetBusy()
        {
            isBusy = true;
        }

        public void SetUnBusy()
        {
            isBusy = false;
        }

        public void AddComment(string comment)
        {
            this.comment.Append(DateTime.Now.ToString("G") + ": " + comment);
        }

        public string GetComment()
        {
            return comment.ToString();
        }
    }
}
