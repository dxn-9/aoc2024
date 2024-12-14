namespace aoc2024.Day12;

using Coord = (int x, int y);

public static class Day12
{
    static (int, int)[] directions =
    [
        (-1, 0), (1, 0), (0, 1), (0, -1)
    ];

    static (int, int)[][] corners =
    {
        [(1, 0), (1, -1), (0, -1)], // Top right corner
        [(-1, 0), (-1, -1), (0, -1)], // Top left corner
        [(1, 0), (1, 1), (0, 1)], // Bottom right corner
        [(-1, 0), (-1, 1), (0, 1)], // Bottom left corner
    };

    static void PopulateSet((int, int) position, HashSet<(int, int)> from, HashSet<(int, int)> to)
    {
        if (!from.Contains(position)) return;
        if (!to.Add(position)) return;
        from.Remove(position);
        foreach (var direction in directions)
        {
            PopulateSet((position.Item1 + direction.Item1, position.Item2 + direction.Item2), from, to);
        }
    }

    public static long Solve1(string input)
    {
        var map = input.Split(Environment.NewLine).SelectMany((line, y) =>
                line.Select((region, x) => KeyValuePair.Create(region, (x, y)))
            ).GroupBy(kv => kv.Key)
            .ToDictionary(pair => pair.Key,
                elementSelector: group => group.Select(kv => kv.Value).ToHashSet());
        var groups = map.SelectMany(kv =>
        {
            var orig = kv.Value;
            var list = new List<HashSet<(int, int)>>();
            while (orig.Count > 0)
            {
                var target = new HashSet<(int, int)>();
                PopulateSet(orig.First(), orig, target);
                list.Add(target);
            }

            return list;
        });
        var values = groups.Select(group => (area: group.Count, perim: group.Sum(plot =>
            4 - directions.Sum(dir => group.Contains((plot.Item1 + dir.Item1, plot.Item2 + dir.Item2)) ? 1 : 0
            ))));
        return values.Sum(pair => pair.area * pair.perim);
    }

    public static Coord TopRight(this Coord coord) => (coord.x + 1, coord.y - 1);
    public static Coord TopLeft(this Coord coord) => (coord.x - 1, coord.y - 1);
    public static Coord BottomRight(this Coord coord) => (coord.x + 1, coord.y + 1);
    public static Coord BottomLeft(this Coord coord) => (coord.x - 1, coord.y + 1);
    public static Coord Left(this Coord coord) => (coord.x - 1, coord.y);
    public static Coord Right(this Coord coord) => (coord.x + 1, coord.y);
    public static Coord Top(this Coord coord) => (coord.x, coord.y - 1);
    public static Coord Bottom(this Coord coord) => (coord.x, coord.y + 1);

    public static int Corner(Coord position, HashSet<(int, int)> group)
    {
        var r = 0;
        // Check for convex and concave corners.

        if (!group.Contains(position.Top()) && !group.Contains(position.Right()) ||
            (!group.Contains(position.TopRight()) && group.Contains(position.Top()) &&
             group.Contains(position.Right())))
        {
            r++;
        }

        if (!group.Contains(position.Top()) && !group.Contains(position.Left()) ||
            (!group.Contains(position.TopLeft()) && group.Contains(position.Top()) &&
             group.Contains(position.Left())))
        {
            r++;
        }

        if (!group.Contains(position.Bottom()) && !group.Contains(position.Left()) ||
            (!group.Contains(position.BottomLeft()) && group.Contains(position.Bottom()) &&
             group.Contains(position.Left())))
        {
            r++;
        }

        if (
            (!group.Contains(position.Bottom()) && !group.Contains(position.Right())) ||
            (!group.Contains(position.BottomRight()) && group.Contains(position.Bottom()) &&
             group.Contains(position.Right()))
        )
        {
            r++;
        }

        return r;
    }

    public static long Solve2(string input)
    {
        var map = input.Split(Environment.NewLine).SelectMany((line, y) =>
                line.Select((region, x) => KeyValuePair.Create(region, (x, y)))
            ).GroupBy(kv => kv.Key)
            .ToDictionary(pair => pair.Key,
                elementSelector: group => group.Select(kv => kv.Value).ToHashSet());
        var groups = map.SelectMany(kv =>
        {
            var orig = kv.Value;
            var list = new List<HashSet<(int, int)>>();
            while (orig.Count > 0)
            {
                var target = new HashSet<(int, int)>();
                PopulateSet(orig.First(), orig, target);
                list.Add(target);
            }

            return list;
        });

        // Idea: counting corners.
        var values = groups.Select(group => (area: group.Count, perim: group.Sum(plot =>
            Corner((plot.Item1, plot.Item2), group)
        ))).ToList();
        return values.Sum(pair => pair.area * pair.perim);
    }
}