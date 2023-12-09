using System.Net.NetworkInformation;
using System.Text.Json;

class Day9 : Solution
{
    public string Part1()
    {
        var contents = File.ReadAllLines("Inputs/Day9.in");
        var histories = new List<List<int>>();
        histories = contents.Select(l => l.Split(' ').Select(int.Parse).ToList()).ToList();
        var ans = 0;
        foreach (var h in histories)
        {
            var next = getNextReading(h);
            ans += h.Last() + next;
        }
        return $"{ans}";
    }

    private int getNextReading(List<int> h)
    {
        if (h.All(a => a == 0))
        {
            return 0;
        }
        var differences = h.Skip(1).Select((x, i) => x - h[i]).ToList();
        return differences.Last() + getNextReading(differences);
    }

    public string Part2()
    {
        var contents = File.ReadAllLines("Inputs/Day9.in");
        var histories = new List<List<int>>();
        histories = contents.Select(l => l.Split(' ').Select(int.Parse).ToList()).ToList();
        var ans = 0;
        foreach (var h in histories)
        {
            var prev = getPrevValue(h);
            ans += prev;
        }
        return $"{ans}";
    }

    private int getPrevValue(List<int> h)
    {
        if (h.All(a => a == 0))
        {
            return 0;
        }
        var differences = h.Skip(1).Select((x, i) => x - h[i]).ToList();
        var prev = getPrevValue(differences);
        return h.First() - prev;
    }
}