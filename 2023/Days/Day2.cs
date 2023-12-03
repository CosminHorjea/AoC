using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Nodes;

class Day2 : Solution
{
    public string Part1()
    {
        var input = File.ReadAllLines("Inputs/Day2.in");
        var ans = 0;
        Dictionary<string, int> maximumCubes = new Dictionary<string, int> {
            {"red",12},
            {"green",13},
            {"blue",14}
        };

        foreach (var line in input)
        {
            var gameID = int.Parse(line.Split(":")[0].Split(" ")[1]);
            var games = line.Split(": ")[1].Split("; ");
            bool flag = true;
            foreach (var game in games)
            {
                if (!isPossible(game, maximumCubes))
                {
                    flag = false;
                }
            }
            if (flag)
            {
                ans += gameID;
            }
        }
        return "" + ans;
    }

    private bool isPossible(string game, IDictionary<string, int> maximum)
    {
        foreach (var round in game.Split(", "))
        {
            var cubePairs = round.Split(" ");
            var color = cubePairs[1];
            var number = int.Parse(cubePairs[0]);
            if (maximum[color] < number)
            {
                // Console.WriteLine("Its not possible because " + number);
                return false;
            }
        }
        return true;
    }

    public string Part2()
    {
        var input = File.ReadAllLines("Inputs/Day2.in");
        var ans = 0;
        foreach (var line in input)
        {
            var gameID = int.Parse(line.Split(":")[0].Split(" ")[1]);
            Dictionary<string, int> minimumCubes = new Dictionary<string, int>{
                {"red",int.MinValue},
                {"blue",int.MinValue},
                {"green",int.MinValue}
            };
            var draws = line.Split(": ")[1].Split("; ");
            foreach (var draw in draws)
            {
                foreach (var cubePair in draw.Split(", "))
                {
                    var cubeNum = int.Parse(cubePair.Split(" ")[0]);
                    var cubeColor = cubePair.Split(" ")[1];
                    if (minimumCubes[cubeColor] < cubeNum)
                    {
                        minimumCubes[cubeColor] = cubeNum;
                    }
                }
            }
            ans += minimumCubes.Values.Aggregate((a, x) => a * x);
            // Console.WriteLine(JsonSerializer.Serialize(minimumCubes.Values));
            // Console.WriteLine(minimumCubes.Values.Aggregate((a, x) => a * x));
        }
        return "" + ans;
    }
}