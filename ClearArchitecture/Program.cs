using ClearArchitecture.SL;
using System;

namespace ConsoleApp1
{
    static class Program
    {

        static void Main(string[] args)
        {
            Secretary<String> s = new();

            s.Put("1", "1");
            s.Put("2", "2");

            // Тест
            Console.WriteLine(s.Size().ToString());
            Console.WriteLine(s.GetValue("1"));
            if (s.GetValue("5") == default(String))
            {
                Console.WriteLine("Error");
            }

        }
    }
}
