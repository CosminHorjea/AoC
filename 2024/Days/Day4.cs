class Day4 : Solution
{
    public string Part1()
    {
        var input = File.ReadAllLines("Inputs\\Day4.in");
        var answer = 0;
        int n = input.Length;
        int m = input[0].Length;
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                if (input[i][j] == 'X')
                {
                    answer += searchInAllDirections(input, i, j, n, m);
                }
            }
        }


        return "" + answer;
    }

    private int searchInAllDirections(string[] input, int i, int j, int n, int m, string word = "XMAS")
    {
        var ans = 0;
        var directions = new int[] { -1, 0, 1 };
        for (int x = 0; x < directions.Length; x++)
        {
            for (int y = 0; y < directions.Length; y++)
            {
                ans += searchInDirection(input, i, j, n, m, directions[x], directions[y], word);
            }
        }
        // Console.WriteLine($"i: {i}, j: {j}, ans: {ans}");
        return ans;
    }

    private int searchInDirection(string[] input, int i, int j, int n, int m, int v1, int v2, string word)
    {
        var x = i + v1;
        var y = j + v2;
        var index = 1;
        while (x >= 0 && x < n && y >= 0 && y < m && index < word.Length)
        {
            if (input[x][y] != word[index])
            {
                return 0;
            }
            x += v1;
            y += v2;
            index++;
        }

        return index == word.Length ? 1 : 0;
    }

    public string Part2()
    {
        var input = File.ReadAllLines("Inputs\\Day4.in");
        var answer = 0;
        int n = input.Length;
        int m = input[0].Length;
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                if (input[i][j] == 'A' && i > 0 && j > 0 && i < n - 1 && j < m - 1)
                {
                    answer += IsXMas(input, i, j, n, m);
                }
            }
        }
        return "" + answer;
    }

    private int IsXMas(string[] input, int i, int j, int n, int m)
    {
        var leftUpper = input[i - 1][j - 1];
        var rightUpper = input[i - 1][j + 1];
        var leftLower = input[i + 1][j - 1];
        var rightLower = input[i + 1][j + 1];

        if (leftUpper == 'M' && leftLower == 'M' && rightUpper == 'S' && rightLower == 'S')
        {
            return 1;
        }

        if (rightUpper == 'M' && rightLower == 'M' && leftUpper == 'S' && leftLower == 'S')
        {
            return 1;
        }

        if (leftUpper == 'M' && rightUpper == 'M' && leftLower == 'S' && rightLower == 'S')
        {
            return 1;
        }

        if (leftLower == 'M' && rightLower == 'M' && leftUpper == 'S' && rightUpper == 'S')
        {
            return 1;
        }

        return 0;
    }
}