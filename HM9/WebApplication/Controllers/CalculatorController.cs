using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Models;

// ReSharper disable StringLiteralTypo

namespace WebApplication.Controllers
{
    public class CalculatorController : Controller
    {
        /// <summary>
        /// пробел считает за '+', поэтому нельзя использовать пробелы
        /// </summary>
        /// <param name="expressionString"></param>
        /// <returns></returns>
        [HttpGet, Route("calc")]
        public async Task<IActionResult> Calculate(string expressionString)
        {
            string AddPluses(string str) =>
                str.Aggregate(new StringBuilder(), (builder, c) => builder.Append(c is ' ' ? '+' : c)).ToString();

            expressionString = AddPluses(expressionString);
            Console.WriteLine();
            Console.WriteLine($"полечено выражение:\n\t{expressionString}");
            var tree = ExpressionNode.FromString(expressionString);
            Console.WriteLine($"пребразовано в:\n\t{tree.ToString()[1..^1]}");
            var result = await tree.GetResultAsync();
            Console.WriteLine($"результат вычисления:\n\t{result}");

            Console.WriteLine();
            var expression = ExpressionCalculator.FromString(expressionString);
            var exAsFunc = expression.AsFunc();
            Console.WriteLine($"результат через ExpressionCalculator:\n\t{exAsFunc()}");
            return Ok(result);
        }
    }
}
