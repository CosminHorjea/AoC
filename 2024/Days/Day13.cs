using System.Globalization;
using System.Numerics;
using System.Text.RegularExpressions;

class Day13 : Solution
{
    public string Part1()
    {
        var input = File.ReadAllText("Inputs\\Day13.in");
        var answer = 0;
        string pattern = @"X\+(\d+), Y\+(\d+)";
        var rg = new Regex(pattern);
        var matches = rg.Matches(input);
        List<(int, int)> coeff = new List<(int, int)>();
        foreach (var nums in matches.Select(match => match.Groups))
        {
            coeff.Add((int.Parse(nums[1].Value), int.Parse(nums[2].Value)));
        }
        // print coeff
        for (int i = 0; i < coeff.Count; i++)
        {
            var (x, y) = coeff[i];
            Console.WriteLine($"{x} {y}");
        }

        pattern = @"X=(\d+), Y=(\d+)";
        rg = new Regex(pattern);
        matches = rg.Matches(input);
        List<(int, int)> results = new List<(int, int)>();
        foreach (var nums in matches.Select(match => match.Groups))
        {
            results.Add((int.Parse(nums[1].Value), int.Parse(nums[2].Value)));
        }
        for (int i = 0; i < results.Count; i++)
        {
            var (r1, r2) = results[i];
            var (c1_x, c2_x) = coeff[2 * i];
            var (c1_y, c2_y) = coeff[2 * i + 1];
            Console.WriteLine($"{r1} {r2} {c1_x} {c2_x} {c1_y} {c2_y}");
            Console.WriteLine($"({r1} * {c2_y} - {r2} * {c1_y}) / ({c1_x} * {c2_y} - {c2_x} * {c1_y})");
            var x = (r1 * c2_y - r2 * c1_y) / (c1_x * c2_y - c2_x * c1_y);
            if ((r1 * c2_y - r2 * c1_y) % (c1_x * c2_y - c2_x * c1_y) != 0)
            {
                continue;
            }
            var y = (r1 - c1_x * x) / c1_y;
            if ((r1 - c1_x * x) % c1_y != 0)
            {
                continue;
            }
            if (x > 0 && y > 0 && x <= 100 && y <= 100)
            {
                answer += x * 3 + y;
            }
        }
        return answer.ToString();
    }

    public string Part2()
    {
        var input = File.ReadAllText("Inputs\\Day13.in");
        BigInteger answer = 0;
        string pattern = @"X\+(\d+), Y\+(\d+)";
        var rg = new Regex(pattern);
        var matches = rg.Matches(input);
        List<(BigInteger, BigInteger)> coeff = new List<(BigInteger, BigInteger)>();
        foreach (var nums in matches.Select(match => match.Groups))
        {
            coeff.Add((BigInteger.Parse(nums[1].Value), BigInteger.Parse(nums[2].Value)));
        }
        // print coeff
        for (int i = 0; i < coeff.Count; i++)
        {
            var (x, y) = coeff[i];
            Console.WriteLine($"{x} {y}");
        }

        pattern = @"X=(\d+), Y=(\d+)";
        rg = new Regex(pattern);
        matches = rg.Matches(input);
        List<(BigInteger, BigInteger)> results = new List<(BigInteger, BigInteger)>();
        BigInteger diff = 10000000000000;
        foreach (var nums in matches.Select(match => match.Groups))
        {
            results.Add((diff + BigInteger.Parse(nums[1].Value), diff + BigInteger.Parse(nums[2].Value)));
        }
        for (int i = 0; i < results.Count; i++)
        {
            var (r1, r2) = results[i];
            var (c1_x, c2_x) = coeff[2 * i];
            var (c1_y, c2_y) = coeff[2 * i + 1];
            Console.WriteLine($"{r1} {r2} {c1_x} {c2_x} {c1_y} {c2_y}");
            Console.WriteLine($"({r1} * {c2_y} - {r2} * {c1_y}) / ({c1_x} * {c2_y} - {c2_x} * {c1_y})");
            var x = (r1 * c2_y - r2 * c1_y) / (c1_x * c2_y - c2_x * c1_y);
            if ((r1 * c2_y - r2 * c1_y) % (c1_x * c2_y - c2_x * c1_y) != 0)
            {
                continue;
            }
            var y = (r1 - c1_x * x) / c1_y;
            if ((r1 - c1_x * x) % c1_y != 0)
            {
                continue;
            }
            if (x > 0 && y > 0)
            {
                answer += x * 3 + y;
            }
        }
        return answer.ToString();
    }
}