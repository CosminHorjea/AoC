using System.Text.RegularExpressions;

class Day3 : Solution
{
    public string Part1()
    {
        var input = File.ReadAllText("Inputs\\Day3.in");
        var answer = 0;
        string pattern = @"mul\((\d{1,3}),(\d{1,3})\)";
        var rg = new Regex(pattern);
        var matches = rg.Matches(input);
        foreach (var nums in matches.Select(match => match.Groups))
        {
            answer += int.Parse(nums[1].Value) * int.Parse(nums[2].Value);
        }
        return "" + answer;
    }

    public string Part2()
    {
        var input = File.ReadAllText("Inputs\\Day3.in");
        var answer = 0;
        string pattern = @"mul\((\d{1,3}),(\d{1,3})\)|don't\(\)|do\(\)";
        var rg = new Regex(pattern);
        var matches = rg.Matches(input);
        var enabled = true;
        foreach (var nums in matches.Select(match => match.Groups))
        {
            switch (nums[0].Value)
            {
                case "don't()":
                    enabled = false;
                    break;
                case "do()":
                    enabled = true;
                    break;
                default:
                    if (!enabled) break;
                    answer += int.Parse(nums[1].Value) * int.Parse(nums[2].Value);
                    break;
            }
        }
        return "" + answer;


    }
}