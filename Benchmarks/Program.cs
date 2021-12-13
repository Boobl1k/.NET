using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Running;
using Microsoft.AspNetCore.Mvc.Testing;
using WebApplication;



BenchmarkRunner.Run<SimpleBenchmark>();

[SimpleJob(RunStrategy.Throughput, targetCount: 10, id: "MonitoringJob")]
public class SimpleBenchmark
{
    private readonly WebApplicationFactory<Startup> _csFac = new();
    private readonly HttpClient _csClient;
    private readonly WebApplicationFactory<WebApplicationFs.Startup> _fsFac = new();
    private readonly HttpClient _fsClient;

    public SimpleBenchmark()
    {
        _csClient = _csFac.CreateClient();
        _fsClient = _fsFac.CreateClient();
    }

    private static void Plus(HttpClient client) =>
        client.GetAsync("https://localhost:5001/calc?expressionString=1+2").GetAwaiter().GetResult();

    [Benchmark]
    public void PlusCs() =>
        Plus(_csClient);

    [Benchmark]
    public void PlusFs() =>
        Plus(_fsClient);

    private static void Expr(HttpClient client) =>
        client.GetAsync("https://localhost:5001/calc?expressionString=(2+3)/12*7+8*9");

    [Benchmark]
    public void ExprCs() =>
        Expr(_csClient);

    [Benchmark]
    public void ExprFs() =>
        Expr(_fsClient);
}
