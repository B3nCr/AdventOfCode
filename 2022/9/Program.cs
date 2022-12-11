var lines = File.ReadAllLines("small-input.txt");

var rope = new Rope();
var i = 1;
foreach (var line in lines)
{
    Console.WriteLine($"Move {i} {line}");

    rope.Move(line);

    i++;

    rope.Draw();

    Console.WriteLine();
}

Console.WriteLine(rope.TailPositions.Count());

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

