

var input = File.ReadAllLines("input.txt");

var containedCount = 0;
var overlapdCount = 0;
for(int i = 0; i < input.Length; i++)
{
    var cleanupDuty = new WorkDuty(input[i]);
    if (cleanupDuty.ContainsContainedSection())
    {
        containedCount++;
    }
    if (cleanupDuty.ContainsOverlap())
    {
        overlapdCount++;
    }
    Console.WriteLine(cleanupDuty);
}

Console.WriteLine($"Contained: {containedCount}\nOverlap: {overlapdCount}");

