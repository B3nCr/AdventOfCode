﻿
namespace _12;

public class Hill
{
    private char[,] hill;
    private bool[,] visited;
    private int[,] distance;

    private (int x, int y) CurrentPosition;
    private (int x, int y) EndPosition;

    public Hill(string[] lines)
    {
        hill = new char[lines.Length, lines[0].Length];
        visited = new bool[lines.Length, lines[0].Length];
        distance = new int[lines.Length, lines[0].Length];

        for (int i = 0; i < lines.Length; i++)
        {
            for (int x = 0; x < lines[0].Length; x++)
            {
                if (lines[i][x] == 'S')
                {
                    CurrentPosition = (x, i);
                    distance[i, x] = 0;
                }
                else
                {
                    distance[i, x] = int.MaxValue;
                }

                if (lines[i][x] == 'E') EndPosition = (x, i);

                hill[i, x] = lines[i][x];
                Console.Write(lines[i][x]);
            }
            Console.WriteLine();

        }

        Console.WriteLine(CurrentPosition);
        Console.WriteLine(EndPosition);
    }

    internal int ShortestRoute()
    {
        //For the current node, consider all of its unvisited neighbors and calculate their
        //tentative distances through the current node. Compare the newly calculated tentative
        //distance to the one currently assigned to the neighbor and assign it the smaller one.
        //For example, if the current node A is marked with a distance of 6, and the edge
        //connecting it with a neighbor B has length 2, then the distance to B through A will
        //be 6 + 2 = 8.If B was previously marked with a distance greater than 8 then change it
        //to 8.Otherwise, the current value will be kept.
        Dictionary<(int, int), int> nextNodes = new Dictionary<(int, int), int>();

        while (CurrentPosition != EndPosition)
        {
            foreach (var neighbourCoords in GetVisitableNeighbours())
            {
                if (visited[neighbourCoords.y, neighbourCoords.x])
                {
                    continue;
                }

                var newDistance = distance[CurrentPosition.y, CurrentPosition.x] + 1;

                if (hill[CurrentPosition.y, CurrentPosition.x] == 'z' && neighbourCoords == EndPosition)
                {
                    PrintDistances();

                    Console.WriteLine("Found the end");
                    return newDistance;
                }

                if (distance[neighbourCoords.y, neighbourCoords.x] >= newDistance)
                {
                    distance[neighbourCoords.y, neighbourCoords.x] = newDistance;
                }

                nextNodes[neighbourCoords] = distance[neighbourCoords.y, neighbourCoords.x];
            }

            if (nextNodes.Count == 0)
            {
                PrintDistances();
                PrintNeighbours(CurrentPosition);

                Console.WriteLine($"Stuck node {CurrentPosition}");
                foreach (var v in GetVisitableNeighbours())
                {
                    Console.WriteLine(v);
                }

                return -2;
            }

            //When we are done considering all of the unvisited neighbors of the current node, mark
            //the current node as visited and remove it from the unvisited set.A visited node will
            //never be checked again(this is valid and optimal in connection with the behavior in
            //step 6.: that the next nodes to visit will always be in the order of 'smallest distance
            //from initial node first' so any visits after would have a greater distance).

            visited[CurrentPosition.y, CurrentPosition.x] = true;

            //If the destination node has been marked visited(when planning a route between two specific
            //nodes) or if the smallest tentative distance among the nodes in the unvisited set is
            //infinity(when planning a complete traversal; occurs when there is no connection between
            //the initial node and remaining unvisited nodes), then stop. The algorithm has finished.

            if (CurrentPosition.x == EndPosition.x && CurrentPosition.y == EndPosition.y)
            {
                Console.WriteLine("found that end");
                return distance[CurrentPosition.y, CurrentPosition.x];
            }

            var closestNextNode = nextNodes.MinBy(x => x.Value) ;

            CurrentPosition = closestNextNode.Key;

            nextNodes.Remove(closestNextNode.Key);
        }


        return -1;
    }

    private IEnumerable<(int x, int y)> GetVisitableNeighbours()
    {
        foreach (var n in GetNeighbours())
        {
            if (IsEnd(n) && hill[CurrentPosition.y, CurrentPosition.x] != 'z')
            {
                continue;
            }

            if (IsStart(CurrentPosition) || hill[n.y, n.x] <= hill[CurrentPosition.y, CurrentPosition.x] + 1)
            {
                yield return n;
            }
        }
    }

    private bool IsEnd((int x, int y) p) => hill[p.y, p.x] == 'E';

    private bool IsStart((int x, int y) p) => hill[p.y, p.x] == 'S';

    private IEnumerable<(int x, int y)> GetNeighbours()
    {
        if (Up != null) yield return Up.Value;
        if (Down != null) yield return Down.Value;
        if (Left != null) yield return Left.Value;
        if (Right != null) yield return Right.Value;
    }

    private void PrintNeighbours((int x, int y) p)
    {
        Console.WriteLine(p);
        for (int y = -1; y < 2; y++)
        {
            for (int x = -1; x < 2; x++)
            {
                Console.Write($"P {(p.x + x, p.y + y)} V {visited[p.y + y, p.x + x]},");
            }
            Console.WriteLine();
        }
    }

    private void PrintDistances()
    {
        for (int i = 0; i < distance.GetLength(0); i++)
        {
            for (int x = 0; x < distance.GetLength(1); x++)
            {
                Console.Write(distance[i, x] == int.MaxValue ? "---" : distance[i, x].ToString("000"));
                Console.Write(',');
            }
            Console.WriteLine();
        }
    }

    private void PrintArray<T>(T[,] array)
    {
        for (int i = 0; i < array.GetLength(0); i++)
        {
            for (int x = 0; x < array.GetLength(1); x++)
            {
                Console.Write(array[i, x]);
                Console.Write(',');
            }
            Console.WriteLine();
        }
    }

    private (int x, int y)? Up => CurrentPosition.y >= 1 ? (CurrentPosition.x, CurrentPosition.y - 1) : null;
    private (int x, int y)? Down => CurrentPosition.y < hill.GetLength(0) - 1 ? (CurrentPosition.x, CurrentPosition.y + 1) : null;
    private (int x, int y)? Left => CurrentPosition.x >= 1 ? (CurrentPosition.x - 1, CurrentPosition.y) : null;
    private (int x, int y)? Right => CurrentPosition.x < hill.GetLength(1) - 1 ? (CurrentPosition.x + 1, CurrentPosition.y) : null;
}