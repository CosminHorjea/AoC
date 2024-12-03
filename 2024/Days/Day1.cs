class Day1 : Solution
{
    public string Part1()
    {
        var input = File.ReadAllLines("Inputs\\Day1.in");
        var answer = 0;
        List<int> left = new List<int>(), right = new List<int>();
        foreach (var line in input)
        {
            var nums = line.Split("   ");
            left.Add(int.Parse(nums[0]));
            right.Add(int.Parse(nums[1]));
        }
        left.Sort();
        right.Sort();
        var diffs = left.Zip(right, (l, r) =>
        {
            return Math.Abs(l - r);
        });
        answer = diffs.Sum();
        return "" + answer;
    }

    public string Part2()
    {
        var input = File.ReadAllLines("Inputs\\Day1.in");
        var answer = 0;
        List<int> left = new List<int>();
        Dictionary<int, int> right = new Dictionary<int, int>();
        foreach (var line in input)
        {
            var nums = line.Split("   ");
            left.Add(int.Parse(nums[0]));
            if (right.ContainsKey(int.Parse(nums[1])))
            {
                right[int.Parse(nums[1])]++;
            }
            else
            {
                right[int.Parse(nums[1])] = 1;
            }
        }
        foreach (var item in left)
        {
            answer += item * right.GetValueOrDefault(item, 0);
        }
        return "" + answer;

    }
}