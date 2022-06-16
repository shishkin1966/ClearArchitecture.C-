namespace ClearArchitecture.SL
{
    public class PresenterUnion : AbsSmallUnion, IPresenterUnion
    {
        public const string NAME = "PresenterUnion";

        public override int CompareTo(IProvider other)
        {
            if (other is IPresenterUnion)
            { return 0; }
            else
            { return 1; }
        }

        public override string GetName()
        {
            return NAME;
        }

        public IPresenterSubscriber GetPresenter(string name)
        {
            return base.GetSubscriber(name) as IPresenterSubscriber;

        }
    }
}
