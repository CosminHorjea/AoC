using System.Numerics;

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

    public string Part2()
    {
        var lines = File.ReadAllLines("Inputs/Day5.test");
        var seeds_ranges = lines[0].Split(": ")[1].Split(" ").Select(BigInteger.Parse).ToList();
        var seeds = new List<BigInteger>();
        for (var i = seeds_ranges[0]; i < seeds_ranges[0] + seeds_ranges[1]; i++)
        {
            seeds.Add(i);
        }
        for (var i = seeds_ranges[2]; i < seeds_ranges[2] + seeds_ranges[3]; i++)
        {
            seeds.Add(i);
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
        Console.WriteLine("number of rule maps" + rulesLists.Count);

        var locations = new List<BigInteger>();
        foreach (var seed in seeds)
        {
            locations.Add(getLocation(seed, rulesLists));
        }
        Console.WriteLine(locations.Min());
        return "";
    }
}