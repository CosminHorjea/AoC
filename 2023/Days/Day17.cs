using System.Diagnostics.Contracts;
using Microsoft.VisualBasic;

class Day17 : Solution
{
    public const int RIGHT = 0;
    public const int DOWN = 1;
    public const int LEFT = 2;
    public const int UP = 3;
    public string Part1()
    {
        List<List<int>> map = File.ReadAllLines("Inputs/Day17.test").Select(s => s.Select(c => c - '0').ToList()).ToList();

        int n = map.Count;
        int m = map[0].Count;
        List<(int, int)> d = new List<(int, int)>() { (0, 1), (1, 0), (0, -1), (-1, 0) };
        PriorityQueue<((int, int), int, int), int> q = new PriorityQueue<((int, int), int, int), int>();
        q.Enqueue(((0, 0), 1, RIGHT), 0);
        q.Enqueue(((0, 0), 1, DOWN), 0);
        // 1028 too high
        // 1013 too high
        HashSet<((int, int), int, int)> seen = new HashSet<((int, int), int, int)>();
        while (q.Count > 0)
        {
            q.TryDequeue(out var curr, out var heat);
            var i = curr.Item1.Item1;
            var j = curr.Item1.Item2;
            var len = curr.Item2;
            var dir = curr.Item3;

            if (i == n - 1 && j == m - 1)
            {
                return $"{heat}";
            }
            // for some reason this config worked, when i put heat and other stuff it wont get the right answer
            if (seen.Contains(((i, j), len, dir)))
            {
                continue;
            }
            seen.Add(((i, j), len, dir));
            // Console.WriteLine($"{i},{j}|{heat}");
            if (i < 0 || i >= n || j < 0 || j >= m)
            {
                continue;
            }
            switch (dir)
            {
                case UP:
                    if (j - 1 >= 0)
                        q.Enqueue(((i, j - 1), 1, LEFT), heat + map[i][j - 1]);
                    if (j + 1 < m)
                        q.Enqueue(((i, j + 1), 1, RIGHT), heat + map[i][j + 1]);
                    if (len != 3)
                    {
                        if (i - 1 >= 0)
                            q.Enqueue(((i - 1, j), len + 1, UP), heat + map[i - 1][j]);
                    }
                    break;
                case DOWN:
                    if (j - 1 >= 0)
                        q.Enqueue(((i, j - 1), 1, LEFT), heat + map[i][j - 1]);
                    if (j + 1 < m)
                        q.Enqueue(((i, j + 1), 1, RIGHT), heat + map[i][j + 1]);
                    if (len != 3)
                    {
                        if (i + 1 < n)
                            q.Enqueue(((i + 1, j), len + 1, DOWN), heat + map[i + 1][j]);
                    }
                    break;
                case LEFT:
                    if (i - 1 >= 0)
                        q.Enqueue(((i - 1, j), 1, UP), heat + map[i - 1][j]);
                    if (i + 1 < n)
                        q.Enqueue(((i + 1, j), 1, DOWN), heat + map[i + 1][j]);
                    if (len != 3)
                    {
                        if (j - 1 >= 0)
                            q.Enqueue(((i, j - 1), len + 1, LEFT), heat + map[i][j - 1]);
                    }
                    break;
                case RIGHT:
                    if (i - 1 >= 0)
                        q.Enqueue(((i - 1, j), 1, UP), heat + map[i - 1][j]);
                    if (i + 1 < n)
                        q.Enqueue(((i + 1, j), 1, DOWN), heat + map[i + 1][j]);
                    if (len != 3)
                    {
                        if (j + 1 < m)
                            q.Enqueue(((i, j + 1), len + 1, RIGHT), heat + map[i][j + 1]);
                    }
                    break;
            }
        }
        throw new Exception("Never got there");
    }

    public string Part2()
    {
        List<List<int>> map = File.ReadAllLines("Inputs/Day17.in").Select(s => s.Select(c => c - '0').ToList()).ToList();

        int n = map.Count;
        int m = map[0].Count;
        List<(int, int)> d = new List<(int, int)>() { (0, 1), (1, 0), (0, -1), (-1, 0) };
        PriorityQueue<((int, int), int, int), int> q = new PriorityQueue<((int, int), int, int), int>();
        q.Enqueue(((0, 0), 1, RIGHT), 0);
        q.Enqueue(((0, 0), 1, DOWN), 0);
        HashSet<((int, int), int, int)> seen = new HashSet<((int, int), int, int)>();
        while (q.Count > 0)
        {
            q.TryDequeue(out var curr, out var heat);
            var i = curr.Item1.Item1;
            var j = curr.Item1.Item2;
            var len = curr.Item2;
            var dir = curr.Item3;

            if (i == n - 1 && j == m - 1)
            {
                return $"{heat}";
            }
            // for some reason this config worked, when i put heat and other stuff it wont get the right answer
            if (seen.Contains(((i, j), len, dir)))
            {
                continue;
            }
            seen.Add(((i, j), len, dir));
            // Console.WriteLine($"{i},{j}|{heat}");
            if (i < 0 || i >= n || j < 0 || j >= m)
            {
                continue;
            }
            switch (dir)
            {
                case UP:
                    if (len < 4)
                    {
                        if (i - 1 >= 0)
                            q.Enqueue(((i - 1, j), len + 1, UP), heat + map[i - 1][j]);
                        continue;
                    }
                    else if (len < 10)
                    {
                        if (i - 1 >= 0)
                            q.Enqueue(((i - 1, j), len + 1, UP), heat + map[i - 1][j]);
                    }
                    if (j - 1 >= 0)
                        q.Enqueue(((i, j - 1), 1, LEFT), heat + map[i][j - 1]);
                    if (j + 1 < m)
                        q.Enqueue(((i, j + 1), 1, RIGHT), heat + map[i][j + 1]);
                    break;
                case DOWN:
                    if (len < 4)
                    {
                        if (i + 1 < n)
                            q.Enqueue(((i + 1, j), len + 1, DOWN), heat + map[i + 1][j]);
                        continue;
                    }
                    else if (len < 10)
                    {
                        if (i + 1 < n)
                            q.Enqueue(((i + 1, j), len + 1, DOWN), heat + map[i + 1][j]);
                    }
                    if (j - 1 >= 0)
                        q.Enqueue(((i, j - 1), 1, LEFT), heat + map[i][j - 1]);
                    if (j + 1 < m)
                        q.Enqueue(((i, j + 1), 1, RIGHT), heat + map[i][j + 1]);
                    break;
                case LEFT:
                    if (len < 4)
                    {
                        if (j - 1 >= 0)
                            q.Enqueue(((i, j - 1), len + 1, LEFT), heat + map[i][j - 1]);
                        continue;
                    }
                    else if (len < 10)
                    {
                        if (j - 1 >= 0)
                            q.Enqueue(((i, j - 1), len + 1, LEFT), heat + map[i][j - 1]);
                    }
                    if (i - 1 >= 0)
                        q.Enqueue(((i - 1, j), 1, UP), heat + map[i - 1][j]);
                    if (i + 1 < n)
                        q.Enqueue(((i + 1, j), 1, DOWN), heat + map[i + 1][j]);
                    break;
                case RIGHT:
                    if (len < 4)
                    {
                        if (j + 1 < m)
                            q.Enqueue(((i, j + 1), len + 1, RIGHT), heat + map[i][j + 1]);
                        break;
                    }
                    else if (len < 10)
                    {
                        if (j + 1 < m)
                            q.Enqueue(((i, j + 1), len + 1, RIGHT), heat + map[i][j + 1]);
                    }
                    if (i - 1 >= 0)
                        q.Enqueue(((i - 1, j), 1, UP), heat + map[i - 1][j]);
                    if (i + 1 < n)
                        q.Enqueue(((i + 1, j), 1, DOWN), heat + map[i + 1][j]);

                    break;
            }
        }
        throw new Exception("Never got there");
    }
}