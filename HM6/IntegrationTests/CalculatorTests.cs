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
            var httpClient = new HttpClient();

            var response = await httpClient.GetAsync("http://localhost:5000/calc?v1=1&v2=2&op=sum");

            var t = await response.Content.ReadAsStringAsync();
            
            Assert.AreEqual('3',  t[0]);
        }
    }
}
