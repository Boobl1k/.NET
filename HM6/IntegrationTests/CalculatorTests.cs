using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using WebApplication;

namespace IntegrationTests
{
    [TestFixture]
    public class CalculatorTests
    {
        [Test]
        public async Task Test1()
        {
            var hostBuilder = new WebApplicationFactory<Startup>();
            
            var httpClient = hostBuilder.CreateClient();

            var response = await httpClient.GetAsync("http://localhost:5000/calc?v1=1&v2=2&op=sum");

            var t = await response.Content.ReadAsStringAsync();
            
            Assert.AreEqual(3m,  decimal.Parse(t, CultureInfo.InvariantCulture));
        }
    }
}
