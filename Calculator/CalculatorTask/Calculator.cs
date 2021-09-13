using System;
using System.Collections.Generic;
using System.Text;

namespace CalculatorTask
{
    public static class Calculator
    {
        public static readonly Exception DevByZero = new DivideByZeroException("val2 was 0");
        public static readonly Exception WrongOperation = new ArgumentException("Wrong operation");
        public static readonly Exception OutOfRange = new ArgumentOutOfRangeException($"Operation was out of range");

        public enum Operation
        {
            Unassigned,
            Plus,
            Minus,
            Divide,
            Multiply,
        }

        public static int Calculate(int val1, int val2, Operation operation)
        {
            switch (operation)
            {
                case Operation.Plus:
                    return val1 + val2;
                case Operation.Minus:
                    return val1 - val2;
                case Operation.Multiply:
                    return val1 * val2;
                case Operation.Divide:
                {
                    if (val2 == 0)
                        throw DevByZero;
                    return val1 / val2;
                }
                case Operation.Unassigned:
                    throw WrongOperation;
                default:
                    throw OutOfRange;
            }
        }
    }
}