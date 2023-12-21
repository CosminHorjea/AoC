using System.Runtime.CompilerServices;

class Day21 : Solution
{
    public string Part1()
    {
        var content = File.ReadAllLines("Inputs/Day21.in");
        HashSet<(int, int)> rocks = new HashSet<(int, int)>();
        (int, int) start = (0, 0);
        int n = content.Count();
        int m = content[0].Count();
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                if (content[i][j] == 'S')
                {
                    start = (i, j);
                }
                if (content[i][j] == '#')
                {
                    rocks.Add((i, j));
                }
            }
        }
        int STEPS = 64;
        List<(int, int)> directions = [(-1, 0), (0, 1), (0, -1), (1, 0)];
        HashSet<(int, int)> reached = [start];
        for (int iter = 0; iter < STEPS; iter++)
        {
            HashSet<(int, int)> next_reached = new HashSet<(int, int)>();
            foreach (var a in reached)
            {
                foreach (var dir in directions)
                {
                    int next_i = a.Item1 + dir.Item1;
                    int next_j = a.Item2 + dir.Item2;
                    if (!rocks.Contains((next_i, next_j)))
                    {
                        next_reached.Add((next_i, next_j));
                    }
                }
            }
            reached = new HashSet<(int, int)>(next_reached);
        }
        return $"{reached.Count}";
    }

    public string Part2()
    {
        throw new NotImplementedException();
    }
}