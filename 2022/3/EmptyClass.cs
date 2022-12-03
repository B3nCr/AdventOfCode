using System;
using BenchmarkDotNet.Attributes;

namespace _3
{
	public class EmptyClass
	{
        [Benchmark]
		public int GetPriproty()
		{
            var lines = File.ReadAllLines("input.txt");

            var total = 0;
            for (int i = 0; i < lines.Length; i += 3)
            {
                var part1 = lines[i];
                var part2 = lines[i + 1];
                var part3 = lines[i + 2];

                var union = part1.Intersect(part2).Intersect(part3);

                total += GetPriority(union.First());

            }
            return total;
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
}

