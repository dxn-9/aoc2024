namespace aoc2024.Day19;

public static class Day19
{
    public static long ContainsPattern(string s, HashSet<string> patterns, Dictionary<string, long> memo)
    {
        if (s.Length == 0) return 1;
        if (memo.TryGetValue(s, out var v)) return v;

        long count = 0;

        for (int i = 1; i <= s.Length; i++)
        {
            var substr = s[..i];
            if (patterns.Contains(substr))
            {
                var r = ContainsPattern(s[i..], patterns, memo);
                memo[s[i..]] = r;
                count += r;
            }
        }

        return count;
    }


    public static int Solve1(string input)
    {
        var patterns = input.Split("\n\n")[0].Split(',').Select(f => f.Trim()).ToHashSet();
        var memo = new Dictionary<string, long>();
        return input.Split("\n\n")[1].Split('\n'
        ).Count(pattern =>
            ContainsPattern(pattern, patterns, memo) > 0
        );
    }

    public static ulong Solve2(string input)
    {
        var patterns = input.Split("\n\n")[0].Split(',').Select(f => f.Trim()).ToHashSet();
        var memo = new Dictionary<string, long>();
        return (ulong)input.Split("\n\n")[1].Split('\n'
        ).Sum(pattern =>
            ContainsPattern(pattern, patterns, memo));
    }
}