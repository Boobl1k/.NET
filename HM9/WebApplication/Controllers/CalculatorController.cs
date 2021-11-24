using System;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
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
        public IActionResult Calculate(string expressionString)
        {
            string AddPluses(string str) =>
                str.Aggregate(new StringBuilder(), (builder, c) => builder.Append(c switch
                {
                    ' ' => "+",
                    '-' => builder.Length is not 0 && !"()*/+-".Contains(builder[^1]) ? "+-" : "-",
                    _ => c.ToString()
                })).ToString();

            expressionString = AddPluses(expressionString);
            Console.WriteLine();
            Console.WriteLine($"полечено выражение:\n\t{expressionString}");

            var expression = ExpressionCalculator.FromString(expressionString);
            var res1 = ExpressionCalculator.ExecuteSlowly(expression);
            Console.WriteLine($"результат через ExpressionCalculator:\n\t{res1?.ToString(CultureInfo.InvariantCulture) ?? "ошибка"}");
            return Ok(res1?.ToString(CultureInfo.InvariantCulture) ?? "ошибка");
        }
    }
}
