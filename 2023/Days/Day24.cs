using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.Marshalling;
using System.Xml;

class Day24 : Solution
{
    class Line
    {
        public double m { get; set; }
        public double start_x { get; set; }
        public double start_vel { get; set; }
        public double b { get; set; }

        public Line(string line)
        {
            var parts = line.Split(" @ ");
            var currentPos = parts[0].Split(", ").Select(double.Parse).ToList();
            var velocity = parts[1].Split(", ").Select(double.Parse).ToList();
            m = velocity[1] / velocity[0];
            b = m * (-1 * currentPos[0]) + currentPos[1];
            start_x = currentPos[0];
            start_vel = velocity[0];
        }
    }
    public string Part1()
    {
        var contents = File.ReadAllLines("Inputs/Day24.in");
        var lines = new List<Line>();
        foreach (var line in contents)
        {
            lines.Add(new Line(line));
        }
        // var low_boundary = 7;
        // var up_boundary = 27;
        var low_boundary = 200000000000000;
        var up_boundary = 400000000000000;
        int n = lines.Count;
        var ans = 0;
        for (var i = 0; i < n - 1; i++)
        {
            for (var j = i + 1; j < n; j++)
            {
                if (crosses(lines[i], lines[j], low_boundary, up_boundary))
                {
                    ans += 1;
                }
            }
        }
        return $"{ans}";
    }

    private bool crosses(Line line1, Line line2, double lb, double ub)
    {
        if (line1.m == line2.m)
        {
            return false;
        }
        var x = (line2.b - line1.b) / (line1.m - line2.m);
        if (x < lb || x > ub)
        {
            return false;
        }
        var y = line1.m * x + line1.b;
        if (y < lb || y > ub)
        {
            return false;
        }
        return isInFuture(line1, x) && isInFuture(line2, x);

    }

    private static bool isInFuture(Line line, double x)
    {
        if (x > line.start_x && line.start_vel < 0)
        {
            return false;
        }
        if (x < line.start_x && line.start_vel > 0)
        {
            return false;
        }
        return true;
    }

    public string Part2()
    {
        throw new NotImplementedException();
    }
}