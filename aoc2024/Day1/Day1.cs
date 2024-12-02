namespace aoc2024.Day1;

public static class Day1
{
    public static int Solve1(string input)
    {
        var (l1, l2) = input.Split("\n")
            .Select(line =>
                line.Split("   ")
                    .Select(num => Int32.Parse(num.Trim()))
                    .ToArray()
            )
            .Aggregate((new List<int>(), new List<int>()), (a, n) =>
            {
                a.Item1.Add(n[0]);
                a.Item2.Add(n[1]);
                return a;
            });
        l1.Sort();
        l2.Sort();
        return l1.Zip(l2).Aggregate(0, (a, t) => a += Math.Abs(t.First - t.Second));
    }

    public static int Solve2(string input)
    {
        var (l, d) = input.Split("\n")
            .Select(line =>
                line.Split("   ")
                    .Select(num => Int32.Parse(num.Trim()))
                    .ToArray()
            )
            .Aggregate((new List<int>(), new Dictionary<int, int>()), (a, n) =>
            {
                var (l, d) = a;
                l.Add(n[0]);
                d[n[1]] = d.GetValueOrDefault(n[1], 0) + 1;
                return a;
            });
        return l.Select(n => n * d.GetValueOrDefault(n, 0)).Sum();
    }
}