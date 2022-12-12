
using _11;

var lines = File.ReadAllLines("small-input.txt");

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

var counts = new List<int>() { 0, 0, 0, 0, 0, 0 };
for (int i = 0; i < 10000; i++)
{
    foreach (var monkey in monkeys)
    {
        //Console.WriteLine($"Monkey {monkey.Index} Count {string.Join(',', monkey.Items)}");

        foreach (var item in monkey.InspectItems())
        {
            monkeys[item.Item1].Items.Add(item.Item2);
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




