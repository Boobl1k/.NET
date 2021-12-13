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
        [FromServices] ExpressionsCache cache,
        [FromServices] ICachedCalculator calculator,
        string expressionString)
    {
        expressionString = ExpressionStringFix.Fix(expressionString);

        var expression = calculator.FromString(expressionString);

        var result = calculator.CalculateWithCache(expression, cache);

        return Ok(result.ToString(CultureInfo.InvariantCulture));
    }
}
