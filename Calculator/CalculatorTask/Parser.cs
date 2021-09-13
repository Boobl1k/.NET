using System;
using System.Collections.Generic;
using System.Text;

namespace CalculatorTask
{
    public static class Parser
    {
        public static bool ParseCalculatiorOperation(string arg, out Calculator.Operation operation)
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
        public static bool TryParsOrQuit(string str, out int result)
        {
            if (!int.TryParse(str, out result))
            {
                Console.WriteLine($"value is not int. The value was {str}");
                return true;
            }
            return default;
        }
    }
}
