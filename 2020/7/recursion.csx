using System;
using System.IO;
using System.Collections.Generic;

public class Bag
{
    public Bag(string colour)
    {
        Colour = colour;
        Contains = new HashSet<(string, int)>();
    }
    public string Colour { get; set; }
    public HashSet<(string Colour, int Count)> Contains { get; set; }

    public override string ToString()
    {
        return Colour;
    }
}

public class Graph<T>
{
    public Graph() { }
    public Graph(IEnumerable<T> vertices, IEnumerable<Tuple<T, T>> edges)
    {
        foreach (var vertex in vertices)
        {
            AddVertex(vertex);
        }

        foreach (var edge in edges)
        {
            AddEdge(edge);
        }
    }

    public Dictionary<T, HashSet<T>> AdjacencyList { get; } = new Dictionary<T, HashSet<T>>();

    public void AddVertex(T vertex)
    {
        AdjacencyList[vertex] = new HashSet<T>();
    }

    public void AddEdge(Tuple<T, T> edge)
    {
        if (AdjacencyList.ContainsKey(edge.Item1) && AdjacencyList.ContainsKey(edge.Item2))
        {
            AdjacencyList[edge.Item1].Add(edge.Item2);
        }
    }
}

public class Algorithms<T>
{
    public List<T> matchingTs = new List<T>();
    public int count = 0;

    public HashSet<T> DFS(Graph<T> graph, T start, T find)
    {
        var visited = new HashSet<T>();

        if (!graph.AdjacencyList.ContainsKey(start))
        {
            return visited;
        }

        var stack = new Stack<T>();
        stack.Push(start);

        while (stack.Count > 0)
        {
            var vertex = stack.Pop();

            if (visited.Contains(vertex))
            {
                continue;
            }

            visited.Add(vertex);

            foreach (var neighbor in graph.AdjacencyList[vertex])
            {
                if (!visited.Contains(neighbor))
                {
                    stack.Push(neighbor);
                }

                matchingTs.Add(neighbor);
            }
        }

        return visited;
    }

    public int CountBags(Dictionary<string, Bag> graph, Bag start)
    {
        int count = 0;
        WriteLine($"Count bag: {start.Colour}");
        foreach (var bag in start.Contains)
        {
            count += bag.Count;

            WriteLine($"Add Bag: {bag.Colour} Count: {bag.Count} Total: {count}");

            count += bag.Count * CountBags(graph, graph[bag.Colour], output);

            WriteLine($"Running total: {count}");
        }

        return count;
    }

}

public class Parser
{
    public Dictionary<string, Bag> Bags { get; } = new Dictionary<string, Bag>();

    public Parser(string path = "./7/file.txt")
    {
        var file = File.ReadAllLines(path);

        foreach (var row in file)
        {
            var indexOfContains = row.IndexOf("contain ");

            var bag = new Bag(row.Substring(0, indexOfContains - 6));

            var ruleStrings = row.Substring(indexOfContains + 8)
                        .Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var rule in ruleStrings)
            {
                if (rule.Equals("no other bags.")) break;

                var ruleComponents = rule.Split(' ');

                var content = $"{ruleComponents[1]} {ruleComponents[2]}";

                bag.Contains.Add(new(content, int.Parse(ruleComponents[0])));
            }

            Bags.Add(bag.Colour, bag);
        }
    }
}

var parser = new Parser();

var edges = parser.Bags.SelectMany(b => b.Value.Contains.Select(inner => new Tuple<Bag, Bag>(b.Value, parser.Bags[inner.Colour])));

WriteLine($"Verticies: {parser.Bags.Keys.Count}  Edges: {edges.Count()}");

var graph = new Graph<Bag>(parser.Bags.Values, edges);
var find = "shiny gold";
var edge = find;

var algorithms = new Algorithms<Bag>();

algorithms.DFS(graph, parser.Bags["shiny gold"], parser.Bags["shiny gold"]);

WriteLine($"Edge: {edge} Count:{ algorithms.matchingTs.Count}");
WriteLine($"Route: {string.Join(", ", algorithms.matchingTs)}");

int count = algorithms.CountBags(parser.Bags, parser.Bags["shiny gold"]);


WriteLine(count);