using System.Runtime.CompilerServices;

class Day10 : Solution
{
    public static Dictionary<char, List<(int, int)>> moves = new Dictionary<char, List<(int, int)>>{
            {'|',new List<(int,int)>(){(-1,0),(1,0)}},
            {'-',new List<(int,int)>(){(0,1),(0,-1)}},
            {'L',new List<(int,int)>(){(-1,0),(0,1)}},
            {'J',new List<(int,int)>(){(-1,0),(0,-1)}},
            {'F',new List<(int,int)>(){(1,0),(0,1)}},
            {'7',new List<(int,int)>(){(1,0),(0,-1)}},
        };
    const string PIPES = "|-LJ7F";
    public string Part1()
    {
        var contest = File.ReadAllLines("Inputs/Day10.in");
        List<List<char>> map = contest.Select(c => c.ToList()).ToList();
        var n = map.Count();
        var m = map[0].Count;
        (int, int) starting_position = (0, 0);
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                if (map[i][j] == 'S')
                {
                    starting_position = (i, j);
                    break;
                }
            }
        }
        Queue<((int, int), int)> q = new Queue<((int, int), int)>();
        HashSet<(int, int)> visited = new HashSet<(int, int)>();
        q.Enqueue((starting_position, 0));
        var ans = 0;
        while (q.Count > 0)
        {
            var current = q.Dequeue();
            var label = map[current.Item1.Item1][current.Item1.Item2];
            visited.Add(current.Item1);
            if (label == 'S')
            {
                label = 'J'; // ill just hardcode this
            }
            foreach (var direction in moves[label])
            {
                var next_i = current.Item1.Item1 + direction.Item1;
                var next_j = current.Item1.Item2 + direction.Item2;
                if (next_i < 0 || next_i >= n || next_j < 0 || next_j >= m)
                {
                    continue;
                }
                if (PIPES.IndexOf(map[next_i][next_j]) == -1)
                {
                    continue;
                }
                if (visited.Contains((next_i, next_j)))
                {
                    continue;
                }
                q.Enqueue(((next_i, next_j), current.Item2 + 1));
            }
            ans = Math.Max(ans, current.Item2);
        }
        return $"{ans}";
    }

    public string Part2()
    {
        var contest = File.ReadAllLines("Inputs/Day10.in");
        List<List<char>> map = contest.Select(c => c.ToList()).ToList();
        var n = map.Count();
        var m = map[0].Count;
        (int, int) starting_position = (0, 0);
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                if (map[i][j] == 'S')
                {
                    starting_position = (i, j);
                    break;
                }
            }
        }
        Queue<(int, int)> q = new Queue<(int, int)>();
        HashSet<(int, int)> main_loop = new HashSet<(int, int)>();
        q.Enqueue(starting_position);
        while (q.Count > 0)
        {
            var current = q.Dequeue();
            var label = map[current.Item1][current.Item2];
            main_loop.Add((current.Item1, current.Item2));
            if (label == 'S')
            {
                label = 'J'; // ill just hardcode this
            }
            foreach (var direction in moves[label])
            {
                var next_i = current.Item1 + direction.Item1;
                var next_j = current.Item2 + direction.Item2;
                if (next_i < 0 || next_i >= n || next_j < 0 || next_j >= m)
                {
                    continue;
                }
                if (PIPES.IndexOf(map[next_i][next_j]) == -1)
                {
                    continue;
                }
                if (main_loop.Contains((next_i, next_j)))
                {
                    continue;
                }
                q.Enqueue((next_i, next_j));
            }
        }
        HashSet<(int, int)> visited = new HashSet<(int, int)>();
        var ans = 0;
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                if (isInside((i, j), map, main_loop))
                {
                    ans += 1;
                }
            }
        }
        return $"{ans}";
    }

    private bool isInside((int i, int j) value, List<List<char>> map, HashSet<(int, int)> main_loop)
    {
        if (main_loop.Contains((value.i, value.j)))
        {
            return false;
        }
        var jumps = 0;
        while (value.j >= 0)
        {
            if (main_loop.Contains((value.i, value.j)) && "L|JS".IndexOf(map[value.i][value.j]) != -1)
            {
                jumps++;
            }
            value.j--;
        }
        return jumps % 2 != 0;
    }
}
// 7238 -- too high
// 297