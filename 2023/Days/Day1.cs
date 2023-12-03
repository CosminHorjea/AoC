
using System.Security.Cryptography;
using System.Text.Json;
using Microsoft.VisualBasic;

class Day1 : Solution
{
    public string Part1()
    {
        var input = File.ReadAllLines("Inputs\\Day1.in");
        var answer = 0;
        foreach (var line in input)
        {
            var digits = getDigits(line);
            // Console.WriteLine(JsonSerializer.Serialize(digits));
            answer += int.Parse("" + digits[0] + digits[digits.Count - 1]);

        }
        return "" + answer;
    }

    private List<char> getDigits(string line)
    {
        return line.Where(c => char.IsDigit(c)).ToList();
    }

    public string Part2()
    {
        var input = File.ReadAllLines("Inputs\\Day1.in");
        var answer = 0;
        foreach (var line in input)
        {
            var digits = getAllDigits(line);
            answer += digits;
        }
        return "" + answer;
    }

    private int getAllDigits(string line)
    {
        var digits = "0123456789";
        List<string> spelledDigits = ["zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine"];
        Dictionary<string, int> reverseDigitLookup = new Dictionary<string, int> {
            {"one",1},
            {"two",2},
            {"three",3},
            {"four",4},
            {"five",5},
            {"six",6},
            {"seven",7},
            {"eight",8},
            {"nine",9},
            {"zero",0},
         };
        Tuple<int, int> earliestDigit = new Tuple<int, int>(9999, 0);
        Tuple<int, int> latestDigit = new Tuple<int, int>(-1, 0);
        foreach (var sd in spelledDigits)
        {
            var firstIndex = line.IndexOf(sd);
            var lastIndex = line.LastIndexOf(sd);
            if (earliestDigit.Item1 > firstIndex && firstIndex > -1)
            {
                earliestDigit = Tuple.Create(firstIndex, reverseDigitLookup[sd]);
            }
            if (latestDigit.Item1 < lastIndex && lastIndex > -1)
            {
                latestDigit = Tuple.Create(lastIndex, reverseDigitLookup[sd]);
            }
        }
        foreach (var d in digits)
        {
            var firstIndex = line.IndexOf(d);
            var lastIndex = line.LastIndexOf(d);
            if (earliestDigit.Item1 > firstIndex && firstIndex > -1)
            {
                earliestDigit = Tuple.Create(firstIndex, d - '0');
            }
            if (latestDigit.Item1 < lastIndex && lastIndex > -1)
            {
                latestDigit = Tuple.Create(lastIndex, d - '0');
            }
        }

        // Alternative solution, worth coming back to
        // for (var i = 0; i < line.Count(); i++)
        // {
        //     if (char.IsDigit(line[i]))
        //     {
        //         earliestDigit = Tuple.Create(i, line[i] - '0');
        //         break;
        //     }
        //     foreach (var d in spelledDigits)
        //     {
        //         var s = new string(line.Skip(i).ToArray());
        //         if (s.StartsWith(d))
        //         {
        //             earliestDigit = Tuple.Create(i, reverseDigitLookup[d]);
        //             break;
        //         }
        //     }
        // }

        // for (var i = line.Count() - 1; i >= 0; i--)
        // {
        //     if (char.IsDigit(line[i]))
        //     {
        //         latestDigit = Tuple.Create(i, line[i] - '0');
        //         break;
        //     }
        //     foreach (var d in spelledDigits)
        //     {
        //         if (line.Reverse().Skip(i).ToString().StartsWith(d.Reverse().ToString()))
        //         {
        //             latestDigit = Tuple.Create(i, reverseDigitLookup[d]);
        //             break;
        //         }
        //     }
        // }


        return int.Parse(earliestDigit.Item2.ToString() + latestDigit.Item2.ToString());
    }
}