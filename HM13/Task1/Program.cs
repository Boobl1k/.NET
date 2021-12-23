using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<Benchmarks>();

[MaxColumn, MinColumn]
public class Benchmarks
{
    private static readonly Test test = new();
    private static readonly int argument = Random.Shared.Next();

    [Benchmark]
    public void Static() => Test.StaticMethod(argument);

    [Benchmark]
    public void Simple() => test.SimpleMethod(argument);

    [Benchmark]
    public void Virtual() => test.VirtualMethod(argument);

    [Benchmark]
    public void Generic() => test.GenericMethod(argument);

    [Benchmark]
    public void Dynamic() => test.DynamicMethod(argument);

    [Benchmark]
    public void Reflection() => test.ReflectionMethod(argument);
}

internal class TestParent
{
    public static string StaticMethod(int num) => num.ToString();
    public string SimpleMethod(int num) => num.ToString();
    public virtual string VirtualMethod(int num) => num.ToString();
    public string GenericMethod<T>(T num) where T : struct => num.ToString()!;
    public string DynamicMethod(dynamic num) => num.ToString();

    public string ReflectionMethod(int num) =>
        (typeof(TestParent).GetMethod("StaticMethod")!.Invoke(default, new[] {(object) num}) as string)!;
}

internal class Test : TestParent { }
