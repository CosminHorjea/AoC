class Day23 : Solution
{
    Dictionary<char, (int, int)> valley = new Dictionary<char, (int, int)>{
            {'>',(0,1)},
            {'<',(0,-1)},
            {'^',(-1,0)},
            {'v',(1,0)},
        };
    List<(int, int)> dirs = [(0, 1), (1, 0), (-1, 0), (0, -1)];

    public string Part1()
    {
        var map = File.ReadAllLines("Inputs/Day23.test").Select(s => s.ToList()).ToList();
        int n = map.Count;
        int m = map[0].Count;
        var start = (0, 1);
        var end = (n - 1, m - 2);
        PriorityQueue<(int, int, int), int> q = new PriorityQueue<(int, int, int), int>();
        HashSet<(int, int)> seen = new HashSet<(int, int)>();
        return $"{getLongestPath(map, start, 0, seen, end)}";
    }

    public int getLongestPath(List<List<char>> map, (int, int) pos, int steps, HashSet<(int, int)> seen, (int, int) end)
    {
        int n = map.Count;
        int m = map[0].Count;
        if (seen.Contains(pos))
        {
            return 0;
        }
        if (pos.Item1 == end.Item1 && pos.Item2 == end.Item2)
        {
            return steps;
        }
        var tile = map[pos.Item1][pos.Item2];
        HashSet<(int, int)> next_seen = new HashSet<(int, int)>(seen);
        next_seen.Add(pos);
        if (valley.Keys.Contains(tile))
        {
            var next_i = pos.Item1 + valley[tile].Item1;
            var next_j = pos.Item2 + valley[tile].Item2;
            if (map[next_i][next_j] != '#')
            {
                return getLongestPath(map, (next_i, next_j), steps + 1, next_seen, end);
            }
        }
        else
        {
            List<int> results = new List<int>();
            foreach (var dir in dirs)
            {
                var next_i = pos.Item1 + dir.Item1;
                var next_j = pos.Item2 + dir.Item2;
                if (next_i >= 0 && next_i < n && next_j >= 0 && next_j < m && map[next_i][next_j] != '#')
                {

                    results.Add(getLongestPath(map, (next_i, next_j), steps + 1, next_seen, end));
                }
            }
            if (results.Count > 0)
            {
                return results.Max();
            }
            else return 0;
        }
        return 0;
    }
    public string Part2()
    {
        var map = File.ReadAllLines("Inputs/Day23.test").Select(s => s.ToList()).ToList();
        int n = map.Count;
        int m = map[0].Count;
        var start = (0, 1);
        var end = (n - 1, m - 2);
        PriorityQueue<(int, int, int), int> q = new PriorityQueue<(int, int, int), int>();
        HashSet<(int, int)> seen = new HashSet<(int, int)>();
        return $"{getLongestPathPart2(map, start, 0, seen, end)}";
    }
    public int getLongestPathPart2(List<List<char>> map, (int, int) pos, int steps, HashSet<(int, int)> seen, (int, int) end)
    {
        int n = map.Count;
        int m = map[0].Count;
        if (seen.Contains(pos))
        {
            return 0;
        }
        if (pos.Item1 == end.Item1 && pos.Item2 == end.Item2)
        {
            return steps;
        }
        var tile = map[pos.Item1][pos.Item2];
        HashSet<(int, int)> next_seen = new HashSet<(int, int)>(seen);
        next_seen.Add(pos);
        List<int> results = new List<int>();
        foreach (var dir in dirs)
        {
            var next_i = pos.Item1 + dir.Item1;
            var next_j = pos.Item2 + dir.Item2;
            if (next_i >= 0 && next_i < n && next_j >= 0 && next_j < m && map[next_i][next_j] != '#')
            {

                results.Add(getLongestPathPart2(map, (next_i, next_j), steps + 1, next_seen, end));
            }
        }
        if (results.Count > 0)
        {
            return results.Max();
        }
        else return 0;
    }
}