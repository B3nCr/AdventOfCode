using System.Globalization;

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
        return $"X: {X} Y: {Y} Height: {Height}";
    }
}

public class Forest
{
    Tree[,] Trees { get; set; }// = new Tree[lines.Length][];

    public Forest(string[] lines)
    {
        Trees = new Tree[lines.Length, lines[0].Length];

        for (int x = 0; x < lines.Length; x++)
        {
            for (int y = 0; y < lines[0].Length; y++)
            {
                //Console.Write(lines[x][y]);

                var tree = new Tree(x, y, CharUnicodeInfo.GetDecimalDigitValue(lines[x][y]));
                Trees[x, y] = tree;
                //Console.WriteLine(tree);
            }
            //Console.WriteLine();
        }

        //Trees.GetRow(0).ToList().ForEach(Console.WriteLine);

    }

    public int CountVisibleTress()
    {
        var visibleTrees = 0;

        for (int i_row = 1; i_row < Trees.GetLength(0) - 1; i_row++)
        {
            var row = Trees.GetRow(i_row);

            for (int i_col = 1; i_col < Trees.GetLength(1) - 1; i_col++)
            {
                var column = Trees.GetColumn(i_col);

                var left = row[..i_col].Max(x => x.Height);
                var height = Trees[i_row, i_col].Height;

                if (left < height)
                {
                    visibleTrees++;
                    continue;
                }

                var right = row[(i_col + 1)..].Max(x => x.Height);
                if (right < height)
                {
                    visibleTrees++;
                    continue;
                }



                var top = column[..i_row].Max(x => x.Height);
                if (top < height)
                {
                    visibleTrees++;
                    continue;
                }
                var bottom = column[(i_row + 1)..].Max(x => x.Height);
                if (bottom < height)
                {
                    visibleTrees++;
                    continue;
                }


                //Console.WriteLine($"i_row {i_row}, i_col {i_col}, val {Trees[i_row][i_col]}, max {left} {right} {top}");
                //Trees[i_row..].ToList().ForEach(Console.WriteLine);

                //Console.WriteLine(Trees[i_row, i_col].Height);
            }
            //Console.WriteLine/*(*/);

        }

        return visibleTrees + ((Trees.GetLength(0) * 2) + ((Trees.GetLength(1) - 2) * 2));
    }

    public int GetSenicScore()
    {
        var senicScore = 0;

        for (int i_row = 1; i_row < Trees.GetLength(0) - 1; i_row++)
        {
            var row = Trees.GetRow(i_row);


            for (int i_col = 1; i_col < Trees.GetLength(1) - 1; i_col++)
            {
                var tree = Trees[i_row, i_col];

                var column = Trees.GetColumn(i_col);
                List<Tree[]> sightLines = new List<Tree[]>
                {
                    row[..i_col].Reverse().ToArray(),
                    row[(i_col + 1)..],
                    column[..i_row].Reverse().ToArray(),
                    column[(i_row + 1)..]
                };

                var scores = new[] { 0, 0, 0, 0 };

                for (int l = 0; l < sightLines.Count; l++)
                {
                    var line = sightLines[l];

                    for (var i = 0; i < line.Length; i++)
                    {
                        scores[l] = i + 1;

                        if (line[i].Height >= tree.Height)
                        {
                            break;
                        }
                    }
                }

                Console.WriteLine($"{scores[2]}{scores[0]}{scores[1]}{scores[3]}");
                Console.WriteLine();
                var treeResult = scores[2] * (scores[0]) * (scores[1]) * scores[3];
                Console.WriteLine(treeResult);
                if (treeResult > senicScore) { senicScore = treeResult; }
                Console.WriteLine();
            }
        }

        return senicScore;
    }

}

public static class CustomArray
{
    public static T[] GetColumn<T>(this T[,] matrix, int columnNumber)
    {
        return Enumerable.Range(0, matrix.GetLength(0))
                .Select(x => matrix[x, columnNumber])
                .ToArray();
    }

    public static T[] GetRow<T>(this T[,] matrix, int rowNumber)
    {
        return Enumerable.Range(0, matrix.GetLength(1))
                .Select(x => matrix[rowNumber, x])
                .ToArray();
    }
}