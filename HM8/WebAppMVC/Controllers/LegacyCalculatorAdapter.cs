using System;
using FSLibraryResult;
using WebAppMVC.Models;

namespace WebAppMVC.Controllers
{
    public class LegacyCalculatorAdapter : ICalculator
    {
        public readonly Exception CalculationException = new("cant calculate");

        /// <summary>
        /// операции вида: plus, minus, mult, div
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public decimal Calculate(CalculatorArgs args)
        {
            var operationParsed = args.Op switch
            {
                "plus" => ParserFs.parseCalculatorOperation("+"),
                "minus" => ParserFs.parseCalculatorOperation("-"),
                "mult" => ParserFs.parseCalculatorOperation("*"),
                "div" => ParserFs.parseCalculatorOperation("/"),
                _ => ParserFs.parseCalculatorOperation(default)
            };
            var result = CalculatorFs.calculate(
                ParserFs.parseDecimal(args.Val1),
                ParserFs.parseDecimal(args.Val2),
                operationParsed);
            return result.IsOk ? result.ResultValue : throw CalculationException;
        }
    }
}
