using System.Net.Http;
using System.Text;
using JetBrains.dotMemoryUnit;
using Microsoft.AspNetCore.Mvc.Testing;
using WebApplication;
using Xunit;
using Xunit.Abstractions;

namespace Task3;

public class UnitTest1
{
    private readonly ITestOutputHelper _writer;
    private readonly HttpClient _client = new WebApplicationFactory<Startup>().CreateClient();

    public UnitTest1(ITestOutputHelper writer)
    {
        _writer = writer;
        DotMemoryUnitTestOutput.SetOutputMethod(_writer.WriteLine);
    }

    [DotMemoryUnit(CollectAllocations = true)]
    [Fact]
    public void Test1()
    {
        var point = dotMemory.Check();
        
        long allocated = 0;
        for (long i = 0; i < 1e5; ++i)
        {
            var str = $"{i}+{i}";
            allocated += Encoding.UTF8.GetBytes(str).Length;
            _client.GetAsync($"https://localhost:5001/calc?expressionString={str}").GetAwaiter().GetResult();
        }

        dotMemory.Check(memory =>
        {
            _writer.WriteLine(memory.GetTrafficFrom(point).CollectedMemory.SizeInBytes.ToString());
            _writer.WriteLine(allocated.ToString());
            Assert.True(memory.GetTrafficFrom(point).CollectedMemory.SizeInBytes >= allocated);
        });
    }
}
