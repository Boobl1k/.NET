using System;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace WebApplication.Tests
{
    public class CalculatorTests
    {
        private readonly WebApplicationFactory<Startup> factory;

        public CalculatorTests(WebApplicationFactory<Startup> factory) =>
            this.factory = factory;

        private async Task<decimal> Action(decimal v1, decimal v2, string operation)
        {
            var client = factory.CreateClient();
            
            var response = await client.GetAsync($"http://localhost:5000/calc?v1={v1}&v2={v2}&op={operation}");
            var result = decimal.Parse(await response.Content.ReadAsStringAsync(), CultureInfo.InvariantCulture);

            return result;
        }

        private async Task<decimal> Sum(decimal v1, decimal v2) => await Action(v1, v2, "sum");
        private async Task<decimal> Dif(decimal v1, decimal v2) => await Action(v1, v2, "dif");
        private async Task<decimal> Mult(decimal v1, decimal v2) => await Action(v1, v2, "milt");
        private async Task<decimal> Div(decimal v1, decimal v2) => await Action(v1, v2, "div");

        [Fact]
        public async Task Sums()
        {
            Assert.Equal(3m, await Sum(1, 2));
            Assert.Equal(6m, await Sum(4, 2));
            Assert.Equal(123m, await Sum(101, 22));
            Assert.Equal(4835m, await Sum(1252, 3583));
        }

        [Fact]
        public async Task Difs()
        {
            Assert.Equal(-1m, await Sum(1, 2));
            Assert.Equal(2m, await Sum(4, 2));
            Assert.Equal(79m, await Sum(101, 22));
            Assert.Equal(-2331m, await Sum(1252, 3583));
        }
        
        [Fact]
        public async Task Mults()
        {
            Assert.Equal(2m, await Sum(1, 2));
            Assert.Equal(8m, await Sum(4, 2));
            Assert.Equal(2222m, await Sum(101, 22));
            Assert.Equal(4485916m, await Sum(1252, 3583));
        }
        
        [Fact]
        public async Task Divs()
        {
            Assert.Equal(0.5m, await Sum(1, 2));
            Assert.Equal(2m, await Sum(4, 2));
            Assert.Equal(4.590909090909091m, await Sum(101, 22));
            Assert.Equal(0.3494278537538376m, await Sum(1252, 3583));
        }
    }
}
