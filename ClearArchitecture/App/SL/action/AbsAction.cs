namespace ClearArchitecture.SL
{
    public abstract class AbsAction : IAction
    {
        private bool isRun = false;

        protected AbsAction()
        {
        }

        protected AbsAction(string v)
        {
        }

        public bool IsRun()
        {
            return isRun;
        }

        public void SetRun()
        {
            isRun = true;
        }
    }
}
