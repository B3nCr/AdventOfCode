public class Tree
{
    public Tree(int x, int y, int height)
    {
        X = x;
        Y = y;
        Height = height;
    }

    public int X { get; }
    public int Y { get; }
    public int Height { get; }

    public override string ToString()
    {
        return Height.ToString();
    }
}

public class Forest
{
    Tree[][] Trees { get; set; }// = new Tree[lines.Length][];

    public Forest(string[] lines)
    {
        Trees = new Tree[lines.Length][];

        foreach (var line in lines.Select((x, i) => new { i, x }))
        {

            Trees[line.i] = line.x.Select((y, yi) => new Tree(line.i, yi, y - '0')).ToArray();

            //Console.WriteLine(Trees[line.i]);
        }


        //Console.WriteLine(Trees);

    }

    public int CountVisibleTress()
    {
        var visibleTrees = 0;

        for (int i_row = 1; i_row < Trees[0].Length-1; i_row++)
        {
            for (int i_col = 1; i_col < Trees.Length-1; i_col++)
            {
                var left = Trees[i_row][..i_col].Max(x => x.Height);
                if (left < Trees[i_row][i_col].Height)
                {
                    visibleTrees++;
                    continue;
                }
                var right = Trees[i_row][i_col..].Max(x => x.Height);
                if (right < Trees[i_row][i_col].Height)
                {
                    visibleTrees++;
                    continue;
                }

                var column = Enumerable.Range(0, Trees.GetLength(1)).Select(x => Trees[x][i_col]).ToArray();

                var top = column[i_row..].Max(x => x.Height);
                if (top < Trees[i_row][i_col].Height)
                {
                    visibleTrees++;
                    //continue;
                }
                //var bottom = Trees[i_row..][i_col].Max(x => x.Height);
                //if (bottom < Trees[i_row][i_col].Height)
                //{
                //    visibleTrees++;
                //    continue;
                //}


                Console.WriteLine($"i_row { i_row}, i_col {i_col}, val {Trees[i_row][i_col]}, max {left} {right} {top}");
                //Trees[i_row..].ToList().ForEach(Console.WriteLine);

                //Console.WriteLine(Trees[i_row][i_col]);
            }
            Console.WriteLine();

        }
        Console.WriteLine(visibleTrees);

        return visibleTrees;

    }

}