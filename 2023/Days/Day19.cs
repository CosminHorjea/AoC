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
        throw new NotImplementedException();
    }
}