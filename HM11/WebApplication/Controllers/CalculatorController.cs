using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplication.Models;

namespace WebApplication.Controllers;

public class CalculatorController : Controller
{
    // пробел считает за '+', поэтому нельзя использовать пробелы
    [HttpGet, Route("calc")]
    public IActionResult Calculate(
        [FromServices] ExceptionHandler exceptionHandler,
        [FromServices] ExpressionsCache cache,
        [FromServices] ICachedCalculator calculator,
        string expressionString)
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

        Expression expression;
        try
        {
            expression = calculator.FromString(expressionString);
        }
        catch (Exception e)
        {
            exceptionHandler.DoHandle(LogLevel.Error, e);
            return BadRequest();
        }

        var stopwatch = new Stopwatch();
        stopwatch.Start();
        decimal result;
        try
        {
            result = calculator.CalculateWithCache(expression, cache);
        }
        catch (Exception e)
        {
            exceptionHandler.DoHandle(LogLevel.Error, e);
            return BadRequest();
        }
        stopwatch.Stop();
        Console.WriteLine(
            $"результат через ExpressionCalculator:\n\t{result.ToString(CultureInfo.InvariantCulture)}");
        return Ok(result.ToString(CultureInfo.InvariantCulture) +
                  $" заняло времени: {stopwatch.ElapsedMilliseconds} миллисекунд");
    }
}
