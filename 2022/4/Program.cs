

var input = File.ReadAllLines("input.txt");

var count = 0;
for(int i = 0; i < input.Length; i++)
{
    var cleanupDuty = new WorkDuty(input[i]);
    if (cleanupDuty.ContainsContainedSection())
    {
        count++;
    }
    Console.WriteLine(cleanupDuty);
}

Console.WriteLine(count);

