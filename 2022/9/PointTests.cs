namespace _9;

public class PointTests
{
    [Theory]
    [InlineData(0, 0, 0, 1)]
    [InlineData(0, 0, 1, 1)]
    [InlineData(0, 0, 1, 0)]
    [InlineData(0, 0, 1, -1)]
    [InlineData(0, 0, 0, -1)]
    [InlineData(0, 0, -1, -1)]
    [InlineData(0, 0, -1, 0)]
    public void Touching(params int[] points)
    {
        var point1 = new Point(points[0], points[1]);
        var point2 = new Point(points[2], points[3]);

        Assert.True(point1.IsTouching(point2));
    }

    [Theory]
    [InlineData(0, 0, 0, 2)] // A
    [InlineData(0, 0, 1, 2)] // B
    [InlineData(0, 0, 2, 0)] // C
    [InlineData(0, 0, 2, -1)] // D
    [InlineData(0, 0, 0, -2)] // E
    [InlineData(0, 0, -1, -2)] // F
    [InlineData(0, 0, -2, 0)] // G
    public void NotTouching(params int[] points)
    {
        // -2 -1 0 1 2
        //  .  . A B . 2
        //  .  . . . . 1
        //  E  . X . C 0
        //  F  . . . D -1
        //  .  . G . . -2

        var point1 = new Point(points[0], points[1]);
        var point2 = new Point(points[2], points[3]);

        Assert.False(point1.IsTouching(point2));
    }

    [Fact]
    public void IsInRow()
    {
        var p1 = new Point(0, 0);
        var p2 = new Point(0, 1);

        Assert.True(p1.IsInRow(p2));
        Assert.False(p1.IsInColumn(p2));
    }

    [Theory]
    [InlineData(0, 0, 0, 0, 0, 0)]
    //[InlineData(0, 0, 0, 1, 0, 0)]
    [InlineData(0, 0, 0, 2, 0, 1)]
    [InlineData(0, 0, 0, -2, 0, -1)]
    [InlineData(0, 0, 2, 0, 1, 0)]
    [InlineData(0, 0, -2, 0, -1, 0)]
    [InlineData(0, 0, 2, 2, 1, 1)]
    [InlineData(0, 0, -2, -2, -1, -1)]
    public void GetOffset(params int[] args)
    {
        var point1 = new Point(args[0], args[1]);
        var point2 = new Point(args[2], args[3]);

        Assert.Equal<int>(args[4], point2.Offset(point1).x);
        Assert.Equal<int>(args[5], point2.Offset(point1).y);
    }
}
