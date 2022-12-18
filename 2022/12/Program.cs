using _12;

var lines = File.ReadAllLines("small-input.txt");
var hill = new Hill(lines);

Console.WriteLine(hill.ShortestRouteFromLowPoints().Min());
