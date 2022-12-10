
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

    public int GetDistance(Point to)
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
    public Point Head
    {
        get;
        private set;
    } = new Point();

    public Point Tail
    {
        get;
        private set;
    } = new Point();

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
            Head.Move(direction);
            var gap = Head.GetDistance(Tail);

            Console.Write($"Head Position: {Head} ");

            if (gap > 1)
            {
                //Console.WriteLine("Too far mate");
                Tail.MoveTo(Head.PreviousPosition);
                TailPositions.Add(new Point(Tail.X, Tail.Y));
                Console.WriteLine($"Tail Position: {Tail}");
            }
        }
    }
}