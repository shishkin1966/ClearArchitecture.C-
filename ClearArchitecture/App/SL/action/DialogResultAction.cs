namespace ClearArchitecture.SL
{
    public class DialogResultAction : AbsAction
    {
        private readonly int id;

        public DialogResultAction(int id)
        {
            this.id = id;
        }

        public int GetId()
        {
            return id;
        }
    }
}
