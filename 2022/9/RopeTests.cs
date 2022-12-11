using System;
namespace _9
{
    public class RopeTests
    {
        [Fact]
        public void TestAllMoves()
        {
            var lines = File.ReadAllLines("input.txt");

            var rope = new Rope();
            var tailPositions = 1;

            foreach (var line in lines)
            {
                Console.WriteLine($"Move {tailPositions} {line}");

                tailPositions += rope.Move(line);
            }

            Assert.Equal(36, rope.TailPositionCount);
        }

        [Fact]
        public void TestSmallRope()
        {
            var rope = new Rope(10);

            rope.Move("R 4");

            Assert.Equal(new Point(4, 0), rope.Points[0]);
            Assert.Equal(new Point(3, 0), rope.Points[1]);
            Assert.Equal(new Point(2, 0), rope.Points[2]);
            Assert.Equal(new Point(1, 0), rope.Points[3]);
            Assert.Equal(new Point(0, 0), rope.Points[4]);
            Assert.Equal(new Point(0, 0), rope.Points[5]);

            Assert.Equal(1, rope.TailPositionCount);

            rope.Move("U 1");

            //......
            //......
            //......
            //....H.
            //4321..

            Assert.Equal(new Point(4, 1), rope.Points[0]);
            Assert.Equal(new Point(3, 0), rope.Points[1]);
            Assert.Equal(new Point(2, 0), rope.Points[2]);
            Assert.Equal(new Point(1, 0), rope.Points[3]);
            Assert.Equal(new Point(0, 0), rope.Points[4]);
            Assert.Equal(new Point(0, 0), rope.Points[5]);

            Assert.Equal(1, rope.TailPositionCount);

            rope.Move("U 1");

            //......
            //......
            //....H.2
            //.4321.1
            //5.....0
            //012345

            Assert.Equal(new Point(4, 2), rope.Points[0]);
            Assert.Equal(new Point(4, 1), rope.Points[1]);
            Assert.Equal(new Point(3, 1), rope.Points[2]);
            Assert.Equal(new Point(2, 1), rope.Points[3]);
            Assert.Equal(new Point(1, 1), rope.Points[4]);
            Assert.Equal(new Point(0, 0), rope.Points[5]);

            Assert.Equal(1, rope.TailPositionCount);

            rope.Move("U 1");

            /*
            * ......
            * ....H.
            * ....1.
            * .432..
            * 5.....
            */

            Assert.Equal(new Point(4, 3), rope.Points[0]);
            Assert.Equal(new Point(4, 2), rope.Points[1]);
            Assert.Equal(new Point(3, 1), rope.Points[2]);
            Assert.Equal(new Point(2, 1), rope.Points[3]);
            Assert.Equal(new Point(1, 1), rope.Points[4]);
            Assert.Equal(new Point(0, 0), rope.Points[5]);

            Assert.Equal(1, rope.TailPositionCount);

            rope.Move("U 1");

            Assert.Equal(1, rope.TailPositionCount);
        }

        [Fact]
        public void MediumInput()
        {

            var rope = new Rope(10);

            rope.Move("R 5");

            Assert.Equal(1, rope.TailPositionCount);

            rope.Move("U 8");

            Assert.Equal(1, rope.TailPositionCount);

            rope.Move("L 8");


            Assert.Equal(4, rope.TailPositionCount);

            rope.Move("D 3");

            Assert.Equal(4, rope.TailPositionCount);

        }
    }
}

