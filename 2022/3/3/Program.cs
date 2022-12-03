using BenchmarkDotNet.Running;

namespace _3;
class Program
{
    static void Main(string[] args)
    {
        var summary = BenchmarkRunner.Run<EmptyClass>();
    }
}