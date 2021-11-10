using System;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using WebAppMVC;
using WebAppMVC.Controllers;
using Xunit;

namespace TestProject1
{
    public class UnitTest1
    {
        private LegacyCalculatorAdapter _calculator = new LegacyCalculatorAdapter();
        [Theory]
        [InlineData(1, "plus", 2, 3)]
        [InlineData(3, "plus", 2, 5)]
        [InlineData(12.4, "minus", 2.6, 9.8)]
        public void Calc(decimal val1, string operation, decimal val2, decimal result)
        {
            var actual = _calculator.Calculate(new CalculatorArgs
            {
                Val1 = val1.ToString(),
                Val2 = val2.ToString(),
                Op = operation
            });
            Assert.Equal(result, actual);
        }
    }

    public class IntegrationTest
    {
        private WebApplicationFactory<Startup> factory;
        private HttpClient client;
        
        public IntegrationTest()
        {
            factory = new WebApplicationFactory<Startup>();
            client = factory.CreateClient();
        }

        private async Task<decimal> Action(decimal v1, decimal v2, string operation)
        {
            var response = await client.GetAsync($"http://localhost:5000/calc?val1={v1}&val2={v2}&op={operation}");

            var strNumber = await response.Content.ReadAsStringAsync();
            decimal parsed;
            try
            {
                parsed = decimal.Parse(strNumber, CultureInfo.InvariantCulture);
            }
            catch
            {
                throw new Exception($"cant parse, the number is {strNumber}");
            }

            return parsed;
        }

        private async Task<decimal> Sum(decimal v1, decimal v2) => await Action(v1, v2, "plus");
        private async Task<decimal> Dif(decimal v1, decimal v2) => await Action(v1, v2, "minus");
        private async Task<decimal> Mult(decimal v1, decimal v2) => await Action(v1, v2, "mult");
        private async Task<decimal> Div(decimal v1, decimal v2) => await Action(v1, v2, "div");

        private static void CheckEquality(decimal v1, decimal v2) => Assert.True(Math.Round(v1 - v2) < 0.0001m);

        [Theory]
        [InlineData(1, 2)]
        [InlineData(3, 2)]
        [InlineData(12.4, 2.6)]
        public async Task Calc(decimal val1, decimal val2)
        {
            CheckEquality(await Sum(val1, val2), val1 + val2);
            CheckEquality(await Dif(val1, val2), val1 - val2);
            CheckEquality(await Mult(val1, val2), val1 * val2);
            CheckEquality(await Div(val1, val2), val1 / val2);
        }
    }
}
