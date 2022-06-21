using ClearArchitecture.SL;
using System;

namespace ConsoleApp1.App
{
    public class TestPool : AbsPool<TestObj>
    {
        public TestPool() : base("TestPool", 16)
        {
        }

        public override int CompareTo(IProvider other)
        {
            if (other.GetName() == "TestPool")
            { return 0; }
            else
            { return 1; }
        }

        public override TestObj ObjectFactory()
        {
            return new TestObj();
        }

        public override string ToString()
        {
            string s = string.Format("Pool: {0} {1} {2}", GetName(), this.Capacity, this.Count);
            return s;
        }
    }
}
