
using System.Diagnostics;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text.Json;

class Day12 : Solution
{
    public string Part1()
    {
        var content = File.ReadAllLines("Inputs/Day12.in");
        var ans = 0;
        foreach (var line in content)
        {
            var damaged = line.Split(" ")[0];
            var nums = line.Split(" ")[1].Split(",").Select(int.Parse).ToList();
            ans += getWays(damaged, nums);
            // Console.WriteLine($"{damaged} {getWays(damaged, nums)}");
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
        var content = File.ReadAllLines("Inputs/Day12.in");
        BigInteger ans = 0;
        foreach (var line in content)
        {
            var damaged = line.Split(" ")[0];
            // repeat string 5 times
            damaged = damaged + "?" + damaged + "?" + damaged + "?" + damaged + "?" + damaged;

            var nums = line.Split(" ")[1].Split(",").Select(int.Parse).ToList();
            var aux = new List<int>(nums);
            nums.AddRange(aux);
            nums.AddRange(aux);
            nums.AddRange(aux);
            nums.AddRange(aux);
            // Console.WriteLine($"{damaged}|{JsonSerializer.Serialize(nums)}|{nums.Count}");
            Dictionary<(string, int, int), BigInteger> dp = new Dictionary<(string, int, int), BigInteger>();
            var x = countWays(damaged, nums, 0, dp);
            // Console.WriteLine($"{damaged} | {x}");
            ans += x;
        }
        return $"{ans}";
    }

    private BigInteger countWays(string damaged, List<int> nums, int currLen, Dictionary<(string, int, int), BigInteger> dp)
    {
        if (dp.Keys.Contains((damaged, nums.Count, currLen)))
        {
            return dp[(damaged, nums.Count, currLen)];
        }
        // Console.WriteLine($"{damaged} {JsonSerializer.Serialize(nums)} {currLen}");
        if (nums.Count == 0)
        {
            // if (!damaged.Contains("#"))
            // Console.WriteLine("Found a solution");
            dp.Add((damaged, nums.Count, currLen), damaged.Contains('#') ? 0 : 1);
            return dp[(damaged, nums.Count, currLen)];
        }
        if (string.IsNullOrEmpty(damaged) && nums.Count > 0)
        {
            if (currLen == nums[0] && nums.Count == 1)
            {
                // Console.WriteLine("Found a solution");
                dp.Add((damaged, nums.Count, currLen), 1);
                return dp[(damaged, nums.Count, currLen)];
            }
            dp.Add((damaged, nums.Count, currLen), 0);
            return 0;
        }
        if (currLen > nums[0])
        {
            dp.Add((damaged, nums.Count, currLen), 0);
            return 0;
        }
        var aux = new List<int>(nums);
        var curr = damaged[0];
        if (curr == '#')
        {
            dp.Add((damaged, nums.Count, currLen), countWays(damaged.Substring(1), aux, currLen + 1, dp));
            return dp[(damaged, nums.Count, currLen)];
        }
        if (curr == '.')
        {
            if (currLen == nums[0])
            {
                aux.RemoveAt(0);
            }
            if (currLen > 0 && currLen != nums[0])
            {
                return 0;
            }
            dp.Add((damaged, nums.Count, currLen), countWays(damaged.Substring(1), aux, 0, dp));
            return dp[(damaged, nums.Count, currLen)];
        }
        if (curr == '?')
        {
            var emptyCase = '.' + damaged.Substring(1);
            var fillCase = '#' + damaged.Substring(1);
            dp.TryAdd((emptyCase, nums.Count, currLen), countWays(emptyCase, aux, currLen, dp));
            dp.TryAdd((fillCase, nums.Count, currLen), countWays(fillCase, aux, currLen, dp));
            return dp[(emptyCase, aux.Count, currLen)] + dp[(fillCase, aux.Count, currLen)];
        }
        return 0;

    }
}
// 1678777900 low
// 6512849198636