
namespace _12;

public class Hill
{
    private char[,] hill;
    private bool[,] visited;
    private int[,] distance;

    private (int x, int y) CurrentPosition;
    private (int x, int i) EndPosition;

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
                    distance[i, x] = -1;
                }

                if (lines[i][x] == 'E') EndPosition = (x, i);

                hill[i, x] = lines[i][x];
                Console.Write(lines[i][x]);
            }
            Console.WriteLine();
        }
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

        for (int i = 0; i < hill.GetLength(0); i++)
        {
            for (int x = 0; x < hill.GetLength(1); x++)
            {

                foreach (var neigbourCoords in GetNeighbours())
                {
                    if (!visited[neigbourCoords.y, neigbourCoords.x])
                    {
                        if (hill[neigbourCoords.y, neigbourCoords.x] < hill[CurrentPosition.y, CurrentPosition.x] + 1)
                        {
                            // don't consider nodes we can't reach
                            continue;
                        }

                        if (hill[neigbourCoords.y, neigbourCoords.x] > hill[CurrentPosition.y, CurrentPosition.x])
                        {
                            if (distance[neigbourCoords.y, neigbourCoords.x] == -1) distance[neigbourCoords.y, neigbourCoords.x] = 0;
                            distance[neigbourCoords.y, neigbourCoords.x] = distance[CurrentPosition.y, CurrentPosition.x] + 1;
                        }

                    }
                }

                PrintArray(visited);
                Console.WriteLine();
                PrintArray(distance);

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

                if (CurrentPosition == EndPosition) return distance[CurrentPosition.y, CurrentPosition.x];

                //Otherwise, select the unvisited node that is marked with the smallest tentative distance,
                //set it as the new current node, and go back to step 3.

                (int x, int y) minPos = (-1, -1);
                int minDist = -1;
                foreach (var position in GetNeighbours())
                {
                    if (minDist == -1 || distance[position.y, position.x] < minDist)
                    {
                        minPos = position;
                    }
                }

                CurrentPosition = minPos;
            }
        }

        return -1;
    }

    private IEnumerable<(int x, int y)> GetNeighbours()
    {
        if (Up != null) yield return Up.Value;
        if (Down != null) yield return Down.Value;
        if (Left != null) yield return Left.Value;
        if (Right != null) yield return Right.Value;
    }

    private void PrintArray<T>(T[,] array)
    {
        for (int i = 0; i < array.GetLength(0); i++)
        {
            for (int x = 0; x < array.GetLength(1); x++)
            {
                Console.Write(array[i, x]);
            }
            Console.WriteLine();
        }
    }

    private (int x, int y)? Up => CurrentPosition.y > 1 ? (CurrentPosition.y - 1, CurrentPosition.x) : null;
    private (int x, int y)? Down => CurrentPosition.y < hill.GetLength(0) - 1 ? (CurrentPosition.y + 1, CurrentPosition.x) : null;
    private (int x, int y)? Left => CurrentPosition.x > 1 ? (CurrentPosition.y, CurrentPosition.x - 1) : null;
    private (int x, int y)? Right => CurrentPosition.x < hill.GetLength(1) ? (CurrentPosition.y, CurrentPosition.x + 1) : null;
}