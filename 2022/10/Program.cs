using _10;

var lines = File.ReadAllLines("input.txt");
var intervals = new HashSet<int>(new[] { 20, 60, 100, 140, 180, 220 });

char[,] crt = new char[6, 40];

for (int x = 0; x < crt.GetLength(0); x += 1)
{
    for (int y = 0; y < crt.GetLength(1); y += 1)
    {
        crt[x, y] = '.';
    }
}

Crt.Render(crt);

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
    //Console.WriteLine($"S Clock: {clock} Register {register} Ptr {instructionPointer} Instruction {instruction} Value {value}");

    if (intervals.Contains(clock))
    {
        sum += clock * register;
    }

    int row = (clock - 1) / 40;
    int pixel = (clock - 1) % 40;
    (int min, int max) window = (register - 1, register + 1);

    Console.WriteLine($"Clock = {clock} Register {register} Min {window.min} Max {window.max} Pixel {pixel} Row {row}");

    // ##..##..##..##..##..##..##..##..##..##..
    // ###...###...###...###...###...###...###.
    // ####....####....####....####....####....
    // #####.....#####.....#####.....#####.....
    // ######......######......######......####
    // #######.......#######.......#######.....

    //##..##..##..##..##..##..##..##..##..##..
    //###...###...###...###...###...###...###.
    //####....####....####....####....####....
    //#####.....#####.....#####.....#####.....
    //######......######......######......####
    //#######.......#######.......#######.....

    if (pixel >= window.min && pixel <= window.max)
    {
        crt[row, pixel] = '#';
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

    //Console.WriteLine($"E Clock: {clock} Register {register} Ptr {instructionPointer} Instruction {instruction} Value {value}");
    //Console.WriteLine("");

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

Crt.Render(crt);
Console.WriteLine($"Sum {sum}");