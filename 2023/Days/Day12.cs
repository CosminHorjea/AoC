
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text.Json;

class Day12 : Solution
{
    public string Part1()
    {
        var content = File.ReadAllLines("Inputs/Day12.test");
        var ans = 0;
        foreach (var line in content)
        {
            var damaged = line.Split(" ")[0];
            var nums = line.Split(" ")[1].Split(",").Select(int.Parse).ToList();
            ans += getWays(damaged, nums);
            Console.WriteLine($"{damaged} {getWays(damaged, nums)}");
        }
        return $"{ans}";
    }

    private int getWays(string damaged, List<int> nums)
    {
        if (damaged.Any(a => a == '?'))
        {
            var pos = damaged.IndexOf('?');
            var aux = damaged.Substring(0, pos) + '.' + damaged.Substring(pos + 1);
            var aux2 = damaged.Substring(0, pos) + '#' + damaged.Substring(pos + 1);
            return getWays(aux, nums) + getWays(aux2, nums);
        }
        else
        {
            return isValid(damaged, nums);
        }
    }

    private int isValid(string damaged, List<int> nums)
    {
        var parts = damaged.Split(".", StringSplitOptions.RemoveEmptyEntries);
        if (parts.Count() != nums.Count)
        {
            return 0;
        }
        for (int i = 0; i < nums.Count; i++)
        {
            if (parts[i].Length != nums[i])
            {
                return 0;
            }
        }
        return 1;
    }

    public string Part2()
    {
        var content = File.ReadAllLines("Inputs/Day12.test");
        var ans = 0;
        foreach (var line in content)
        {
            var damaged = line.Split(" ")[0];
            // repeat string 5 times
            // damaged = damaged + "?" + damaged + "?" + damaged + "?" + damaged + "?" + damaged;

            var nums = line.Split(" ")[1].Split(",").Select(int.Parse).ToList();
            // var aux = new List<int>(nums);
            // nums.AddRange(aux);
            // nums.AddRange(aux);
            // nums.AddRange(aux);
            // nums.AddRange(aux);
            Console.WriteLine($"{damaged}|{JsonSerializer.Serialize(nums)}|{nums.Count}");
            ans += countWays(damaged, nums, 0);

        }
        return $"{ans}";
    }

    private int countWays(string damaged, List<int> nums, int currLen)
    {
        if (currLen > nums[0])
        {
            return 0;
        }
        var aux = new List<int>(nums);
        if (currLen == nums[0])
        {
            aux.RemoveAt(0);

        }
        // Console.WriteLine($"{damaged} {JsonSerializer.Serialize(nums)} {currLen}");
        if (nums.Count == 0 && damaged.Length == 0)
        {
            return 1;
        }

        if (damaged.Length == 0 || nums.Count == 0)
        {
            return 0;
        }

        var curr = damaged[0];
        if (curr == '#')
        {

            return countWays(damaged.Substring(1), aux, currLen + 1);
        }
        if (curr == '.')
        {

            return countWays(damaged.Substring(1), aux, 0);
        }
        if (curr == '?')
        {
            var emptyCase = '.' + damaged.Substring(1);
            var fillCase = '#' + damaged.Substring(1);
            return countWays(emptyCase, aux, currLen) + countWays(fillCase, aux, currLen);
        }
        return 0;

    }
}