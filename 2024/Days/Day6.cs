class Day6 : Solution
{
    public string Part1()
    {
        var map = File.ReadAllLines("Inputs\\Day6.in");
        var answer = 0;
        int n = map.Length;
        int m = map[0].Length;
        HashSet<(int, int)> obstacles = new HashSet<(int, int)>();
        (int, int) explorer = (0, 0);
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                switch (map[i][j])
                {
                    case '#':
                        obstacles.Add((i, j));
                        break;
                    case '^':
                        explorer = (i, j);
                        break;
                }
            }
        }
        HashSet<(int, int)> visited = new HashSet<(int, int)>();
        visited.Add(explorer);
        List<(int, int)> directions = new List<(int, int)> { (-1, 0), (0, 1), (1, 0), (0, -1) };
        var currentDirection = 0;

        while (true)
        {
            var next = (explorer.Item1 + directions[currentDirection].Item1, explorer.Item2 + directions[currentDirection].Item2);
            if (obstacles.Contains(next))
            {
                currentDirection = (currentDirection + 1) % 4;
                continue;
            }
            if (next.Item1 < 0 || next.Item1 >= n || next.Item2 < 0 || next.Item2 >= m)
            {
                break;
            }
            visited.Add(next);
            explorer = next;
        }

        answer = visited.Count;
        return "" + answer;
    }

    public string Part2()
    {
        var map = File.ReadAllLines("Inputs\\Day6.in");
        var answer = 0;
        int n = map.Length;
        int m = map[0].Length;
        HashSet<(int, int)> obstacles = new HashSet<(int, int)>();
        (int, int) explorer = (0, 0);
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                switch (map[i][j])
                {
                    case '#':
                        obstacles.Add((i, j));
                        break;
                    case '^':
                        explorer = (i, j);
                        break;
                }
            }
        }

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                var newObstacles = new HashSet<(int, int)>(obstacles);
                newObstacles.Add((i, j));
                if (explorer != (i, j) && map[i][j] != '#' && IsLooping(n, m, newObstacles, explorer))
                {
                    answer++;
                }
            }
        }

        return "" + answer;
    }

    private static bool IsLooping(int n, int m, HashSet<(int, int)> obstacles, (int, int) explorer)
    {
        HashSet<((int, int), int)> visited = new HashSet<((int, int), int)>();
        List<(int, int)> directions = new List<(int, int)> { (-1, 0), (0, 1), (1, 0), (0, -1) };
        var currentDirection = 0;
        visited.Add((explorer, currentDirection));

        while (true)
        {
            var next = (explorer.Item1 + directions[currentDirection].Item1, explorer.Item2 + directions[currentDirection].Item2);
            if (obstacles.Contains(next))
            {
                currentDirection = (currentDirection + 1) % 4;
                continue;
            }
            if (next.Item1 < 0 || next.Item1 >= n || next.Item2 < 0 || next.Item2 >= m)
            {
                break;
            }
            if (visited.Contains((next, currentDirection)))
            {
                return true;
            }
            visited.Add((next, currentDirection));
            explorer = next;
        }

        return false;
    }
}