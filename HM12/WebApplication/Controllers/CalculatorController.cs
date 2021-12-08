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
        expressionString = ExpressionStringFix.Fix(expressionString);

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

        return Ok(result.ToString(CultureInfo.InvariantCulture));
    }
}
