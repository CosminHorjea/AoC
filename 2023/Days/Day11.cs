using System.Numerics;
using System.Text.Json;

class Day11 : Solution
{
    public string Part1()
    {
        var contents = File.ReadAllLines("Inputs/Day11.in");
        List<List<char>> map = contents.Select(c => c.ToList()).ToList();
        int n = map.Count;
        int m = map[0].Count;
        Console.WriteLine($"Initial n-m : {n}-{m}");
        List<List<char>> extended_map = new List<List<char>>();
        foreach (var line in map)
        {
            extended_map.Add(line);
            if (line.All(c => c == '.'))
            {
                extended_map.Add(new string('.', m).ToList());
            }
        }
        map = new List<List<char>>(extended_map);
        n = map.Count;
        Console.WriteLine($"new n {n}");
        var empty_cols = new List<int>();
        for (var j = map[0].Count - 1; j >= 0; j--)
        {
            var column = "";
            for (var i = 0; i < n; i++)
            {
                column += map[i][j];
            }
            if (column.All(c => c == '.'))
            {
                empty_cols.Add(j);
            }
        }
        for (var i = 0; i < n; i++)
        {
            map[i] = InsertChars(map[i], empty_cols, '.');
        }

        m = map[0].Count;
        Console.WriteLine($"new m {m}");
        // for (int i = 0; i < n; i++)
        // {
        //     Console.WriteLine(new string(map[i].ToArray()));
        // }
        List<(int, int)> points = new List<(int, int)>();
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                if (map[i][j] != '.')
                {
                    points.Add((i, j));
                }
            }
        }
        Console.WriteLine($"Points length {points.Count}");
        List<int> minimum_steps = new List<int>();
        HashSet<((int, int), (int, int))> computed = new HashSet<((int, int), (int, int))>();
        foreach (var item in points)
        {
            foreach (var item2 in points)
            {
                // Console.WriteLine($"Verifying {item} {item2}");
                if (item != item2 && !computed.Contains((item2, item)))
                {
                    minimum_steps.Add(getShortestLength(map, item, item2));
                    computed.Add((item, item2));
                    computed.Add((item2, item));
                }
            }
        }
        return "" + minimum_steps.Sum();
    }
    static List<char> InsertChars(List<char> original, List<int> positions, char charToInsert)
    {
        // Ensure positions are in ascending order
        positions.Sort();

        // Create a new list of chars by iteratively inserting the character
        List<char> result = new List<char>(original);
        int offset = 0;

        foreach (int position in positions)
        {
            int adjustedPosition = position + offset;

            // Check if the adjusted position is within the bounds of the list
            if (adjustedPosition < 0 || adjustedPosition > result.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(position), "Position is out of range.");
            }

            // Insert the character
            result.Insert(adjustedPosition, charToInsert);
            offset++;
        }

        return result;
    }

    static int getShortestLength(List<List<char>> map, (int, int) start, (int, int) end)
    {
        if (start == end)
        {
            return 0;
        }
        // this works, but I already spent time doing bfs :(
        return Math.Abs(start.Item1 - end.Item1) + Math.Abs(start.Item2 - end.Item2); // :(
        int n = map.Count;
        int m = map[0].Count;
        PriorityQueue<((int, int), int), int> q = new PriorityQueue<((int, int), int), int>();
        HashSet<(int, int)> visited = new HashSet<(int, int)>();
        List<(int, int)> moves = new List<(int, int)>() { (-1, 0), (0, -1), (1, 0), (0, 1) };
        q.Enqueue((start, 0), 0);
        while (q.Count > 0)
        {
            var current = q.Dequeue();
            visited.Add(current.Item1);
            foreach (var direction in moves)
            {
                var next_i = current.Item1.Item1 + direction.Item1;
                var next_j = current.Item1.Item2 + direction.Item2;
                if (next_i < 0 || next_i >= n || next_j < 0 || next_j >= m)
                {
                    continue;
                }
                if (visited.Contains((next_i, next_j)))
                {
                    continue;
                }
                if ((next_i, next_j) == end)
                {
                    return current.Item2 + 1;
                }
                q.Enqueue(((next_i, next_j), current.Item2 + 1), Math.Abs(next_i - end.Item1) + Math.Abs(next_j - end.Item2));
            }
        }
        throw new Exception("Wtf happened;");
        return 0;
    }

    public string Part2()
    {
        var contents = File.ReadAllLines("Inputs/Day11.in");
        List<List<char>> map = contents.Select(c => c.ToList()).ToList();
        int n = map.Count;
        int m = map[0].Count;
        Console.WriteLine($"Initial n-m : {n}-{m}");
        HashSet<int> extended_rows = new HashSet<int>();
        for (int i = 0; i < n; i++)
        {
            if (map[i].All(c => c == '.'))
            {
                extended_rows.Add(i);
            }
        }
        var extended_cols = new HashSet<int>();
        for (var j = map[0].Count - 1; j >= 0; j--)
        {
            var column = "";
            for (var i = 0; i < n; i++)
            {
                column += map[i][j];
            }
            if (column.All(c => c == '.'))
            {
                extended_cols.Add(j);
            }
        }
        List<(int, int)> points = new List<(int, int)>();
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                if (map[i][j] != '.')
                {
                    points.Add((i, j));
                }
            }
        }
        Console.WriteLine($"Points length {points.Count}");
        List<BigInteger> minimum_steps = new List<BigInteger>();
        HashSet<((int, int), (int, int))> computed = new HashSet<((int, int), (int, int))>();
        foreach (var item in points)
        {
            foreach (var item2 in points)
            {
                Console.WriteLine($"Verifying {item} {item2}");
                if (item != item2 && !computed.Contains((item2, item)))
                {
                    minimum_steps.Add(getShortestLength(map, item, item2, 1000000, extended_rows, extended_cols));
                    computed.Add((item, item2));
                    computed.Add((item2, item));
                }
            }
        }
        return "" + minimum_steps.Aggregate((x, a) => x + a);
        throw new NotImplementedException();
    }

    private BigInteger getShortestLength(List<List<char>> map, (int, int) start, (int, int) end, int displacement, HashSet<int> extended_rows, HashSet<int> extended_cols)
    {
        if (start == end)
        {
            return 0;
        }

        // return Math.Abs(start.Item1 - end.Item1) + Math.Abs(start.Item2 - end.Item2); // :(
        int n = map.Count;
        int m = map[0].Count;
        PriorityQueue<((int, int), BigInteger), BigInteger> q = new PriorityQueue<((int, int), BigInteger), BigInteger>();
        HashSet<(int, int)> visited = new HashSet<(int, int)>();
        List<(int, int)> moves = new List<(int, int)>() { (-1, 0), (0, -1), (1, 0), (0, 1) };
        q.Enqueue((start, 0), 0);
        while (q.Count > 0)
        {
            var current = q.Dequeue();
            visited.Add(current.Item1);
            foreach (var direction in moves)
            {
                var next_i = current.Item1.Item1 + direction.Item1;
                var next_j = current.Item1.Item2 + direction.Item2;
                if (next_i < 0 || next_i >= n || next_j < 0 || next_j >= m)
                {
                    continue;
                }
                if (visited.Contains((next_i, next_j)))
                {
                    continue;
                }
                if ((next_i, next_j) == end)
                {
                    return current.Item2 + new BigInteger(1);
                }
                var cost = 1;
                if (extended_rows.Contains(next_i) && next_i != current.Item1.Item1)
                    cost += displacement - 1;
                if (extended_cols.Contains(next_j) && next_j != current.Item1.Item2)
                    cost += displacement - 1;
                q.Enqueue(((next_i, next_j), current.Item2 + cost), Math.Abs(next_i - end.Item1) + Math.Abs(next_j - end.Item2));
                // apparently it's enough to consider the manhattan distance as the priority, and ignore cost, strange
            }
        }
        throw new Exception("Wtf happened;");
        return 0;
    }
}
// 550359414682 high
// 550358864332