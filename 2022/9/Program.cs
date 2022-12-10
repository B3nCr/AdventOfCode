var lines = File.ReadAllLines("small-input.txt");

var rope = new Rope();

foreach (var line in lines)
{
    rope.Move(line);
}

Console.WriteLine(rope.TailPositions.Count());

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

