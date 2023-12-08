using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security.Authentication;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;

class Day8 : Solution
{
    struct Node
    {
        public string label;
        public string left, right;
        public Node(string line)
        {
            Regex patters = new Regex(@"([A-Z1-9]{3})");
            var matches = patters.Matches(line).Select(m => m.Value).ToList();
            label = matches[0];
            left = matches[1];
            right = matches[2];
        }
    }
    public string Part1()
    {
        var contents = File.ReadAllLines("Inputs/Day8.in");
        var directions = contents[0];
        Dictionary<string, Node> nodes = new Dictionary<string, Node>();
        foreach (var item in contents.Skip(2))
        {
            Node temp_node = new Node(item);
            nodes.Add(temp_node.label, temp_node);
        }
        var current_label = "AAA";
        var i = 0;
        while (current_label != "ZZZ")
        {
            var dir = directions[i % directions.Count()];
            var current_node = nodes[current_label];
            current_label = dir == 'L' ? current_node.left : current_node.right;
            i++;
        }
        return $"{i}";
    }
    private static BigInteger LowestCommonMultiple(List<BigInteger> nums)
    {
        BigInteger lcm = nums[0];

        for (int i = 1; i < nums.Count; i++)
        {
            BigInteger gcd = GreatestCommonDivisor(lcm, nums[i]);
            lcm = (lcm * nums[i]) / gcd;
        }

        return lcm;

    }

    private static BigInteger GreatestCommonDivisor(BigInteger a, BigInteger b)
    {
        while (b != 0)
        {
            BigInteger temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }
    public string Part2()
    {
        var contents = File.ReadAllLines("Inputs/Day8.in");
        var directions = contents[0];
        Dictionary<string, Node> nodes = new Dictionary<string, Node>();
        foreach (var item in contents.Skip(2))
        {
            Node temp_node = new Node(item);
            nodes.Add(temp_node.label, temp_node);
        }
        var current_labels = nodes.Keys.Where(l => l.Last() == 'A').ToList();
        var steps = new List<BigInteger>();
        foreach (var current_label in current_labels)
        {
            var i = 0;
            var aux = current_label;
            while (!(aux.Last() == 'Z'))
            {
                var dir = directions[i % directions.Count()];
                var current_node = nodes[aux];
                aux = dir == 'L' ? current_node.left : current_node.right;
                i++;
            }
            steps.Add(i);
        }

        return $"{LowestCommonMultiple(steps)}";
    }
}