using System;
using System.Collections.Generic;
using System.Text;

namespace CalculatorTask
{
    public static class Calculator
    {
        public enum Operation
        {
            Plus,
            Minus,
            Divide,
            Multiply,
        }

        public static int Calculate(int val1, int val2, Operation operation)
        {
            return operation switch
            {
                Operation.Plus => val1 + val2,
                Operation.Minus => val1 - val2,
                Operation.Multiply => val1 * val2,
                Operation.Divide => val1 / val2
            };
        }
    }
}
