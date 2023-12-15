using System.Runtime.CompilerServices;
using System.Text.Json;

class Day15 : Solution
{
    public string Part1()
    {
        var contents = File.ReadAllText("Inputs/Day15.in");
        var steps = contents.Split(",");
        var ans = 0;
        ans = steps.Select(getHash).Sum();
        return $"{ans}";
    }

    private int getHash(string step)
    {
        int ans = 0;
        foreach (char c in step)
        {
            ans += c;
            ans *= 17;
            ans %= 256;
        }
        return ans;
    }

    public string Part2()
    {
        var contents = File.ReadAllText("Inputs/Day15.in");
        var steps = contents.Split(",");
        var ans = 0;
        Dictionary<int, List<(string, int)>> map = new Dictionary<int, List<(string, int)>>();
        foreach (var step in steps)
        {
            string label;
            int focal_length = 0;
            if (step.Contains('='))
            {
                label = step.Split("=").First();
                focal_length = int.Parse(step.Split("=").ToList()[1]);
            }
            else
            {
                label = step.Split("-").First();
            }
            var box = getHash(label);
            if (step.Contains("="))
            {
                if (!map.Keys.Contains(box))
                    map.Add(box, new List<(string, int)>());
                var itemToRemove = map[box].FirstOrDefault(r => r.Item1 == label);
                if (itemToRemove != default((string, int)))
                {
                    var idx = map[box].FindIndex(r => r.Item1 == label);
                    map[box][idx] = (label, focal_length);
                }
                else
                    map[box].Add((label, focal_length));
            }
            else
            {
                if (!map.Keys.Contains(box))
                    continue;
                var itemToRemove = map[box].FirstOrDefault(r => r.Item1 == label);
                if (itemToRemove != default((string, int)))
                    map[box].Remove(itemToRemove);

            }
        }
        // Console.WriteLine(JsonSerializer.Serialize(map));
        foreach (var key in map.Keys)
        {
            var values = map[key];
            foreach (var item in values.Select((value, i) => new { i, value }))
            {
                ans += (key + 1) * (item.i + 1) * item.value.Item2;
            }
        }
        return $"{ans}";
    }
}