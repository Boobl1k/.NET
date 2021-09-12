using System;

namespace CalculatorTask
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
            if (!int.TryParse(str, out result))
            {
                Console.WriteLine($"value is not int. The value was {str}");
                return true;
            }
            return default;
        }

        private static bool ParseCalculatiorOperation(string arg, out Calculator.Operation operation)
        {
            switch (arg)
            {
                case "+":
                    operation = Calculator.Operation.Plus;
                    break;
                case "-":
                    operation
                    = Calculator.Operation.Minus;
                    break;
                case "*":
                    operation =
                 Calculator.Operation.Multiply;
                    break;
                case "/":
                    operation = Calculator.Operation.Divide;
                    break;
                default:
                    operation = default;
                    return true;
            };
            return false;
        }

        private const int NotEnoughtArgs = 1;
        private const int WrongArgFormat = 2;
        private const int WrongOperation = 3;

        static int Main(string[] args)
        {
            if (CheckArgsLenght(args))
                return NotEnoughtArgs;

            if (TryParsOrQuit(args[0], out var val1) || TryParsOrQuit(args[2], out var val2))
                return WrongArgFormat;

            if (ParseCalculatiorOperation(args[1], out var operation))
                return WrongOperation;

            var result = Calculator.Calculate(val1, operation, val2);
            Console.WriteLine($"Result : {result}");

            return 0;
        }
    }

    static class Calculator
    {
        public enum Operation
        {
            Plus,
            Minus,
            Divide,
            Multiply,
        }

        public static int Calculate(int val1, Operation operation, int val2)
        {
            return operation switch
            {
                Operation.Plus => val1 + val2,
                Operation.Minus => val1 - val2,
                Operation.Multiply => val1 * val2,
                Operation.Divide => val1 / val2,
                _ => 0,
            };
        }
    }
}
