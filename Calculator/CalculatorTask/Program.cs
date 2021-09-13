using System;

namespace CalculatorTask
{
    public class Program
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

        private const int NotEnoughtArgs = 1;
        private const int WrongArgFormat = 2;
        private const int WrongOperation = 3;

        public static int Main(string[] args)
        {
            if (CheckArgsLenght(args))
                return NotEnoughtArgs;

            if (Parser.TryParsOrQuit(args[0], out var val1) || Parser.TryParsOrQuit(args[2], out var val2))
                return WrongArgFormat;

            if (Parser.ParseCalculatiorOperation(args[1], out var operation))
                return WrongOperation;

            var result = Calculator.Calculate(val1, val2, operation);
            Console.WriteLine($"Result : {result}");

            return 0;
        }
    }
}
