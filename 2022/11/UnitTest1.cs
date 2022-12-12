namespace _11;

public class MonkeyTests
{
    string[] input = new[]
    {
        "Monkey 0:",
        "  Starting items: 79, 98",
        "  Operation: new = old* 1,9",
        "  Test: divisible by 49",
        "    If true: throw to monkey 2",
        "    If false: throw to monkey 3"
    };

    Monkey sut;

    public MonkeyTests()
    {
        sut = new Monkey(input);
    }

    [Fact]
    public void IndexConfigured()
    {
        Assert.Equal(0, sut.Index);
    }

    [Fact]
    public void ItemsConfigured()
    {
        Assert.Equal(2, sut.Items.Count);
        Assert.Equal<ulong>(79, sut.Items[0]);
        Assert.Equal<ulong>(98, sut.Items[1]);
    }

    [Fact]
    public void GetOperation()
    {
        //"  Operation: new = old* 1,9",
        Assert.Equal(Monkey.Operation.Multiply, sut.Op);
    }

    [Fact]
    public void GetTest()
    {
        Assert.True(sut.Test(1));
    }

    [Theory]
    [InlineData(0, 3)]
    [InlineData(1, 2)]
    public void DestinationMonkey(int index, int expected)
    {
        Assert.Equal(expected, sut.ToMonkey(index));
    }
}

public class Monkey
{
    public int Index { get; private set; }

    public Operation Op { get; private set; }

    private uint Multiplier;
    public List<ulong> Items = new List<ulong>();

    private uint Divisor { get; }
    private int IfTrue { get; }
    private int IfFalse { get; }
    public int Inspected { get; private set; }

    Dictionary<Operation, Func<ulong, ulong, ulong, ulong>> operations = new Dictionary<Operation, Func<ulong, ulong, ulong, ulong>>
    {
        [Operation.Multiply] = (x, y, _) => x * y,
        [Operation.Factor] = (x, _, z) => x * z,
        [Operation.Plus] = (x, y, _) => x + y
    };

    public Monkey(string[] monkey)
    {
        //Monkey 0:
        //  Starting items: 79, 98
        //  Operation: new = old * 19
        //  Test: divisible by 23
        //    If true: throw to monkey 2
        //    If false: throw to monkey 3
        Index = int.Parse(monkey[0].Substring(6, monkey[0].Length - 7));
        Items = new List<ulong>(monkey[1].Substring(18).Split(',').Select(ulong.Parse));

        if (monkey[2].Contains("old * old")) Op = Operation.Factor;
        else
        {
            Op = monkey[2].Contains('*') ? Operation.Multiply : Operation.Plus;
            Multiplier = uint.Parse(monkey[2].Substring(24));
        }


        Divisor = uint.Parse(monkey[3].Substring(21));
        IfTrue = int.Parse(monkey[4].Substring(28));
        IfFalse = int.Parse(monkey[5].Substring(29)); ;
    }

    public bool Test(int index) => TestItem(Items[index]);

    private bool TestItem(ulong item) => item % Divisor == 0;

    internal int ToMonkey(int index) => Test(index) ? IfTrue : IfFalse;

    public IEnumerable<(int monkeyIndex, ulong result)> InspectItems()
    {
        Inspected += Items.Count;

        for (var i = 0; i < Items.Count; i++)
        {
            var item = Items[i];

            var result = operations[Op](item, Multiplier, item);

            //result = result / 3;

            yield return TestItem(result) ? (IfTrue, result) : (IfFalse, result);
        }

        Items.RemoveAll(x => true);
    }


    public enum Operation
    {
        Plus,
        Multiply,
        Factor
    }
}
