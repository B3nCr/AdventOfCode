namespace _3;
class Program
{
    static void Main(string[] args)
    {
        var lines = File.ReadAllLines("input.txt");

        var total = 0;
        for (int i = 0; i < lines.Length; i+=3)
        {

            //var mid = lines[i].Length / 2;
            var part1 = lines[i];
            var part2 = lines[i+1];
            var part3 = lines[i + 2];

            var union = part1.Intersect(part2).Intersect(part3);

            Console.WriteLine(union.First());

            total += GetPriority(union.First());

            //Console.WriteLine(union.Count());
            //Console.WriteLine(part1);

        }
        Console.WriteLine(total);
    }

    private static int GetPriority(char itemType)
    {
        if (char.IsUpper(itemType))
        {
            return (int)itemType - 65 + 27;
        }

        return itemType - 96;
    }
}