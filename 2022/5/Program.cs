﻿var input = File.ReadAllLines("input.txt");

var currentMode = Mode.instructions;

var instructions = new Stack<Instruction>();
List<Stack<char>> stacks = null;
var columnCount = -1;

for (int i = input.Length - 1; i >= 0; i--)
{
    if (string.IsNullOrEmpty(input[i]))
    {
        currentMode = Mode.columnCount;
        continue;
    }

    if (currentMode == Mode.instructions)
    {
        instructions.Push(new Instruction(input[i]));
    }

    if (currentMode == Mode.columnCount)
    {
        var columns = input[i];

        columnCount = columns
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .Max();

        Console.WriteLine(columnCount);

        currentMode = Mode.columns;

        stacks = new List<Stack<char>>(columnCount);
        for (int s = 0; s < columnCount; s++)
        {
            stacks.Add(new Stack<char>());
        }

        continue;
    }

    if (currentMode == Mode.columns)
    {
        Console.WriteLine("Reading stacks");
        var columns = input[i];

        Console.WriteLine(columns);

        for (int c = 0; c < columnCount; c++)
        {
            var character = columns.ToCharArray()[c * 4 + 1];
            if (character == ' ') continue;

            Console.WriteLine(character);

            stacks[c].Push(character);
        }
    }
}

foreach (var stack in stacks)
{
    Console.WriteLine(stack.Count);
}

foreach (var instruction in instructions)
{
    for (int i = 0; i < instruction.Count; i++)
    {
        var crate = stacks[instruction.From - 1].Pop();
        stacks[instruction.To - 1].Push(crate);
    }
}

foreach (var stack in stacks)
{
    Console.Write(stack.Peek());
}