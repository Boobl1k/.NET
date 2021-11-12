using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Models;

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
                str.Select(c => c is ' ' ? '+' : c)
                    .Aggregate(string.Empty, (c1, c2) => string.Concat(c1, c2));

            Console.WriteLine();
            var tree = ExpressionNode.FromString(AddPluses(expressionString));
            Console.WriteLine($"полечено выражение: {tree}");
            var result = await tree.GetResultAsync();
            Console.WriteLine($"результат вычисления: {result}");
            return Ok(result);
        }
    }
}
