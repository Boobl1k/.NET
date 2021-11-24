using System;
using System.Diagnostics;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using WebApplication;
using Xunit;
using Xunit.Abstractions;

namespace TestProject1
{
    public class Integration
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly HttpClient _client;

        public Integration(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            _client = new WebApplicationFactory<Startup>().CreateClient();
        }

        private const string ResponseBody = "https://localhost:5001/calc?expressionString=";

        [Theory, InlineData("1+2+3+4+5", 3, 15), InlineData("2/2", 1, 1), InlineData("(2+1)/2", 2, 1.5)]
        [InlineData("(2+3)/12*7+8*9", 4, 74.916666666666666666666666667), InlineData("1-2+3", 2, 2)]
        [InlineData("-(1+3)", 1, -4), InlineData("-(-1)", 0, 1), InlineData("1/(2+3)", 2, 0.2)]
        public async Task TimeTest(string expression, int timeInSeconds, decimal answer)
        {
            var watch = new Stopwatch();
            watch.Start();
            var response = await _client.GetAsync($"{ResponseBody}{expression}");
            watch.Stop();
            var result = decimal.Parse(await response.Content.ReadAsStringAsync(), NumberStyles.Any,
                CultureInfo.InvariantCulture);
            try
            {
                Assert.True(Math.Abs(result - answer) < 0.00001m);
            }
            catch
            {
                Assert.Equal(answer, result);
            }

            Assert.Equal(timeInSeconds, (int) Math.Round(watch.ElapsedMilliseconds / 1000.0));
        }
    }
}
