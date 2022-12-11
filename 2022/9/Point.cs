
using System.Globalization;

public class Point
{
    public int X
    {
        get;
        private set;
    } = 0;

    public int Y
    {
        get;
        private set;
    } = 0;

    public Point PreviousPosition { get; private set; }

    public Point()
    {
    }

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }

    public void Move(char direction)
    {
        PreviousPosition = new Point(X, Y);

        switch (direction)
        {
            case 'U':
                Y++;
                break;

            case 'D':
                Y--;
                break;

            case 'L':
                X--;
                break;

            case 'R':
                X++;
                break;
        }
    }


    internal void Move(int x, int y)
    {
        PreviousPosition = new Point(X, Y);

        X += x;
        Y += y;
    }

    public void MoveTo(Point to)
    {
        PreviousPosition = new Point(X, Y);

        X = to.X; Y = to.Y;
    }

    public int GetDistanceTo(Point to)
    {
        var horizontalDistnace = (to.X - X);
        var verticalDistance = (to.Y - Y);

        var xsq = Math.Pow(horizontalDistnace, 2);
        var ysq = Math.Pow(verticalDistance, 2);

        var xsqplusysq = xsq + ysq;

        var result = (int)Math.Round(Math.Sqrt(xsqplusysq));

        //Console.WriteLine($"Distance {result}");

        return result;
    }

    public bool IsTouching(Point p)
    {
        return Math.Abs(X - p.X) <= 1 && Math.Abs(Y - p.Y) <= 1;
    }

    public override string ToString()
    {
        return $"X{X},Y{Y}";
    }

    public override bool Equals(object? obj)
    {
        var point = obj as Point;

        if (point == null) return false;

        return point.X == X && point.Y == Y;
    }

    public override int GetHashCode()
    {
        return X.GetHashCode() + Y.GetHashCode();
    }

    public bool IsInRow(Point p) => X == p.X;

    public bool IsInColumn(Point p) => Y == p.Y;

    public (int x, int y) Offset(Point p)
    {
        var x = 0;
        var y = 0;

        if (!IsInRow(p))
        {
            x = (X - p.X) / Math.Abs(X - p.X);
        }

        if (!IsInColumn(p))
        {
            y = (Y - p.Y) / Math.Abs(Y - p.Y);
        }

        return (x, y);
    }
}

public class Rope
{
    public Rope(int ropeLength = 10)
    {
        for (int i = 0; i < ropeLength; i++)
        {
            Points.Add(new Point());
        }
    }

    public List<Point> Points { get; private set; } = new List<Point>();

    public HashSet<Point> TailPositions
    {
        get;
        private set;
    } = new HashSet<Point>(new[] { new Point(0, 0) });

    public int TailPositionCount => TailPositions.Count;

    public int Move(string move)
    {
        var direction = move[0];
        var distance = int.Parse(move.Substring(1));

        for (int i = 0; i < distance; i++)
        {
            Points[0].Move(direction);

            for (int t = 1; t < Points.Count; t++)
            {
                if (!Points[t].IsTouching(Points[t - 1]))
                {
                    var offset = Points[t - 1].Offset(Points[t]);

                    //sign_x = 0 if hx == tx else (hx - tx) / abs(hx - tx)
                    //sign_y = 0 if hy == ty else (hy - ty) / abs(hy - ty)


                    Points[t].Move(offset.x, offset.y);
                    if (t == 9)
                    {
                        TailPositions.Add(new Point(Points[t].X, Points[t].Y));
                    }
                    //Console.WriteLine($"Tail {t} Moved: {Points[t]}");
                }
                else
                {
                    //Console.WriteLine("No more tail moves");
                    break;
                }
            }
        }

        return TailPositions.Count;
    }

    public void Draw()
    {
        var maxx = Points.Max(x => x.X);
        var minx = Points.Min(x => x.X);
        var maxy = Points.Max(x => x.Y);
        var miny = Points.Min(x => x.Y);

        Console.WriteLine($"{minx}:{maxx}:{miny}:{maxy}");

        char[,] grid = new char[maxy - miny + 1, maxx - minx + 1];

        for (int y = miny; y <= maxy; y++)
        {
            for (int x = minx; x < maxx; x++)
            {
                grid[y, x] = '.';
            }
        }

        for (int i = 0; i < Points.Count; i++)
        {
            var point = Points[i];
            grid[point.Y, point.X] = i.ToString()[0];
        }

        for (int y = miny; y <= maxy; y++)
        {
            for (int x = minx; x < maxx; x++)
            {
                Console.Write(grid[y, x]);
            }
            Console.WriteLine();
        }

        Console.WriteLine();
        Console.WriteLine();
    }
}