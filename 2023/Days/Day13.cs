using System.Runtime.InteropServices.Marshalling;
using System.Text.Json;

class Day13 : Solution
{


    public string Part1()
    {
        var content = File.ReadAllText("Inputs/Day13.in");
        var maps = content.Split("\r\n\r\n").Select(s => s.Split('\n').Select(s => s.Trim()).ToList()).ToList();
        var ans = 0;
        foreach (var map in maps)
        {
            int rows = map.Count;
            int cols = map[0].Count();
            var horizontal_index = getReflectionIndex(map, rows);
            if (horizontal_index > -1)
            {
                ans += 100 * horizontal_index;
                continue;
            }
            var vertical_index = 1;
            var transposed_map = TransposeMatrix(map);
            vertical_index = getReflectionIndex(transposed_map, cols);
            ans += vertical_index;
        }
        return $"{ans}";
    }

    private static int getReflectionIndex(List<string> map, int rows)
    {
        var horizontal_index = 1;
        while (horizontal_index < rows)
        {
            int i = 0, j = -1;
            while (string.Equals(map[horizontal_index + i], map[horizontal_index + j]))
            {
                i++; j--;
                if (horizontal_index + i >= rows || horizontal_index + j < 0)
                {
                    return horizontal_index;
                }
            }
            horizontal_index++;
        }
        return -1;
    }

    List<string> TransposeMatrix(List<string> map)
    {
        List<string> transposedList =
            map.SelectMany((row, rowIndex) => row.Select((value, colIndex) => new { value, colIndex }))
            .GroupBy(x => x.colIndex, x => x.value)
            .Select(group => new string(group.ToArray()))
            .ToList();
        return transposedList;
    }

    public string Part2()
    {
        var content = File.ReadAllText("Inputs/Day13.in");
        var maps = content.Split("\r\n\r\n").Select(s => s.Split('\n').Select(s => s.Trim()).ToList()).ToList();
        var ans = 0;
        foreach (var map in maps)
        {
            int rows = map.Count;
            int cols = map[0].Count();
            Console.WriteLine($"{rows},{cols}");
            foreach (var line in map)
            {
                Console.WriteLine(JsonSerializer.Serialize(line));
            }
            var horizontal_index = getReflectionIndexPart2(map, rows);
            if (horizontal_index > -1)
            {
                Console.WriteLine("Found early " + horizontal_index);
                ans += 100 * horizontal_index;
                continue;
            }
            var vertical_index = 1;
            var transposed_map = TransposeMatrix(map);
            Console.WriteLine("-------------");
            foreach (var line in transposed_map)
            {
                Console.WriteLine(JsonSerializer.Serialize(line));
            }
            vertical_index = getReflectionIndexPart2(transposed_map, cols);
            if (vertical_index < 0)
            {
                throw new Exception("something bad");
            }
            Console.WriteLine("Vertical index: " + vertical_index);
            ans += vertical_index;
            Console.WriteLine("Ans:" + ans);
        }
        return $"{ans}";
    }
    // 7345 too low
    // 39714 too high
    private static int getReflectionIndexPart2(List<string> map, int rows)
    {
        var horizontal_index = 1;
        while (horizontal_index < rows)
        {
            int i = 0, j = -1;
            int diff = 0;
            while (countDifferences(map[horizontal_index + i], map[horizontal_index + j]) <= 1)
            {
                diff += countDifferences(map[horizontal_index + i], map[horizontal_index + j]);
                i++; j--;
                if (horizontal_index + i >= rows || horizontal_index + j < 0)
                {
                    if (diff != 1)
                        break;
                    return horizontal_index;
                }
            }
            horizontal_index++;
        }
        return -1;
    }

    private static int countDifferences(string v1, string v2)
    {
        if (v1.Length != v2.Length)
        {
            throw new ArgumentException("Values should have same length");
        }
        return v1.Zip(v2).Where(a => a.First != a.Second).Count();
    }
};

