using System.Text.RegularExpressions;

namespace aoc2024.Day3;

public class Day3
{
    public static int Solve1(string input)
        =>
            Regex.Matches(input, @"mul\(([0-9]{1,3},[0-9]{1,3})\)")
                .Select(match => match.Groups.Values.Last().ToString().Split(","))
                .Aggregate(0, (i, match) =>
                    i + int.Parse(match[0]) *
                    int.Parse(match[1])
                );

    public static int Solve2(string input)
        => Regex.Matches(input, @"(?:(mul\(([0-9]{1,3},[0-9]{1,3})\))|(do(?:n't)?\(\)))")
            .Select(match =>
                (string.IsNullOrEmpty(match.Groups[2].ToString()) ? match.Groups[3] : match.Groups[2]).ToString())
            .Aggregate((0, true), (tuple, s) =>
                s[0] switch
                {
                    'd' => (tuple.Item1, s != "don't()"),
                    _ => tuple.Item2
                        ? (tuple.Item1 + int.Parse(s.Split(",")[0]) * int.Parse(s.Split(",")[1]), true)
                        : tuple,
                }).Item1;
}