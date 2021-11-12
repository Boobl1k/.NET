using WebAppMVC.Controllers;

namespace WebAppMVC.Models
{
    public interface ICalculator
    {
        decimal Calculate(CalculatorArgs args);
    }
}
