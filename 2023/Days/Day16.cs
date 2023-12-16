using System.Text.Json;
using System.Text.Json.Serialization;


class Day16 : Solution
{
    public const int RIGHT = 0;
    public const int DOWN = 1;
    public const int LEFT = 2;
    public const int UP = 3;

    public string Part1()
    {
        var content = File.ReadAllLines("Inputs/Day16.in");
        var map = content.Select(s => s.ToList()).ToList();
        int n = map.Count;
        int m = map[0].Count;
        HashSet<((int, int), int)> seen = new HashSet<((int, int), int)>();
        Queue<((int, int), int)> q = new Queue<((int, int), int)>();
        List<(int, int)> d = new List<(int, int)>() { (0, 1), (1, 0), (0, -1), (-1, 0) };
        q.Enqueue(((0, 0), 0));
        while (q.Count > 0)
        {
            var curr = q.Dequeue();
            var direction = curr.Item2;
            var i = curr.Item1.Item1;
            var j = curr.Item1.Item2;
            if (i >= n || i < 0 || j >= m || j < 0)
            {
                continue;
            }
            if (seen.Contains(curr))
            {
                continue;
            }
            // Console.WriteLine(curr);
            int next_i = curr.Item1.Item1 + d[direction].Item1;
            int next_j = curr.Item1.Item2 + d[direction].Item2;
            if (map[i][j] == '.')
            {
                seen.Add(curr);
                q.Enqueue(((next_i, next_j), curr.Item2));
                continue;
            }
            switch (map[i][j])
            {
                case '\\':
                    switch (direction)
                    {
                        case RIGHT: // dreapta
                            q.Enqueue(((i + 1, j), DOWN));
                            break;
                        case DOWN://jos
                            q.Enqueue(((i, j + 1), RIGHT));
                            break;
                        case LEFT://stanga
                            q.Enqueue(((i - 1, j), UP));
                            break;
                        case UP://sus
                            q.Enqueue(((i, j - 1), LEFT));
                            break;
                    }
                    break;
                case '/':
                    switch (direction)
                    {
                        case 0: // dreapta
                            q.Enqueue(((i - 1, j), UP));
                            break;
                        case 1://jos
                            q.Enqueue(((i, j - 1), LEFT));
                            break;
                        case 2://stanga
                            q.Enqueue(((i + 1, j), DOWN));
                            break;
                        case 3://sus
                            q.Enqueue(((i, j + 1), RIGHT));
                            break;
                    }
                    break;
                case '|':
                    switch (direction)
                    {
                        case 0: // dreapta
                            q.Enqueue(((i + 1, j), DOWN));
                            q.Enqueue(((i - 1, j), UP));
                            break;
                        case 2://stanga
                            q.Enqueue(((i + 1, j), DOWN));
                            q.Enqueue(((i - 1, j), UP));
                            break;
                        default:
                            q.Enqueue(((next_i, next_j), curr.Item2));
                            break;
                    }
                    break;
                case '-':
                    switch (direction)
                    {
                        case UP:
                            q.Enqueue(((i, j + 1), RIGHT));
                            q.Enqueue(((i, j - 1), LEFT));
                            break;
                        case DOWN:
                            q.Enqueue(((i, j + 1), RIGHT));
                            q.Enqueue(((i, j - 1), LEFT));
                            break;
                        default:
                            q.Enqueue(((next_i, next_j), curr.Item2));
                            break;
                    }
                    break;
                default:
                    throw new Exception("hell let loose");
            }
            seen.Add(curr);
        }
        HashSet<(int, int)> ans = new HashSet<(int, int)>(seen.Select(s => s.Item1));
        // PrintReflections(map, n, m, ans);
        return $"{ans.Count}";

    }

    private static void PrintReflections(List<List<char>> map, int n, int m, HashSet<(int, int)> ans)
    {
        for (var i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                if (ans.Contains((i, j)))
                {
                    Console.Write('#');
                }
                else
                {
                    Console.Write(map[i][j]);
                }
            }
            Console.Write("\n");
        }
    }

    public string Part2()
    {
        var content = File.ReadAllLines("Inputs/Day16.in");
        var map = content.Select(s => s.ToList()).ToList();
        int n = map.Count;
        int m = map[0].Count;
        var start = ((0, 0), 0);
        int max_energy = 0;
        for (int i = 0; i < n; i++)
        {
            max_energy = Math.Max(max_energy, GetEnergizedCells(map, n, m, ((i, 0), RIGHT)));
            max_energy = Math.Max(max_energy, GetEnergizedCells(map, n, m, ((i, m - 1), LEFT)));
        }
        for (int j = 0; j < m; j++)
        {
            max_energy = Math.Max(max_energy, GetEnergizedCells(map, n, m, ((0, j), DOWN)));
            max_energy = Math.Max(max_energy, GetEnergizedCells(map, n, m, ((n - 1, j), UP)));
        }
        return $"{max_energy}";
    }
    private static int GetEnergizedCells(List<List<char>> map, int n, int m, ((int, int), int) start)
    {
        HashSet<((int, int), int)> seen = new HashSet<((int, int), int)>();
        Queue<((int, int), int)> q = new Queue<((int, int), int)>();
        List<(int, int)> d = new List<(int, int)>() { (0, 1), (1, 0), (0, -1), (-1, 0) };
        q.Enqueue(start);
        while (q.Count > 0)
        {
            var curr = q.Dequeue();
            var direction = curr.Item2;
            var i = curr.Item1.Item1;
            var j = curr.Item1.Item2;
            if (i >= n || i < 0 || j >= m || j < 0)
            {
                continue;
            }
            if (seen.Contains(curr))
            {
                continue;
            }
            // Console.WriteLine(curr);
            int next_i = curr.Item1.Item1 + d[direction].Item1;
            int next_j = curr.Item1.Item2 + d[direction].Item2;
            if (map[i][j] == '.')
            {
                seen.Add(curr);
                q.Enqueue(((next_i, next_j), curr.Item2));
                continue;
            }
            switch (map[i][j])
            {
                case '\\':
                    switch (direction)
                    {
                        case RIGHT: // dreapta
                            q.Enqueue(((i + 1, j), DOWN));
                            break;
                        case DOWN://jos
                            q.Enqueue(((i, j + 1), RIGHT));
                            break;
                        case LEFT://stanga
                            q.Enqueue(((i - 1, j), UP));
                            break;
                        case UP://sus
                            q.Enqueue(((i, j - 1), LEFT));
                            break;
                    }
                    break;
                case '/':
                    switch (direction)
                    {
                        case 0: // dreapta
                            q.Enqueue(((i - 1, j), UP));
                            break;
                        case 1://jos
                            q.Enqueue(((i, j - 1), LEFT));
                            break;
                        case 2://stanga
                            q.Enqueue(((i + 1, j), DOWN));
                            break;
                        case 3://sus
                            q.Enqueue(((i, j + 1), RIGHT));
                            break;
                    }
                    break;
                case '|':
                    switch (direction)
                    {
                        case 0: // dreapta
                            q.Enqueue(((i + 1, j), DOWN));
                            q.Enqueue(((i - 1, j), UP));
                            break;
                        case 2://stanga
                            q.Enqueue(((i + 1, j), DOWN));
                            q.Enqueue(((i - 1, j), UP));
                            break;
                        default:
                            q.Enqueue(((next_i, next_j), curr.Item2));
                            break;
                    }
                    break;
                case '-':
                    switch (direction)
                    {
                        case UP:
                            q.Enqueue(((i, j + 1), RIGHT));
                            q.Enqueue(((i, j - 1), LEFT));
                            break;
                        case DOWN:
                            q.Enqueue(((i, j + 1), RIGHT));
                            q.Enqueue(((i, j - 1), LEFT));
                            break;
                        default:
                            q.Enqueue(((next_i, next_j), curr.Item2));
                            break;
                    }
                    break;
                default:
                    throw new Exception("hell let loose");
            }
            seen.Add(curr);
        }
        HashSet<(int, int)> ans = new HashSet<(int, int)>(seen.Select(s => s.Item1));
        // PrintReflections(map, n, m, ans);
        return ans.Count;
    }
}