using System;
using Calc;

namespace ConsoleApp
{
    class ConsApp
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("Введите выражение (ВВОД для выхода) ->");
                string line = Console.ReadLine();
                if (string.IsNullOrEmpty(line))
                    break;
                string value = Calculator.Calculate(line);
                Console.WriteLine("Ответ: {0}", value);
            }
        }
    }
}
