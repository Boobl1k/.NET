using System;
using FSLibrary;

namespace CalculatorTask
{
    public static class Program
    {
        private static bool CheckArgsLength(string[] args)
        {
            if (args.Length >= 3) 
                return false;
            Console.WriteLine($"Program requires 3 args. But there is only {args.Length}");
            return true;
        }

        private const int NotEnoughArgs = 1;
        private const int WrongArgFormat = 2;
        private const int WrongOperation = 3;

        public static int Main(string[] args)
        {
            if (CheckArgsLength(args))
                return NotEnoughArgs;

            if (!ParserFs.TryParsOrQuit(args[0], out var val1) || !ParserFs.TryParsOrQuit(args[2], out var val2))
                return WrongArgFormat;

            var operation = ParserFs.ParseCalculatorOperation(args[1]);
            if (operation == CalculatorFs.Operation.Unassigned)
                return WrongOperation;

            var result = CalculatorFs.Calculate(val1, val2, operation);
            Console.WriteLine($"Result : {result}");

            return 0;
        }
    }
}
