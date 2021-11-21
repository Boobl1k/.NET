using System;
using System.Linq;
using System.Text;
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
                str.Aggregate(new StringBuilder(), (builder, c) => builder.Append(c is ' ' ? '+' : c)).ToString();

            Console.WriteLine();
            Console.WriteLine($"полечено выражение: {AddPluses(expressionString)}");
            var tree = ExpressionNode.FromString(AddPluses(expressionString));
            Console.WriteLine($"пребразовано в: {tree.ToString()[1..^1]}");
            var result = await tree.GetResultAsync();
            Console.WriteLine($"результат вычисления: {result}");
            return Ok(result);
        }
    }
}
