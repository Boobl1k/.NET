using System;

namespace _1st
{
    class Program
    {
        private static bool CheckArgsLenght(string[] args)
        {
            if (args.Length < 3)
            {
                Console.WriteLine($"Progrram requress 3 args. But there is only {args.Length}");
                return true;
            }
            return false;
        }

        private static bool TryParsOrQuit(string str, out int result)
        {
            Console.WriteLine($"value is not int");
            return int.TryParse(str, out result);
        }

        private static int Calculate(int val1, string operation, int val2)
        {
            return operation switch
            {
                "+" => val1 + val2,
                "-" => val1 - val2,
                "*" => val1* val2,
                "/" => val1 / val2,
                _ => 0,
            };
    }

        private const int NotEnoughtArgs = 1;
        private const int WrongArgFormat = 2;

        static int Main(string[] args)
        {
            if (CheckArgsLenght(args))
                return NotEnoughtArgs;

            var operation = args[1];
            if (TryParsOrQuit(args[0], out var val1) || TryParsOrQuit(args[2], out var val2))
                return WrongArgFormat;

            var result = Calculate(val1, operation, val2);
            Console.WriteLine($"Result : {result}");

            return 0;
        }
    }
}
