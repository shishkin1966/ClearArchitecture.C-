using ClearArchitecture.SL;

namespace ConsoleApp1.App
{
    public class TestPool : AbsPool<TestObj>
    {
        public const string NAME = "TestPool";

        public TestPool() : base(NAME, 16)
        {
        }

        public override int CompareTo(IProvider other)
        {
            if (other.GetName() == NAME)
            { return 0; }
            else
            { return 1; }
        }

        public override TestObj ObjectFactory()
        {
            return new TestObj();
        }

    }
}
