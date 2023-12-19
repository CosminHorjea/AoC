using System.Numerics;
using System.Runtime.InteropServices.Marshalling;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

class Day19 : Solution
{
    public string Part1()
    {
        Dictionary<string, List<(string, string)>> map = new Dictionary<string, List<(string, string)>>();
        var contents = File.ReadAllLines("Inputs/Day19.in");
        var rules = contents.TakeWhile(c => !string.IsNullOrEmpty(c)).ToList();
        var ans = 0;

        foreach (var rule in rules)
        {
            var label = rule.Split("{")[0];
            map.Add(label, new List<(string, string)>());
            Regex patters = new Regex(@"\{.*?\}");
            var matches = patters.Match(rule);
            var conditions = matches.Value.Trim(['{', '}']).Split(',').ToList();
            for (int i = 0; i < conditions.Count; i++)
            {
                if (conditions[i].Contains(':'))
                {
                    var ineq = conditions[i].Split(':')[0];
                    var dest = conditions[i].Split(':')[1];
                    map[label].Add((ineq, dest));
                }
                else
                {
                    map[label].Add(("_", conditions[i]));
                }
            }
        }

        var pieces = contents.Skip(rules.Count + 1).ToList();
        foreach (var piece in pieces)
        {
            // Console.WriteLine($"The line {piece}");
            var regex = new Regex(@"\d*");
            var values = regex.Matches(piece).Select(c => c.Value).Where(s => !string.IsNullOrEmpty(s)).Select(int.Parse).ToList();
            if (isAccepted(map, values))
            {
                ans += values.Sum();
            }
        }
        return $"{ans}";
    }

    private bool isAccepted(Dictionary<string, List<(string, string)>> map, List<int> values)
    {
        Dictionary<char, int> indices = new() { { 'x', 0 }, { 'm', 1 }, { 'a', 2 }, { 's', 3 } };
        Queue<string> q = new Queue<string>();

        q.Enqueue("in");

        while (q.Count > 0)
        {
            var curr = q.Dequeue();
            if (curr == "A")
            {
                return true;
            }
            if (curr == "R")
            {
                return false;
            }
            var rules = map[curr];

            foreach (var rule in rules)
            {
                // Console.WriteLine(rule);
                if (rule.Item1 == "_")
                {
                    q.Enqueue(rule.Item2);
                }
                else if (rule.Item1.Contains(">"))
                {
                    var num = int.Parse(rule.Item1.Split('>')[1]);
                    if (values[indices[rule.Item1[0]]] > num)
                    {
                        q.Enqueue(rule.Item2);
                        break;
                    }
                }
                else if (rule.Item1.Contains("<"))
                {
                    var num = int.Parse(rule.Item1.Split('<')[1]);
                    if (values[indices[rule.Item1[0]]] < num)
                    {
                        q.Enqueue(rule.Item2);
                        break;
                    }
                }
            }
        }
        return false;
    }


    public string Part2()
    {
        Dictionary<string, List<(string, string)>> map = new Dictionary<string, List<(string, string)>>();
        var contents = File.ReadAllLines("Inputs/Day19.in");
        var rules = contents.TakeWhile(c => !string.IsNullOrEmpty(c)).ToList();
        BigInteger ans = 0;

        foreach (var rule in rules)
        {
            var label = rule.Split("{")[0];
            map.Add(label, new List<(string, string)>());
            Regex patters = new Regex(@"\{.*?\}");
            var matches = patters.Match(rule);
            var conditions = matches.Value.Trim(['{', '}']).Split(',').ToList();
            for (int i = 0; i < conditions.Count; i++)
            {
                if (conditions[i].Contains(':'))
                {
                    var ineq = conditions[i].Split(':')[0];
                    var dest = conditions[i].Split(':')[1];
                    map[label].Add((ineq, dest));
                }
                else
                {
                    map[label].Add(("_", conditions[i]));
                }
            }
        }

        var values = new List<(int, int)> { (1, 4000), (1, 4000), (1, 4000), (1, 4000) };
        Dictionary<char, int> indices = new() { { 'x', 0 }, { 'm', 1 }, { 'a', 2 }, { 's', 3 } };
        Queue<(string, List<(int, int)>)> q = new Queue<(string, List<(int, int)>)>();

        q.Enqueue(("in", values));
        while (q.Count > 0)
        {
            var curr = q.Dequeue();
            if (curr.Item1 == "A")
            {
                BigInteger a = 1;
                foreach (var interval in curr.Item2)
                {
                    Console.Write($"{interval.Item1},{interval.Item2}|");
                    a *= interval.Item2 - interval.Item1 + 1;
                }
                Console.WriteLine();
                ans += a;
                continue;
            }
            if (curr.Item1 == "R")
            {
                continue;
            }
            var labelRules = map[curr.Item1];
            var remainingIntervals = new List<(int, int)>(curr.Item2);
            foreach (var rule in labelRules)
            {
                var next_intervals = new List<(int, int)>(remainingIntervals);
                if (rule.Item1 == "_")
                {
                    q.Enqueue((rule.Item2, remainingIntervals));
                }
                else if (rule.Item1.Contains(">"))
                {
                    var num = int.Parse(rule.Item1.Split('>')[1]);
                    var idx = indices[rule.Item1[0]];
                    next_intervals[idx] = (num + 1, remainingIntervals[idx].Item2);
                    remainingIntervals[idx] = (remainingIntervals[idx].Item1, num);
                    q.Enqueue((rule.Item2, new List<(int, int)>(next_intervals)));
                }
                else if (rule.Item1.Contains("<"))
                {
                    var num = int.Parse(rule.Item1.Split('<')[1]);
                    var idx = indices[rule.Item1[0]];
                    next_intervals[idx] = (remainingIntervals[idx].Item1, num - 1);
                    remainingIntervals[idx] = (num, remainingIntervals[idx].Item2);
                    q.Enqueue((rule.Item2, new List<(int, int)>(next_intervals)));
                }
            }
        }
        return $"{ans}";
    }
}
// test
// 167409079868000 - correct
// 104624836840500
// 104624836840500
// 163494853788000

// input
// 126226266150329 too high