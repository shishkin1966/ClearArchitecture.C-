using System;

namespace ClearArchitecture.SL
{
    public class LogProvider : AbsProvider, ILogProvider
    {
        public const string NAME = "LogProvider";

        public void AddError(ExtError error)
        {
            if (error == null) return;

            Console.WriteLine("Ошибка: "+error.GetErrorText());
        }

        public override int CompareTo(IProvider other)
        {
            if (other is ILogProvider)
            { return 0; }
            else
            { return 1; }
        }

        public override string GetName()
        {
            return NAME;
        }
    }
}
