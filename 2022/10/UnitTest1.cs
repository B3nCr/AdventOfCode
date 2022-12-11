using Xunit.Abstractions;

namespace _10;

public class UnitTest1
{
    private readonly ITestOutputHelper _testOutputHelper;

    public UnitTest1(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void Test1()
    {
        var lines = File.ReadAllLines("input.txt");
        var intervals = new HashSet<int>(new[] { 20, 60, 100, 140, 180, 220 });

        int clock = 1;
        int register = 1;
        int instructionPointer = 0;

        var line = lines[instructionPointer];

        var commands = line.Split(' ');

        string instruction = commands[0];
        var instructionCount = 0;
        var value = 0;

        var sum = 0;

        if (instruction != "noop") value = int.Parse(commands[1]);

        while (!string.IsNullOrEmpty(line))
        {
            _testOutputHelper.WriteLine($"S Clock: {clock} Register {register} Ptr {instructionPointer} Instruction {instruction} Value {value}");

            if (intervals.Contains(clock))
            {
                _testOutputHelper.WriteLine($"SStrength {clock * register}");

                sum += clock * register;
            }

            if (instruction != "noop")
            {
                instructionCount++;

                if (instructionCount == 2)
                {
                    register += value;

                    instructionCount = 0;
                    instructionPointer++;
                }
            }
            else
            {
                instructionPointer++;
            }

            _testOutputHelper.WriteLine($"E Clock: {clock} Register {register} Ptr {instructionPointer} Instruction {instruction} Value {value}");
            _testOutputHelper.WriteLine("");

            if (instructionPointer < lines.Length)
            {
                line = lines[instructionPointer];
                commands = line.Split(' ');
                instruction = commands[0];

                if (instruction == "noop") value = 0;
                else value = int.Parse(commands[1]);
            }
            else
            {
                line = string.Empty;
            }

            clock++;
        }

        _testOutputHelper.WriteLine($"Sum {sum}");
    }
}
