using System.Text;

public class WorkDuty
{
    public WorkDuty(string input)
    {
        var sections = input.Split(',');
        FirstSection = new Section(sections.First());
        SecondSection = new Section(sections.Last());
    }

    public Section FirstSection { get; }
    public Section SecondSection { get; }


    public override string ToString()
    {
        var b = new StringBuilder();

        var limit = new[] { FirstSection.End, SecondSection.End }.Max(x => x);

        b.Append($"{FirstSection.ToStringWithLimit(limit)}\n{SecondSection.ToStringWithLimit(limit)}");
        b.AppendLine();
        b.Append($"Contained: {ContainsContainedSection()}");

        return b.ToString();
    }

    public bool ContainsContainedSection()
    {
        if (FirstSection.Start <= SecondSection.Start && FirstSection.End >= SecondSection.End)
        {
            return true;
        }
        if (SecondSection.Start <= FirstSection.Start && SecondSection.End >= FirstSection.End)
        {
            return true;
        }
        return false;
    }
}

public class Section
{
    public Section(string section)
    {
        var sections = section.Split('-').Select(x => int.Parse(x));
        Start = sections.First();
        End = sections.Last();
    }

    public int Start { get; }
    public int End { get; }

    public string ToStringWithLimit(int limit)
    {
        var b = new StringBuilder();

        for (int i = 0; i <= limit; i++)
        {
            if (i < Start || i > End)
            {
                b.Append('.');
            }
            else
            {
                b.Append(i);
            }
        }

        return b.ToString();
    }
}

