using System;
using IlLibrary;

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
            /*if (CheckArgsLength(args))
                return NotEnoughArgs;

            if (!Parser.TryParsOrQuit(args[0], out var val1) || !Parser.TryParsOrQuit(args[2], out var val2))
                return WrongArgFormat;

            var operation = Parser.ParseCalculatorOperation(args[1]);
            if (operation == default)
                return WrongOperation;

            var result = Calculator.Calculate(val1, val2, operation);
            Console.WriteLine($"Result : {result}");

            return 0;*/
            Console.WriteLine(CalculatorIl.CalculateForCS(3, 4, 1));
            
            return 0;
        }
    }
}
