﻿
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
}

public class Rope
{
    public Rope()
    {
        for (int i = 0; i < 10; i++)
        {
            Points.Add(new Point());
        }
    }

    public List<Point> Points { get; private set; } = new List<Point>();

    public HashSet<Point> TailPositions
    {
        get;
        private set;
    } = new HashSet<Point>();

    public void Move(string move)
    {
        var direction = move[0];
        var distance = int.Parse(move.Substring(1));

        for (int i = 0; i < distance; i++)
        {
            Points[0].Move(direction);

            Console.WriteLine($"Head moved: {Points[0]}");

            for (int t = 1; t < Points.Count; t++)
            {
                var gap = Points[t].GetDistanceTo(Points[t - 1]);
                if (gap > 1)
                {
                    Points[t].MoveTo(Points[t-1].PreviousPosition);
                    if (t == 9)
                    {
                        TailPositions.Add(new Point(Points[t].X, Points[t].Y));
                    }
                    Console.WriteLine($"Tail {t} Moved: {Points[t]}");
                }
                else
                {
                    Console.WriteLine("No more tail moves");
                    break;
                }
            }

            //if (gap > 9)
            //{
            //    Console.WriteLine("too large distance");
            //    //Tail.MoveTo(Head.PreviousPosition);

            //    //TailPositions.Add(new Point(Tail.X, Tail.Y));

            //    //Console.WriteLine($"Tail Position: {Tail}");
            //}
        }

        Console.WriteLine();
    }
}