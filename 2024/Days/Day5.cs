/**
* If you're reading this i hope you find a better writeup because this code is so bad but i'm too tired to rewrite it, i just wanted the stars
*/

class Day5 : Solution
{
    public string Part1()
    {
        var input = File.ReadAllLines("Inputs\\Day5.in");
        var answer = 0;

        var pages = new Dictionary<int, List<int>>();
        var sequences = false;
        foreach (var line in input)
        {
            if (line == "")
            {
                sequences = true;
                continue;
            }
            if (sequences)
            {
                answer += isValidSequence(line, pages);
            }
            else
            {
                var nums = line.Split('|');
                var left = int.Parse(nums[0]);
                var right = int.Parse(nums[1]);
                if (!pages.ContainsKey(left))
                {
                    pages[left] = new List<int>();
                }
                pages[left].Add(right);
            }
        }

        return "" + answer;
    }

    private int isValidSequence(string line, Dictionary<int, List<int>> pages)
    {
        var nums = line.Split(',').Select(int.Parse).ToArray();
        var n = nums.Length;
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = i + 1; j < n; j++)
            {
                if (!validOrder(nums[i], nums[j], pages))
                {
                    return 0;
                }
            }
        }
        return nums[n / 2];
    }

    private bool validOrder(int v1, int v2, Dictionary<int, List<int>> pages)
    {
        if (!pages.ContainsKey(v1))
        {
            return false;
        }
        return pages[v1].Contains(v2);
    }

    public string Part2()
    {
        var input = File.ReadAllLines("Inputs\\Day5.in");
        var answer = 0;

        var pages = new Dictionary<int, List<int>>();
        var sequences = false;
        foreach (var line in input)
        {
            if (line == "")
            {
                sequences = true;
                continue;
            }
            if (sequences)
            {
                answer += isValidSequencePart2(line, pages);
            }
            else
            {
                var nums = line.Split('|');
                var left = int.Parse(nums[0]);
                var right = int.Parse(nums[1]);
                if (!pages.ContainsKey(left))
                {
                    pages[left] = new List<int>();
                }
                pages[left].Add(right);
            }
        }

        return "" + answer;
    }

    private int isValidSequencePart2(string line, Dictionary<int, List<int>> pages)
    {
        var nums = line.Split(',').Select(int.Parse).ToArray();
        var n = nums.Length;
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = i + 1; j < n; j++)
            {
                if (!validOrderPart2(nums[i], nums[j], pages))
                {
                    return middleAfterSort(nums, pages);
                }
            }
        }
        return 0;
    }

    private int middleAfterSort(int[] nums, Dictionary<int, List<int>> pages)
    {
        for (int i = 0; i < nums.Length - 1; i++)
        {
            for (int j = i + 1; j < nums.Length; j++)
            {
                if (!validOrderPart2(nums[i], nums[j], pages))
                {
                    var temp = nums[i];
                    nums[i] = nums[j];
                    nums[j] = temp;
                }
            }
        }
        return nums[nums.Length / 2];
    }

    private bool validOrderPart2(int v1, int v2, Dictionary<int, List<int>> pages)
    {
        if (!pages.ContainsKey(v1))
        {
            return false;
        }
        return pages[v1].Contains(v2);
    }

}