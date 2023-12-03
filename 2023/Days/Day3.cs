using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Globalization;
using System.Runtime.InteropServices.Marshalling;

class Day3 : Solution
{
    public string Part1()
    {
        var grid = File.ReadLines("Inputs/Day3.in").ToList();
        var visited = new HashSet<Tuple<int, int>>();
        var ans = 0;
        for (var i = 0; i < grid.Count; i++)
        {
            for (var j = 0; j < grid[i].Count(); j++)
            {
                if (char.IsDigit(grid[i][j]))
                {
                    if (!visited.Contains(Tuple.Create(i, j)))
                    {
                        var valid = isValid(grid, i, j, visited);
                        if (valid > 0)
                        {
                            ans += valid;
                        }
                    }
                }
            }
        }
        return "" + ans;
    }

    private int isValid(List<string> grid, int i, int j, HashSet<Tuple<int, int>> visited)
    {
        var q = new Queue<Tuple<int, int>>();
        List<List<int>> directions = [[-1, -1], [-1, 1], [-1, 0], [0, 1], [0, -1], [1, -1], [1, 0], [1, 1]];
        q.Enqueue(Tuple.Create(i, j));
        var number = "" + grid[i][j];
        var valid = false;
        while (q.Count() > 0)
        {
            var c = q.Dequeue();
            visited.Add(c);
            foreach (var dir in directions)
            {
                var next_i = dir[0] + c.Item1;
                var next_j = dir[1] + c.Item2;
                if (next_i < 0 || next_i >= grid.Count || next_j < 0 || next_j >= grid[0].Count())
                {
                    continue;
                }
                if (char.IsDigit(grid[next_i][next_j]) && next_i == i && !visited.Contains(Tuple.Create(next_i, next_j)))
                {
                    number += grid[next_i][next_j];
                    q.Enqueue(Tuple.Create(next_i, next_j));
                }
                if (!char.IsDigit(grid[next_i][next_j]) && grid[next_i][next_j] != '.')
                {
                    valid = true;
                }
            }
        }
        // Console.WriteLine($"{number},{valid}");
        if (valid)
        {
            return int.Parse(number);
        }
        return -1;
    }

    public string Part2()
    {
        var grid = File.ReadLines("Inputs/Day3.in").ToList();
        var visited = new HashSet<Tuple<int, int>>();
        var numbers = new Dictionary<Tuple<int, int>, int>();
        var ans = 0;
        var gears = new List<Tuple<int, int>>();
        for (var i = 0; i < grid.Count; i++)
        {
            for (var j = 0; j < grid[i].Count(); j++)
            {
                if (char.IsDigit(grid[i][j]))
                {
                    processNumber(grid, i, j, visited, numbers);
                }
                if (grid[i][j] == '*')
                {
                    gears.Add(Tuple.Create(i, j));
                }
            }
        }
        foreach (var gear in gears)
        {
            ans += getGearRatio(grid, gear, numbers);
        }

        return "" + ans;
    }

    private int getGearRatio(List<string> grid, Tuple<int, int> gear, Dictionary<Tuple<int, int>, int> numbers)
    {
        List<List<int>> directions = [[-1, -1], [-1, 1], [-1, 0], [0, 1], [0, -1], [1, -1], [1, 0], [1, 1]];
        var seen_nums = new HashSet<int>();
        foreach (var dir in directions)
        {
            var n_i = dir[0] + gear.Item1;
            var n_j = dir[1] + gear.Item2;

            if (numbers.ContainsKey(Tuple.Create(n_i, n_j)))
            {
                seen_nums.Add(numbers[Tuple.Create(n_i, n_j)]);
            }
        }
        if (seen_nums.Count == 2)
        {
            return seen_nums.ToList().Aggregate((a, x) => a * x);
        }
        else
        {
            return 0;
        }
    }

    private void processNumber(List<string> grid, int i, int j, HashSet<Tuple<int, int>> visited, Dictionary<Tuple<int, int>, int> numbers)
    {
        if (visited.Contains(Tuple.Create(i, j)))
        {
            return;
        }
        var ans = "";
        var path = new List<Tuple<int, int>>();
        while (char.IsDigit(grid[i][j]))
        {
            ans += grid[i][j];
            path.Add(Tuple.Create(i, j));
            visited.Add(Tuple.Create(i, j));
            j++;
            if (j >= grid[i].Count())
            {
                break;
            }
        }

        foreach (var x in path)
        {
            numbers.Add(Tuple.Create(x.Item1, x.Item2), int.Parse(ans));
        }

    }
}