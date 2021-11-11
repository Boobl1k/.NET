using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAppMVC.Models;

namespace WebAppMVC.Controllers
{
    public class CalculatorController : Controller
    {
        //https://localhost:5001/calc?val1=2&val2=3&op=mult
        [HttpGet, Route("calc")]
        public IActionResult Calc([FromServices] ICalculator calculator, [FromQuery] CalculatorArgs args)
        {
            try
            {
                return Ok(calculator.Calculate(args));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new ObjectResult(e.Message)
                {
                    StatusCode = 450
                };
            }
        }
    }
    
    public class CalculatorArgs
    {
        public string Val1 { get; set; }
        public string Val2 { get; set; }
        public string Op { get; set; }
    }
}
