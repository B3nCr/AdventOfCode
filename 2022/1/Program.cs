// See https://aka.ms/new-console-template for more information

var input = File.ReadAllLines("input.txt");

var allElfCalories = new List<int>();
var elf = 0;
var callories = 0;

for (var i =0 ; i < input.Length; i++)
{
    var line = input[i];

    if(string.IsNullOrEmpty(line)){
        allElfCalories.Add(callories);
        // Console.WriteLine(callories);
        callories = 0;
        elf++;
        continue;
    }

    callories += int.Parse(line);
}

// Console.WriteLine(callories);
allElfCalories.Add(callories);

// var max = allElfCalories.Max(x=>x);
var top = allElfCalories.OrderByDescending(x=>x).Take(3).Sum();

Console.WriteLine(top);

Console.WriteLine("Hello, World!");
