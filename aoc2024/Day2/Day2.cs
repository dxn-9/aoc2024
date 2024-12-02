namespace aoc2024.Day2;

public class Day2
{
    public static int Solve1(string input)
        => input.Split("\n")
            .Select(record => record.Split(" ").Select(c => Int32.Parse(c)))
            .Select(record =>
            {
                var l = record.ToList();
                var sign = Math.Sign(l.Last() - l.First());
                return l.Index().SkipLast(1).All(t =>
                    Enumerable.Range(1, 3)
                        .Contains(Math.Abs(l[t.Index + 1] - t.Item)) && Math.Sign(l[t.Index + 1] - t.Item) == sign)
                    ? 1
                    : 0;
            }).Sum();

    public static int Solve2(string input)
        => input.Split("\n")
            .Select(record => record.Split(" ").Select(c => Int32.Parse(c)))
            .Select(record =>
            {
                var l = record.ToList();
                var lists = Enumerable.Range(0, l.Count)
                    .Select(i => l.Slice(0, i).Concat(l.Slice(Math.Min(i + 1, l.Count - 1), l.Count - 1 - i)))
                    .ToList().Concat([l]);
                return lists.Any(list =>
                    {
                        var ll = list.ToList();
                        var sign = Math.Sign(ll.Last() - ll.First());
                        return ll.Index().SkipLast(1).All(t =>
                            Enumerable.Range(1, 3)
                                .Contains(Math.Abs(ll[t.Index + 1] - t.Item)) &&
                            Math.Sign(ll[t.Index + 1] - t.Item) == sign);
                    }
                )
                    ? 1
                    : 0;
            }).Sum();
}