using BenchmarkDotNet.Attributes;
using F = System.IO.File;

public class Directory : IHasSize
{
    public Directory(string name)
    {
        Parent = null;
        Name = name;
    }

    public Directory(string name, Directory parent) : this(name)
    {
        Parent = parent;
    }

    public string Name { get; }
    public Directory? Parent { get; }

    public List<IHasSize> Contents { get; } = new List<IHasSize>();

    public int GetSize()
    {
        var size = Contents.Sum(x => x.GetSize());
        return size;
    }

    public override string ToString()
    {
        return $"Name: {Name}. Size {GetSize()}";
    }
}

public class File : IHasSize
{
    public int Size { get; }

    public File(string file)
    {
        Size = int.Parse(file.Split(' ').First());
    }

    public int GetSize()
    {
        return Size;
    }
}

public interface IHasSize
{
    int GetSize();
}

public class Thing
{
    [Benchmark]
    public void RunProgram()
    {

        var lines = F.ReadAllLines("input.txt");

        var allDirectories = new List<Directory>();
        var currentDirectory = new Directory("/");
        var topLevelDir = currentDirectory;
        allDirectories.Add(currentDirectory);

        foreach (var line in lines)
        {
            var firstChar = line[0];

            if (line == "$ ls" || line == "$ cd /") continue;

            if (firstChar == '$')
            {
                if (line == "$ cd ..")
                {
                    if (currentDirectory.Parent == null) throw new Exception();

                    currentDirectory = currentDirectory.Parent;
                    continue;
                }

                if (line.StartsWith("$ cd "))
                {
                    var name = line.Split(' ').Last();

                    var newDirectory = new Directory(name, currentDirectory);

                    currentDirectory.Contents.Add(newDirectory);
                    currentDirectory = newDirectory;
                    allDirectories.Add(newDirectory);

                    continue;
                }
            }

            if (Char.IsDigit(firstChar))
            {
                var file = new File(line);
                currentDirectory.Contents.Add(file);
            }
        }

        //Console.WriteLine("Hello, World!");

        var unused = 70000000 - topLevelDir.GetSize();
        var required = 30000000 - unused;

        Console.WriteLine(allDirectories.Where(x => x.GetSize() >= required).OrderBy(x => x.GetSize()).First());
    }
}