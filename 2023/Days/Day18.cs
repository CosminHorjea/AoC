using System.Numerics;
using System.Text.Json;

class Day18 : Solution
{
    class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
    public string Part1()
    {
        var content = File.ReadAllLines("Inputs/Day18.in");
        HashSet<(int, int)> seen = new HashSet<(int, int)>();
        HashSet<Point> points = new HashSet<Point>();
        var currrentPoint = new Point(0, 0);
        points.Add(currrentPoint);
        var boundry = 0;
        foreach (var line in content)
        {
            var parts = line.Split(" ");
            var dir = parts[0][0];
            var val = int.Parse(parts[1]);
            boundry += val;
            var color = parts[2];
            Point nextPoint = new Point(0, 0);
            switch (dir)
            {
                case 'R':
                    nextPoint = new Point(currrentPoint.X + val, currrentPoint.Y);
                    break;
                case 'D':
                    nextPoint = new Point(currrentPoint.X, currrentPoint.Y - val);
                    break;
                case 'L':
                    nextPoint = new Point(currrentPoint.X - val, currrentPoint.Y);
                    break;
                case 'U':
                    nextPoint = new Point(currrentPoint.X, currrentPoint.Y + val);
                    break;
                default:
                    throw new Exception("Unknown direction");
            }
            currrentPoint = nextPoint;
            points.Add(nextPoint);
        }
        var listOfPoints = points.ToList();
        int n = listOfPoints.Count;
        var sum = 0;
        for (int i = 0; i < n - 1; i++)
        {
            var p1 = listOfPoints[i];
            var p2 = listOfPoints[i + 1];
            sum += p1.X * p2.Y - p1.Y * p2.X;
        }
        var area = Math.Abs(0.5 * sum);
        var interior = area - boundry / 2 + 1;
        Console.WriteLine($"{area} {boundry}");
        return boundry + interior + "";
    }
    // 50648 high

    class Point2
    {
        public BigInteger X { get; set; }
        public BigInteger Y { get; set; }
        public Point2(BigInteger x, BigInteger y)
        {
            X = x;
            Y = y;
        }
    }
    public string Part2()
    {
        var content = File.ReadAllLines("Inputs/Day18.in");
        HashSet<Point2> points = new HashSet<Point2>();
        var currrentPoint = new Point2(0, 0);
        points.Add(currrentPoint);
        BigInteger boundry = 0;
        foreach (var line in content)
        {
            var parts = line.Split(" ");
            var color = parts[2].Trim('(');
            var dir = color[6];
            Console.WriteLine(new string(color.Skip(1).Take(5).ToArray()));
            BigInteger val = int.Parse(new string(color.Skip(1).Take(5).ToArray()), System.Globalization.NumberStyles.HexNumber);
            boundry += val;
            Point2 nextPoint = new Point2(0, 0);
            switch (dir)
            {
                case '0':
                    nextPoint = new Point2(currrentPoint.X + val, currrentPoint.Y);
                    break;
                case '1':
                    nextPoint = new Point2(currrentPoint.X, currrentPoint.Y - val);
                    break;
                case '2':
                    nextPoint = new Point2(currrentPoint.X - val, currrentPoint.Y);
                    break;
                case '3':
                    nextPoint = new Point2(currrentPoint.X, currrentPoint.Y + val);
                    break;
                default:
                    throw new Exception("Unknown direction");
            }
            currrentPoint = nextPoint;
            points.Add(nextPoint);
        }
        var listOfPoints = points.ToList();
        int n = listOfPoints.Count;
        BigInteger sum = 0;
        for (int i = 0; i < n - 1; i++)
        {
            var p1 = listOfPoints[i];
            var p2 = listOfPoints[i + 1];
            sum += p1.X * p2.Y - p1.Y * p2.X;
        }
        BigInteger area = sum / 2;
        if (area.Sign < 0)
        {
            area *= -1;
        }
        BigInteger interior = area - boundry / 2 + 1;
        Console.WriteLine($"{area} {boundry}");
        return boundry + interior + "";

    }
}