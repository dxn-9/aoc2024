using System.Text.RegularExpressions;

namespace aoc2024.Day4;

record struct Sequence((int, int) start, (int, int) end);

public static class Day4
{
    public static int Solve1(string input)
    {
        var lines = input.Split("\n");
        var s = lines.Length - 1; // Input is a square. So this works.
        var combinations = new List<Sequence>();
        var strs = new List<string>();

        for (var i = 0; i <= s; i++)
        {
            combinations.Add(new Sequence((i, 0), (i, s)));
            combinations.Add(new Sequence((i, 0), (s, s - i)));
            combinations.Add(new Sequence((s - i, 0), (0, s - i)));
            combinations.Add(new Sequence((0, i), (s, i)));
            if (i == 0) continue;
            combinations.Add(new Sequence((0, i), (s - i, s)));
            combinations.Add(new Sequence((s, i), (i, s)));
        }

        var count = 0;
        foreach (var seq in combinations)
        {
            var dx = seq.end.Item2 - seq.start.Item2;
            var dy = seq.end.Item1 - seq.start.Item1;

            if (dx + Math.Abs(dy) < 4) continue;

            var xi = dx == 0 ? 0 : 1;
            var yi = dy == 0 ? 0 : 1 * Math.Sign(dy);

            var str = "";
            for (int row = seq.start.Item1, col = seq.start.Item2;
                 row * Math.Sign(dy) <= seq.start.Item1 + dy &&
                 col <= seq.start.Item2 + dx;
                 row += yi, col += xi)
            {
                str += lines[row][col];
            }

            strs.Add(str);
        }

        foreach (var su in strs.Select(str => str.ToCharArray()))
        {
            count += Regex.Count(su, @"XMAS");
            Array.Reverse(su);
            count += Regex.Count(su, @"XMAS");
        }

        return count;
    }

    public static int Solve2(string input)
    {
        var lines = input.Split("\n");
        var s = lines.Length - 1;
        var strs = new List<string>();
        for (int i = 0; i <= s - 2; i++)
        {
            for (int j = 0; j <= s - 2; j++)
            {
                var str = "";
                for (int f = 0; f < 3; f++)
                {
                    for (int fx = 0; fx < 3; fx++)
                    {
                        str += $"{lines[i + f][j + fx]}";
                    }
                }

                strs.Add(str);
            }
        }

        var count = strs.Sum(block => block.IsMasBlock() ? 1 : 0);
        return count;
    }

    public static bool IsMas(this string str)
    {
        var chars = str.ToCharArray();
        var o = Regex.IsMatch(chars, @"MAS");
        Array.Reverse(chars);
        return o || Regex.IsMatch(chars, @"MAS");
    }

    public static bool IsMasBlock(this string str)
    {
        var r = str.ToCharArray();
        var r1 = new string(new[] { r[0], r[4], r[8] });
        var r2 = new string(new[] { r[6], r[4], r[2] });

        return r1.IsMas() && r2.IsMas();
    }
}