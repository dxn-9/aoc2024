namespace aoc2024.Day15;

public static class Day15
{
    /**
     * Approach: simulate the problem, on every move we take the previous state, and produce a new one.
     * the last state produced is the one used for counting the score
     */
    public static long Solve1(string input)
    {
        var initial = input.Split($"{Environment.NewLine}{Environment.NewLine}")[0];
        var map = initial.Split(Environment.NewLine).SelectMany((line, y) =>
            line.Select((c, x) => KeyValuePair.Create((x, y), c))).ToDictionary();
        input.Split($"{Environment.NewLine}{Environment.NewLine}")[1].Replace(Environment.NewLine, "")
            .ToCharArray().ToList().ForEach(move =>
                {
                    var newState = new Dictionary<Coord2I, char>(map);
                    var player = newState.First(kv => kv.Value == '@').Key;
                    Coord2I dir = move switch
                    {
                        '<' => (-1, 0),
                        '^' => (0, -1),
                        '>' => (1, 0),
                        'v' => (0, 1),
                        _ => throw new ArgumentException()
                    };
                    var i = 1;
                    while (newState[Move(player, dir, i)] == 'O') i += 1;
                    if (newState[Move(player, dir, i)] == '#') return;
                    for (; i > 0; i--)
                    {
                        newState[Move(player, dir, i)] = newState[Move(player, dir, i - 1)];
                    }

                    newState[Move(player, dir, 0)] = '.';

                    map = newState;
                }
            );

        return map.Where(kv => kv.Value == 'O').Sum(kv => kv.Key.y * 100 + kv.Key.x);
    }

    // Approach: for moving the boxes, collect all the pairs and "forward" points, and check if they're available.
    public static long Solve2(string input)
    {
        var initial = input.Split($"{Environment.NewLine}{Environment.NewLine}")[0].Replace("#", "##")
            .Replace("O", "[]").Replace(".", "..").Replace("@", "@.");

        var map = initial.Split(Environment.NewLine).SelectMany((line, y) =>
            line.Select((c, x) => KeyValuePair.Create((x, y), c))).ToDictionary();

        input.Split($"{Environment.NewLine}{Environment.NewLine}")[1].Replace(Environment.NewLine, "")
            .ToCharArray().ToList().ForEach(move =>
                {
                    var newState = new Dictionary<Coord2I, char>(map);
                    var player = newState.First(kv => kv.Value == '@').Key;
                    Coord2I dir = move switch
                    {
                        '<' => (-1, 0),
                        '^' => (0, -1),
                        '>' => (1, 0),
                        'v' => (0, 1),
                        _ => throw new ArgumentException()
                    };

                    var i = 1;
                    HashSet<Coord2I> points = [Move(player, dir, i)];
                    var shouldStop = points.Any(coord => newState[coord] == '#' || newState[coord] == '.');
                    while (!shouldStop)
                    {
                        i++;
                        if (dir.y != 0)
                        {
                            var insertedIndices = new List<int>();
                            var len = points.Count;
                            // could take i*2 last elements
                            for (var j = 0; j < len; j++)
                            {
                                var point = points.ElementAt(j);
                                if (newState[point] == '.') continue;
                                var pair = newState[point] == ']' ? Move(point, (-1, 0), 1) : Move(point, (1, 0), 1);
                                points.Add(pair);
                                var n1 = Move(point, dir, 1);
                                var n2 = Move(pair, dir, 1);
                                if (points.Add(n1)) insertedIndices.Add(points.Count - 1);
                                if (points.Add(n2)) insertedIndices.Add(points.Count - 1);
                            }

                            shouldStop = points.Any(coord => newState[coord] == '#');
                            if (!shouldStop)
                                shouldStop = points.Where((_, index) => insertedIndices.Contains(index))
                                    .All(coord => newState[coord] == '.');
                        }
                        else
                        {
                            points.Add(Move(player, dir, i));
                            shouldStop = points.Any(coord => newState[coord] == '#' || newState[coord] == '.');
                        }
                    }

                    points.Add(player);
                    if (points.Any(coord => newState[coord] == '#')) return;

                    var groups = points.GroupBy(coord => dir.x != 0 ? coord.x : coord.y);
                    groups = (dir.x + dir.y > 0 ? groups.OrderByDescending(k => k.Key) : groups.OrderBy(k => k.Key))
                        .ToList();
                    foreach (var group in groups)
                    {
                        var previousGroup =
                            groups.FirstOrDefault(g => g.Key == group.Key - dir.y - dir.x, null);
                        foreach (var point in group)
                        {
                            if (previousGroup != null && previousGroup.Contains((point.x - dir.x, point.y - dir.y)))
                            {
                                newState[point] = newState[(point.x - dir.x, point.y - dir.y)];
                            }
                            else
                            {
                                newState[point] = '.';
                            }
                        }
                    }


                    map = newState;
                }
            );

        return map.Where(kv => kv.Value == '[').Sum(kv => kv.Key.y * 100 + kv.Key.x);
    }


    static Coord2I Move(Coord2I orig, Coord2I direction, int scalar) =>
        (orig.x + direction.x * scalar, orig.y + direction.y * scalar);
}