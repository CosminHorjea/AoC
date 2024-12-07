
using System.Numerics;

class Day7 : Solution
{
    public string Part1()
    {
        var input = File.ReadAllLines("Inputs\\Day7.in");
        BigInteger answer = 0;
        foreach (var line in input)
        {
            var parts = line.Split(": ");
            var target = BigInteger.Parse(parts[0]);
            var nums = parts[1].Split(" ").Select(BigInteger.Parse).ToArray();
            if (CanBeAchieved(target, nums))
            {
                answer += target;
            }
        }

        return "" + answer;
    }
    enum OperationType
    {
        Add,
        Multiply
    }
    private bool CanBeAchieved(BigInteger target, BigInteger[] nums)
    {
        var operations = new OperationType[nums.Length - 1];

        for (int i = 0; i < Math.Pow(2, nums.Length - 1); i++)
        {
            var binary = Convert.ToString(i, 2).PadLeft(nums.Length - 1, '0');
            for (int j = 0; j < binary.Length; j++)
            {
                operations[j] = binary[j] == '0' ? OperationType.Add : OperationType.Multiply;
            }

            if (Calculate(nums, operations) == target)
            {
                return true;
            }
        }
        return false;
    }

    private static BigInteger Calculate(BigInteger[] nums, OperationType[] operations)
    {
        BigInteger result = nums[0];
        for (int i = 0; i < operations.Length; i++)
        {
            if (operations[i] == OperationType.Add)
            {
                result += nums[i + 1];
            }
            else
            {
                result *= nums[i + 1];
            }
        }

        return result;
    }

    public string Part2()
    {
        var input = File.ReadAllLines("Inputs\\Day7.test");
        BigInteger answer = 0;
        foreach (var line in input)
        {
            var parts = line.Split(": ");
            var target = BigInteger.Parse(parts[0]);
            var nums = parts[1].Split(" ").Select(BigInteger.Parse).ToArray();
            if (CanBeAchievedPart2(target, nums))
            {
                answer += target;
            }
        }

        return "" + answer;
    }
    enum OperationTypePart2
    {
        Add,
        Multiply,
        Append
    }
    private bool CanBeAchievedPart2(BigInteger target, BigInteger[] nums)
    {
        foreach (var item in GenerateCombinations(Enum.GetValues(typeof(OperationTypePart2)).Cast<OperationTypePart2>().ToList(), nums.Length - 1))
        {
            var operationsArray = item.ToArray();
            if (CalculatePart2(nums, operationsArray) == target)
            {
                return true;
            }
        }
        return false;
    }

    private static BigInteger CalculatePart2(BigInteger[] nums, OperationTypePart2[] operations)
    {
        BigInteger result = nums[0];
        for (int i = 0; i < operations.Length; i++)
        {
            switch (operations[i])
            {
                case OperationTypePart2.Add:
                    result += nums[i + 1];
                    break;
                case OperationTypePart2.Multiply:
                    result *= nums[i + 1];
                    break;
                case OperationTypePart2.Append:
                    result = BigInteger.Parse(result.ToString() + nums[i + 1].ToString());
                    break;
            }
        }

        return result;
    }
    static IEnumerable<List<T>> GenerateCombinations<T>(List<T> elements, int n)
    {
        if (n == 0)
        {
            yield return new List<T>();
            yield break;
        }

        foreach (var element in elements)
        {
            foreach (var combination in GenerateCombinations(elements, n - 1))
            {
                combination.Insert(0, element);
                yield return combination;
            }
        }
    }
}

