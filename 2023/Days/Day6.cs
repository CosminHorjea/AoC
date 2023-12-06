using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;

class Day6 : Solution
{
    public string Part1()
    {
        var contents = File.ReadAllLines("Inputs/day6.in");
        var times = contents[0].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(int.Parse).ToList();
        var distance = contents[1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(int.Parse).ToList();
        int n = times.Count;
        int ans = 1;
        for (int i = 0; i < n; i++)
        {
            int aux = 0;
            for (int t = 0; t < times[i]; t++)
            {
                // Console.WriteLine($"hold: {t}, distance {getDistance(t, times[i])}, with total time {times[i]}");
                if (getDistance(t, times[i]) > distance[i])
                {
                    aux++;
                }
            }
            ans *= aux;
        }
        return $"{ans}";
    }

    public BigInteger getDistance(BigInteger holdTime, BigInteger totalTime)
    {
        return holdTime * (totalTime - holdTime);
    }

    public string Part2()
    {
        BigInteger time = 44707080;
        BigInteger distance = 283113411341491;
        // var time = 71530;
        // var distance = 940200;
        BigInteger lower_bound = 0;
        BigInteger higher_bound = distance;
        BigInteger ans = 0;
        BigInteger l = 0, r = time;
        while (l <= r)
        {
            BigInteger m = l + (r - l) / 2;
            if (getDistance(m, time) > distance && getDistance(m - 1, time) <= distance)
            {
                lower_bound = m;
                break;
            }
            if (getDistance(m, time) > distance)
            {
                r = m - 1;
            }
            else
            {
                l = m + 1;
            }
        }
        Console.WriteLine(lower_bound);
        l = 0;
        r = time;
        while (l < r)
        {
            BigInteger m = l + (r - l) / 2;
            if (getDistance(m, time) > distance && getDistance(m + 1, time) <= distance)
            {
                higher_bound = m;
                break;
            }
            if (getDistance(m, time) > distance)
            {
                l = m + 1;
            }
            else
            {
                r = m - 1;
            }
        }
        Console.WriteLine(higher_bound);
        Console.WriteLine($"{getDistance(lower_bound, time)}, {distance}");
        Console.WriteLine($"{getDistance(lower_bound + 1, time)}, {distance}");
        Console.WriteLine($"{getDistance(lower_bound - 1, time)}, {distance}");
        Console.WriteLine($"{getDistance(higher_bound, time)}, {distance}");
        Console.WriteLine($"{getDistance(higher_bound + 1, time)}, {distance}");
        Console.WriteLine($"{getDistance(higher_bound - 1, time)}, {distance}");
        ans = higher_bound - lower_bound + 1;
        return $"{ans}";
    }
}

// Part 1: 219849
// 7637313
// 283113411341491
// 283113403704179 -- to high
// Part 2: 0
