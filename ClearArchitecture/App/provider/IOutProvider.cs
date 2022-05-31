using ClearArchitecture.SL;

namespace ConsoleApp1.App
{
    public interface IOutProvider : IProvider
    {
        void WriteLine(string line);
    }
}
