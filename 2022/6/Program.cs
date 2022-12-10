

// See https://aka.ms/new-console-template for more information
using System.Reflection;

var input = GetInputFromFile("input.txt").Split(Environment.NewLine);

const int window_size = 14;

foreach (var line in input)
{
    for (var i = 0; i < line.Length - window_size; i++)
    {
        var window = line.Substring(i, window_size);
        if(window.Distinct().Count() == window_size)
        {
            Console.WriteLine(window);
            Console.WriteLine(i+ window_size);
            break;
        }
    }
}

Console.WriteLine(input.Length);


static string GetInputFromFile(string filename)
{
    using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"_6.{filename}");
    using var streamReader = new StreamReader(stream);

    var contents = streamReader.ReadToEnd();

    return contents;
}