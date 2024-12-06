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
        var manageCompanyIds = new Dictionary<int, MonthlyUsageFullRecordDto>();
        // manageCompanyIds[1] = new MonthlyUsageFullRecordDto
        // {
        //     ManageCompanyId = 1,
        //     GravityZoneCompanyId = "1",
        //     EndpointMonthlyUsage = 1
        // };

        Console.WriteLine(manageCompanyIds[1]);
        return "";
    }
    public class MonthlyUsageFullRecordDto
    {
        public int ManageCompanyId { get; set; }
        public string GravityZoneCompanyId { get; set; }
        public int EndpointMonthlyUsage { get; set; }
    }
}