using System;
using FSLibraryResult;

namespace StarterConsole
{
    public static class Program
    {
        public static readonly Exception NotEnoughArgs = new Exception("not enough arguments");
        
        public static int Main(string[] args)
        {
            if (args.Length < 3)
                throw NotEnoughArgs;
            
            var num1Res = ParserFs.ParseInt(args[0]);
            var num2Res = ParserFs.ParseInt(args[2]);
            var operationRes = ParserFs.ParseCalculatorOperation(args[1]);

            if (num1Res.IsError)
                throw new Exception(num1Res.ErrorValue);
            if (num2Res.IsError)
                throw new Exception(num2Res.ErrorValue);
            if (operationRes.IsError)
                throw new Exception(operationRes.ErrorValue);

            var num1 = num1Res.ResultValue;
            var num2 = num2Res.ResultValue;
            var operation = operationRes.ResultValue;

            var calculationRes = CalculatorFs.Calculate(num1, num2, operation);

            if (calculationRes.IsError)
                throw new Exception(calculationRes.ErrorValue);

            var result = calculationRes.ResultValue;
            
            Console.WriteLine($"result is {result}");
            return 0;
        }
    }
}
