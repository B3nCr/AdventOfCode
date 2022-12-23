using Microsoft.VisualStudio.TestPlatform.Utilities;
using Newtonsoft.Json.Linq;
using Xunit.Abstractions;

namespace _13;

public class UnitTest1
{
    private readonly ITestOutputHelper output;

    public UnitTest1(ITestOutputHelper output)
    {
        this.output = output;
    }

    // this solution is an absolute nonsense. 

    [Fact]
    public void Test1()
    {
        var lines = File.ReadAllText("small-input.txt")
            .Split("\n", StringSplitOptions.RemoveEmptyEntries)
            .Select(JToken.Parse)
            .Chunk(2)
            .Select((pair, index) => Compare(pair[0], pair[1]) < 0 ? index + 1 : 0)
            .Sum();

        output.WriteLine($"Lines sum: {lines}");
    }

    [Fact]
    public void Test2()
    {
        var dividers = "[[2]]\n[[6]]".Split("\n").Select(JToken.Parse).ToList();

        var lines = File.ReadAllText("small-input.txt")
            .Split("\n", StringSplitOptions.RemoveEmptyEntries)
            .Select(JToken.Parse)
            .Concat(dividers)
            .ToList();

        lines.Sort(Compare);

        output.WriteLine($"Dividers: {lines.IndexOf(dividers[0])}  {lines.IndexOf(dividers[1])} ");


    }

    int Compare(JToken first, JToken second)
    {
        if (first is JValue && second is JValue)
        {
            return (int)first - (int)second;
        }
        var array1 = first as JArray ?? new JArray((int)first);
        var array2 = second as JArray ?? new JArray((int)second);

        return Enumerable.Zip(array1, array2)
            .Select(p => Compare(p.First, p.Second))
            .FirstOrDefault(c => c != 0, array1.Count - array2.Count);
    }
}