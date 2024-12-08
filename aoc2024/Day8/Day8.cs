using System.Diagnostics;
using System.Text.RegularExpressions;

namespace aoc2024.Day8;

public static class Day8
{
    public static long Solve1(string input)
    {
        var lines = input.Split(Environment.NewLine);
        var size = lines.Length;
        // TODO: look if there is a way to do this via select
        return lines.Index().Aggregate(new Dictionary<char, List<(int, int)>>(), (dictionary, line) =>
        {
            var matches = Regex.Matches(line.Item, @"[^\.\s]");
            foreach (Match match in matches)
            {
                Debug.Assert(match.Length == 1);
                var ch = match.Value[0];
                dictionary[ch] = [..dictionary.GetValueOrDefault(ch, []), (match.Index, line.Index)];
            }

            return dictionary;
        }).SelectMany(kv =>
            {
                var coords = kv.Value;
                var r = coords.SelectMany((coord, index) =>
                    coords.Take(index).Concat(coords.Skip(index + 1)).Select(other => (first: coord, second: other))
                );
                return r;
            }
        ).Select(perm =>
        {
            var x = perm.second.Item1 - perm.first.Item1;
            var y = perm.second.Item2 - perm.first.Item2;
            return (x: perm.second.Item1 + x, y: perm.second.Item2 + y);
        }).Where(tuple =>
            tuple.x >= 0 && tuple.x < size && tuple.y >= 0 && tuple.y < size
        ).ToHashSet().Count;
    }

    public static long Solve2(string input)
    {
        var lines = input.Split(Environment.NewLine);
        var size = lines.Length;
        // TODO: look if there is a way to do this via select
        return lines.Index().Aggregate(new Dictionary<char, List<(int, int)>>(), (dictionary, line) =>
        {
            var matches = Regex.Matches(line.Item, @"[^\.\s]");
            foreach (Match match in matches)
            {
                Debug.Assert(match.Length == 1);
                var ch = match.Value[0];
                dictionary[ch] = [..dictionary.GetValueOrDefault(ch, []), (match.Index, line.Index)];
            }

            return dictionary;
        }).SelectMany(kv =>
            {
                var coords = kv.Value;
                var r = coords.SelectMany((coord, index) =>
                    coords.Take(index).Concat(coords.Skip(index + 1)).Select(other => (first: coord, second: other))
                );
                return r;
            }
        ).SelectMany(perm =>
        {
            var x = perm.second.Item1 - perm.first.Item1;
            var y = perm.second.Item2 - perm.first.Item2;
            var diagonalLength = (int)Math.Sqrt(Math.Pow(size, 2) + Math.Pow(size, 2));

            return Enumerable.Range(0, diagonalLength).Select(factor =>
                (x: perm.second.Item1 + x * factor, y: perm.second.Item2 + y * factor));
        }).Where(tuple =>
            tuple.x >= 0 && tuple.x < size && tuple.y >= 0 && tuple.y < size
        ).ToHashSet().Count;
    }
}