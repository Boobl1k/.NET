using System;

namespace CalculatorTask
{
    public static class Parser
    {
        public static Calculator.Operation ParseCalculatorOperation(string arg) =>
            arg switch
            {
                "+" => Calculator.Operation.Plus,
                "-" => Calculator.Operation.Minus,
                "*" => Calculator.Operation.Multiply,
                "/" => Calculator.Operation.Divide,
                _ => default
            };

        public static bool TryParsOrQuit(string str, out int result)
        {
            if (int.TryParse(str, out result)) return true;
            Console.WriteLine($"value is not int. The value was {str}");
            return default;
        }
    }
}