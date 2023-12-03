using System.Reflection;

var day = DateTime.Now.Day;

var currentDaySolution = Assembly.GetExecutingAssembly().CreateInstance($"Day{day}");

var solution = currentDaySolution as Solution;

if (solution == null)
{
    throw new Exception("Not any class found");
}

Console.WriteLine($"Part 1: {solution.Part1()} ");
Console.WriteLine($"Part 2: {solution.Part2()} ");