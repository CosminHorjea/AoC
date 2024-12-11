class Day8 : Solution
{
    public string Part1()
    {
        var map = File.ReadAllLines("Inputs\\Day8.in");
        var answer = 0;
        int n = map.Length;
        int m = map[0].Length;
        HashSet<(int, int)> antinodes = new HashSet<(int, int)>();
        Dictionary<int, List<(int, int)>> antennas = new Dictionary<int, List<(int, int)>>();
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                if (map[i][j] == '.')
                {
                    continue;
                }

                if (antennas.ContainsKey(map[i][j]))
                {
                    antennas[map[i][j]].Add((i, j));
                }
                else
                {
                    antennas[map[i][j]] = new List<(int, int)> { (i, j) };
                }
            }
        }

        foreach (var antenna in antennas)
        {
            var coords = antenna.Value;
            for (int i = 0; i < coords.Count; i++)
            {
                for (int j = i + 1; j < coords.Count; j++)
                {
                    var x1 = coords[i].Item1;
                    var y1 = coords[i].Item2;
                    var x2 = coords[j].Item1;
                    var y2 = coords[j].Item2;
                    var anti1 = (x1 - x2 + x1, y1 - y2 + y1);
                    var anti2 = (x2 - x1 + x2, y2 - y1 + y2);
                    if (anti1.Item1 >= 0 && anti1.Item1 < n && anti1.Item2 >= 0 && anti1.Item2 < m)
                    {
                        antinodes.Add(anti1);
                    }
                    if (anti2.Item1 >= 0 && anti2.Item1 < n && anti2.Item2 >= 0 && anti2.Item2 < m)
                    {
                        antinodes.Add(anti2);
                    }
                }
            }
        }

        answer = antinodes.Count;
        return "" + answer;
    }

    public string Part2()
    {
        var map = File.ReadAllLines("Inputs\\Day8.in");
        var answer = 0;
        int n = map.Length;
        int m = map[0].Length;
        HashSet<(int, int)> antinodes = new HashSet<(int, int)>();
        Dictionary<int, List<(int, int)>> antennas = new Dictionary<int, List<(int, int)>>();
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                if (map[i][j] == '.')
                {
                    continue;
                }

                if (antennas.ContainsKey(map[i][j]))
                {
                    antennas[map[i][j]].Add((i, j));
                }
                else
                {
                    antennas[map[i][j]] = new List<(int, int)> { (i, j) };
                }
            }
        }

        foreach (var antenna in antennas)
        {
            var coords = antenna.Value;
            for (int i = 0; i < coords.Count; i++)
            {
                antinodes.Add(coords[i]);
                for (int j = i + 1; j < coords.Count; j++)
                {
                    antinodes.Add(coords[j]);
                    var x1 = coords[i].Item1;
                    var y1 = coords[i].Item2;
                    var x2 = coords[j].Item1;
                    var y2 = coords[j].Item2;
                    var dx12 = x1 - x2;
                    var dy12 = y1 - y2;
                    var dx21 = x2 - x1;
                    var dy21 = y2 - y1;
                    var anti1 = (x1 - x2 + x1, y1 - y2 + y1);
                    var anti2 = (x2 - x1 + x2, y2 - y1 + y2);
                    while (anti1.Item1 >= 0 && anti1.Item1 < n && anti1.Item2 >= 0 && anti1.Item2 < m)
                    {
                        antinodes.Add(anti1);
                        anti1 = (anti1.Item1 + dx12, anti1.Item2 + dy12);
                    }
                    while (anti2.Item1 >= 0 && anti2.Item1 < n && anti2.Item2 >= 0 && anti2.Item2 < m)
                    {
                        antinodes.Add(anti2);
                        anti2 = (anti2.Item1 + dx21, anti2.Item2 + dy21);
                    }
                }
            }
        }

        answer = antinodes.Count;
        return "" + answer;
    }
}