using System.Numerics;
using System.Text.Json;

class Day5 : Solution
{
    class Rule
    {
        public BigInteger destination { get; set; }
        public BigInteger source { get; set; }
        public BigInteger length { get; set; }

        public Rule(string line)
        {
            var parts = line.Split(" ").Select(BigInteger.Parse).ToList();
            destination = parts[0];
            source = parts[1];
            length = parts[2];
        }
    }
    public string Part1()
    {
        var lines = File.ReadAllLines("Inputs/Day5.in");
        var seeds = lines[0].Split(": ")[1].Split(" ").Select(BigInteger.Parse);
        var rulesLists = new List<List<Rule>>();
        List<string> acc = new List<string>();
        foreach (var line in lines.Skip(2).ToList())
        {
            if (string.IsNullOrEmpty(line))
            {
                rulesLists.Add(parseRules(acc));
                acc.Clear();
            }
            else
            {
                acc.Add(line);
            }
        }
        rulesLists.Add(parseRules(acc));
        Console.WriteLine("number of rule maps" + rulesLists.Count);

        var locations = new List<BigInteger>();
        foreach (var seed in seeds)
        {
            locations.Add(getLocation(seed, rulesLists));
        }
        Console.WriteLine(locations.Min());
        return "";
    }

    private BigInteger getLocation(BigInteger seed, List<List<Rule>> rulesLists)
    {
        var aux = seed;
        foreach (var list in rulesLists)
        {
            aux = getNext(aux, list);
        }
        return aux;
    }

    private BigInteger getNext(BigInteger aux, List<Rule> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (aux < list[i].source)
            {
                //Console.WriteLine($"return {aux}");
                return aux;
            }
            if (list[i].source + list[i].length > aux)
            {
                aux = list[i].destination + aux - list[i].source;
                break;
            }
        }
        //Console.WriteLine($"return {aux}");
        return aux;
    }

    private List<Rule> parseRules(List<string> lines)
    {
        lines = lines.Skip(1).ToList();
        var rules = new List<Rule>();
        foreach (var line in lines)
        {
            rules.Add(new Rule(line));
        }
        rules = rules.OrderBy(r => r.source).ToList();
        return rules;
    }

    class Interval
    {
        public BigInteger start { get; set; }
        public BigInteger end { get; set; }
    }

    public string Part2()
    {
        var lines = File.ReadAllLines("Inputs/Day5.in");
        var seeds_ranges = lines[0].Split(": ")[1].Split(" ").Select(BigInteger.Parse).ToList();
        var seeds_intervals = new List<Interval>();
        for (int i = 0; i < seeds_ranges.Count; i += 2)
        {
            seeds_intervals.Add(new Interval { start = seeds_ranges[i], end = seeds_ranges[i] + seeds_ranges[i + 1] - 1 });
        }
        var rulesLists = new List<List<Rule>>();
        List<string> acc = new List<string>();
        foreach (var line in lines.Skip(2).ToList())
        {
            if (string.IsNullOrEmpty(line))
            {
                rulesLists.Add(parseRules(acc));
                acc.Clear();
            }
            else
            {
                acc.Add(line);
            }
        }
        rulesLists.Add(parseRules(acc));

        var locations = new List<BigInteger>();
        foreach (var seed_interval in seeds_intervals)
        {
            locations.Add(getLocation(seed_interval, rulesLists));
        }
        Console.WriteLine(locations.Min());
        Console.WriteLine(JsonSerializer.Serialize(locations.Select(l => l.ToString()).ToList()));
        locations.Sort();
        Console.WriteLine(JsonSerializer.Serialize(locations.Select(l => l.ToString()).ToList()));
        return "";
        // 50142747 high
    }

    private BigInteger getLocation(Interval seed_interval, List<List<Rule>> rulesLists)
    {
        var intervals = new List<Interval>
        {
            seed_interval
        };
        foreach (var list in rulesLists)
        {
            intervals = getNext(intervals, list);
        }
        return intervals.OrderBy(i => i.start).First().start;
    }

    private List<Interval> getNext(List<Interval> seed_intervals, List<Rule> list)
    {
        var intervals = new List<Interval>();
        var seeds_queue = new Queue<Interval>(seed_intervals);
        while (seeds_queue.Count > 0)
        {
            var seed_interval = seeds_queue.Dequeue();
            var found = false;
            for (int i = 0; i < list.Count; i++)
            {
                var currentIntervl = new Interval { start = list[i].source, end = list[i].source + list[i].length - 1 };
                if (seed_interval.start >= currentIntervl.start && seed_interval.end <= currentIntervl.end)
                {
                    intervals.Add(new Interval
                    {
                        start = list[i].destination + seed_interval.start - list[i].source,
                        end = list[i].destination + seed_interval.end - list[i].source
                    });
                    found = true;
                    break;
                }
                if (seed_interval.start < currentIntervl.start && seed_interval.end > currentIntervl.end)
                {
                    var first_interval = new Interval { start = seed_interval.start, end = currentIntervl.start - 1 };
                    var middle_interval = new Interval
                    {
                        start = list[i].destination + seed_interval.start - list[i].source,
                        end = list[i].destination + currentIntervl.end - list[i].source
                    };
                    var last_interval = new Interval { start = currentIntervl.end + 1, end = seed_interval.end };
                    intervals.Add(middle_interval);
                    seeds_queue.Enqueue(first_interval);
                    seeds_queue.Enqueue(last_interval);
                    found = true;
                    break;
                }
                if (seed_interval.start < currentIntervl.start && seed_interval.end > currentIntervl.start)
                {
                    var first_interval = new Interval { start = seed_interval.start, end = currentIntervl.start - 1 };
                    var middle_interval = new Interval
                    {
                        start = list[i].destination + currentIntervl.start - list[i].source,
                        end = list[i].destination + seed_interval.end - list[i].source
                    };
                    intervals.Add(middle_interval);
                    seeds_queue.Enqueue(first_interval);
                    found = true;
                    break;
                }
                if (seed_interval.start < currentIntervl.end && seed_interval.end > currentIntervl.end)
                {
                    var middle_interval = new Interval
                    {
                        start = list[i].destination + seed_interval.start - list[i].source,
                        end = list[i].destination + currentIntervl.end - list[i].source
                    };
                    var last_interval = new Interval { start = currentIntervl.end + 1, end = seed_interval.end };
                    intervals.Add(middle_interval);
                    seeds_queue.Enqueue(last_interval);
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                intervals.Add(seed_interval);
            }
        }
        return intervals;
    }
}