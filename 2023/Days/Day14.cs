using System.Runtime.CompilerServices;

class Day14 : Solution
{
    public string Part1()
    {
        var contents = File.ReadAllLines("Inputs/Day14.in");
        var map = contents.Select(c => c.ToList()).ToList();
        int rows = map.Count();
        int cols = map[0].Count();
        moveBallsUp(map, rows, cols);
        int ans = 0;
        foreach (var line in map)
        {
            // Console.WriteLine(line.ToArray());
        }
        for (int i = 0; i < rows; i++)
        {
            ans += map[i].Where(c => c == 'O').Count() * (rows - i);
        }
        return $"{ans}";
    }
    // 196751 high

    private void moveBallsUp(List<List<char>> map, int rows, int cols)
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (map[i][j] == 'O')
                {
                    int aux = i - 1;
                    while (aux >= 0 && map[aux][j] != '#' && map[aux][j] != 'O')
                    {
                        map[aux][j] = 'O';
                        map[aux + 1][j] = '.';
                        aux--;
                    }
                }
            }
        }
    }
    private void moveBallsDown(List<List<char>> map, int rows, int cols)
    {
        for (int i = rows - 1; i >= 0; i--)
        {
            for (int j = 0; j < cols; j++)
            {
                if (map[i][j] == 'O')
                {
                    int aux = i + 1;
                    while (aux < rows && map[aux][j] != '#' && map[aux][j] != 'O')
                    {
                        map[aux][j] = 'O';
                        map[aux - 1][j] = '.';
                        aux++;
                    }
                }
            }
        }
    }

    private void moveBallsEast(List<List<char>> map, int rows, int cols)
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = cols - 1; j >= 0; j--)
            {
                if (map[i][j] == 'O')
                {
                    int aux = j + 1;
                    while (aux < cols && map[i][aux] != '#' && map[i][aux] != 'O')
                    {
                        map[i][aux] = 'O';
                        map[i][aux - 1] = '.';
                        aux++;
                    }
                }
            }
        }
    }

    private void moveBallsWest(List<List<char>> map, int rows, int cols)
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (map[i][j] == 'O')
                {
                    int aux = j - 1;
                    while (aux >= 0 && map[i][aux] != '#' && map[i][aux] != 'O')
                    {
                        map[i][aux] = 'O';
                        map[i][aux + 1] = '.';
                        aux--;
                    }
                }
            }
        }
    }

    public string Part2()
    {
        var contents = File.ReadAllLines("Inputs/Day14.in");
        var map = contents.Select(c => c.ToList()).ToList();
        int rows = map.Count();
        int cols = map[0].Count();
        var oldresistence = 0;
        for (var i = 0; i < 10000; i++)
        {
            if (i % 1000000 == 0)
            {
                Console.WriteLine(i);
            }
            moveBallsUp(map, rows, cols);
            moveBallsWest(map, rows, cols);
            moveBallsDown(map, rows, cols);
            moveBallsEast(map, rows, cols);
            var newresistence = getResistance(map, rows, cols);
            Console.WriteLine($"{i} {newresistence}");
            // 91016 too high
            // 91038 too high
            // 90982
            // soo this is a hand solution
            // you can log the results and a cycle will appear, with a given length, and the n you can calculate to see the reminder of 1000000000 with that length, and then you can use that answer, i have to get back to this


        }
        return "";
    }
    // 96701 too high

    private int getResistance(List<List<char>> map, int rows, int cols)
    {
        var ans = 0;
        for (int i = 0; i < rows; i++)
        {
            ans += map[i].Where(c => c == 'O').Count() * (rows - i);
        }
        return ans;
    }
}