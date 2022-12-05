
using System.Text.RegularExpressions;

public class Instruction
{
    static Regex regex = new Regex("move (\\d+) from (\\d+) to (\\d+)", RegexOptions.Compiled);
    public int Count { get; }
    public int From { get; }
    public int To { get; }

    public Instruction(string instruction)
    {
        var result = regex.Match(instruction);

        Count = int.Parse(result.Groups[1].Value);
        From = int.Parse(result.Groups[2].Value);
        To = int.Parse(result.Groups[3].Value);
    }
    public override string ToString()
    {
        return $"move {Count} from {From} to {To}";
    }
}

public class Crate
{
    public Crate(string crate)
    {
        Char = crate[1];
    }

    public char Char { get; }
}

enum Mode
{
    instructions,
    columnCount,
    columns
};