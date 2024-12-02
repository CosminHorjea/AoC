
using System.Runtime.InteropServices.Marshalling;

class Day2 : Solution
{
    public string Part1()
    {
        var input = File.ReadAllLines("Inputs\\Day2.in");
        var answer = 0;
        foreach (var line in input)
        {
            if (isSafe(line))
            {
                answer++;
            }

        }
        return "" + answer;
    }

    private bool isSafe(string line)
    {
        var nums = line.Split(' ').Select(int.Parse).ToList();
        return isSafeNums(nums);
    }

    private static bool isSafeNums(List<int> nums)
    {
        var aux = nums[0];
        var sign = nums[0] - nums[1] > 0;
        for (int i = 1; i < nums.Count; i++)
        {
            if (Math.Abs(aux - nums[i]) > 3 || Math.Abs(aux - nums[i]) <= 0)
            {
                return false;
            }
            if (sign != aux - nums[i] > 0)
            {
                return false;
            }
            aux = nums[i];
        }
        return true;
    }

    private bool isSafe2(string line)
    {
        var nums = line.Split(' ').Select(int.Parse).ToList();
        var ans = false;
        for (int i = 0; i < nums.Count; i++)
        {
            var numsCopy = new List<int>(nums);
            numsCopy.RemoveAt(i);
            if (isSafeNums(numsCopy))
            {
                ans = true;
            }
        }
        return ans;
    }


    public string Part2()
    {
        var input = File.ReadAllLines("Inputs\\Day2.in");
        var answer = 0;
        foreach (var line in input)
        {
            if (isSafe2(line))
            {
                answer++;
            }

        }
        return "" + answer;

    }
}