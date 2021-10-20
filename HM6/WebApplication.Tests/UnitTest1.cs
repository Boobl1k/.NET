using System;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WebApplication.Tests
{
    [TestClass]
    public class CalculatorTests
    {
        private WebApplicationFactory<Startup> factory;
        private HttpClient client;

        [TestInitialize]
        public void SetFactory()
        {
            factory = new WebApplicationFactory<Startup>();
            client = factory.CreateClient();
        }

        private async Task<decimal> Action(decimal v1, decimal v2, string operation)
        {
            var response = await client.GetAsync($"http://localhost:5000/calc?v1={v1}&v2={v2}&op={operation}");

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

        private async Task<decimal> Sum(decimal v1, decimal v2) => await Action(v1, v2, "sum");
        private async Task<decimal> Dif(decimal v1, decimal v2) => await Action(v1, v2, "dif");
        private async Task<decimal> Mult(decimal v1, decimal v2) => await Action(v1, v2, "mult");
        private async Task<decimal> Div(decimal v1, decimal v2) => await Action(v1, v2, "div");

        private static void CheckEquality(decimal v1, decimal v2) => Assert.IsTrue(Math.Round(v1 - v2) < 0.0001m);

        [TestMethod]
        public async Task Sums()
        {
            CheckEquality(3m, await Sum(1, 2));
            CheckEquality(6m, await Sum(4, 2));
            CheckEquality(123m, await Sum(101, 22));
            CheckEquality(4835m, await Sum(1252, 3583));
        }

        [TestMethod]
        public async Task Difs()
        {
            CheckEquality(-1m, await Dif(1, 2));
            CheckEquality(2m, await Dif(4, 2));
            CheckEquality(79m, await Dif(101, 22));
            CheckEquality(-2331m, await Dif(1252, 3583));
        }

        [TestMethod]
        public async Task Mults()
        {
            CheckEquality(2m, await Mult(1, 2));
            CheckEquality(8m, await Mult(4, 2));
            CheckEquality(2222m, await Mult(101, 22));
            CheckEquality(4485916m, await Mult(1252, 3583));
        }

        [TestMethod]
        public async Task Divs()
        {
            CheckEquality(0.5m, await Div(1, 2));
            CheckEquality(2m, await Div(4, 2));
            CheckEquality(4.590909090909091m, await Div(101, 22));
            CheckEquality(0.3494278537538376m, await Div(1252, 3583));
        }

        [TestMethod]
        public async Task SomeFails()
        {
            var response = await client.GetAsync("http://localhost:5000/calc?v1=1");
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);

            response = await client.GetAsync("http://localhost:5000/calc?v1=1&v2=2&op=qwe");
            Assert.AreEqual((HttpStatusCode) 450, response.StatusCode);
            Assert.AreEqual($"\"{FSLibraryResult.ParserFs.wrongOperation}\"",
                await response.Content.ReadAsStringAsync());

            response = await client.GetAsync("http://localhost:5000/calc?v1=1&v2=0&op=div");
            Assert.AreEqual($"\"{FSLibraryResult.CalculatorFs.devByZero}\"",
                await response.Content.ReadAsStringAsync());
        }
    }
}
