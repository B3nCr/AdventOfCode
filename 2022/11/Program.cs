
using _11;

var lines = File.ReadAllLines("input.txt");

var monkeys = new List<Monkey>();
for (int i = 0; i < lines.Length; i += 7)
{
    monkeys.Add(new Monkey(lines[(i)..(i + 6)]));
}

foreach (var monkey in monkeys)
{
    Console.WriteLine($"Monkey {monkey.Index} Count {string.Join(',', monkey.Items)}");
}

Console.WriteLine();

var counts = new List<ulong>() { 0, 0, 0, 0, 0, 0 };
for (int i = 0; i < 10000; i++)
{
    foreach (var monkey in monkeys)
    {
        //Console.WriteLine($"Monkey {monkey.Index} Count {string.Join(',', monkey.Items)}");

        foreach (var item in monkey.InspectItems())
        {
            var pass = item.Item2 % (monkeys.Select(x => x.Divisor).Aggregate<ulong>((x, y) => x * y));

            monkeys[item.Item1].Items.Add(pass);
        }

        //Console.WriteLine($"Monkey {monkey.Index} Count {string.Join(',', monkey.Items)}");
    }

    foreach (var monkey in monkeys)
    {
        Console.WriteLine($"Monkey {monkey.Index} {monkey.Inspected}");
    }

    Console.WriteLine();
}

Console.WriteLine("The end");
var sortedRes = monkeys.OrderByDescending(x => x.Inspected);
foreach (var monkey in sortedRes)
{
    Console.WriteLine($"Monkey {monkey.Index} {monkey.Inspected}");
}

Console.WriteLine(sortedRes.ElementAt(0).Inspected * sortedRes.ElementAt(1).Inspected);

//Console.WriteLine($"{3 * 4 * 5 * 6}");

//Console.WriteLine($"{new[] { 4 * 3 * 5 * 6 }.Aggregate((x, y) => x * y)}");


