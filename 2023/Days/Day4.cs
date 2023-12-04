using System.ComponentModel;
using System.Text.Json;

class Day4 : Solution
{
    public string Part1()
    {
        var cards = File.ReadAllLines("Inputs/Day4.in");
        var ans = 0;
        foreach (var x in cards)
        {
            var nums = x.Split(": ")[1].Split(" | ");
            // Console.WriteLine(nums[0]);
            // Console.WriteLine(nums[1]);
            var win_nums = nums[0].Split(" ").Where(c => !string.IsNullOrEmpty(c)).Select(c => int.Parse(c));
            var our_nums = nums[1].Split(" ").Where(c => !string.IsNullOrEmpty(c)).Select(c => int.Parse(c));
            // Console.WriteLine(JsonSerializer.Serialize(win_nums));
            // Console.WriteLine(JsonSerializer.Serialize(our_nums));
            // Console.WriteLine($"Got the number {win_nums.Intersect(our_nums).Count()}");
            if (win_nums.Intersect(our_nums).Count() > 0)
            {
                ans = ans + Convert.ToInt32(Math.Pow(2, win_nums.Intersect(our_nums).Count() - 1));
            }
        }
        return "" + ans;
    }

    public string Part2()
    {
        var cards = File.ReadAllLines("Inputs/Day4.test");
        var ans = 0;
        var doubles = new Dictionary<int, int>(cards.Count());
        for (int i = 0; i <= cards.Count(); i++)
        {
            doubles[i] = 1;
        }
        foreach (var x in cards)
        {
            var nums = x.Split(": ")[1].Split(" | ");
            var card_num = int.Parse(x.Split(": ")[0].Split(" ").Where(c => !string.IsNullOrEmpty(c)).ToList()[1]);
            var win_nums = nums[0].Split(" ").Where(c => !string.IsNullOrEmpty(c)).Select(c => int.Parse(c));
            var our_nums = nums[1].Split(" ").Where(c => !string.IsNullOrEmpty(c)).Select(c => int.Parse(c));

            var w = win_nums.Intersect(our_nums).Count();
            for (int i = card_num + 1; i <= card_num + w; i++)
            {
                doubles[i] = doubles[card_num] + doubles[i];
            }
        }
        return "" + (doubles.Values.Sum() - 1);
    }
}