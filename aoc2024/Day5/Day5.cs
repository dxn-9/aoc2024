namespace aoc2024.Day5;

public static class Day5
{
    public static int Solve1(string input)
    {
        var sections = input.Split($"{Environment.NewLine}{Environment.NewLine}");
        var orders = sections[0].Split(Environment.NewLine).Select(s =>
                (int.Parse(s.Split("|")[0]), int.Parse(s.Split("|")[1])))
            .Aggregate(new Dictionary<int, int[]>(), (d, t) =>
                {
                    d[t.Item2] = [..d.GetValueOrDefault(t.Item2, []), t.Item1];
                    return d;
                }
            );
        var updates = sections[1].Split("\n").Select(seq => seq.Split(",").Select(int.Parse).ToList());
        return updates.Sum(update =>
            update.Index().All(t =>
                orders.GetValueOrDefault(t.Item, []).All(num =>
                    !update.TakeLast(update.Count - t.Index).Contains(num)
                )
            )
                ? update[update.Count / 2]
                : 0
        );
    }


    public static int Solve2(string input)
    {
        Console.WriteLine("hello");
        var sections = input.Split($"{Environment.NewLine}{Environment.NewLine}");
        var orders = sections[0].Split(Environment.NewLine).Select(s =>
                (int.Parse(s.Split("|")[0]), int.Parse(s.Split("|")[1])))
            .Aggregate(new Dictionary<int, int[]>(), (d, t) =>
                {
                    d[t.Item2] = [..d.GetValueOrDefault(t.Item2, []), t.Item1];
                    return d;
                }
            );
        var updates = sections[1].Split("\n").Select(seq => seq.Split(",").Select(int.Parse).ToList());
        var comparer = new Comparison<int>((a, b) =>
        {
            if (orders.ContainsKey(a) && orders[a].Contains(b))
            {
                return -1;
            }

            if (orders.ContainsKey(b) && orders[b].Contains(a))
            {
                return 1;
            }

            return 0;
        });
        return updates.Sum(update =>
        {
            var valid = update.Index().All(t =>
                orders.GetValueOrDefault(t.Item, []).All(num =>
                    !update.TakeLast(update.Count - t.Index).Contains(num)
                )
            );
            update.Sort(comparer);
            return !valid ? update[update.Count / 2] : 0;
        });
    }
}