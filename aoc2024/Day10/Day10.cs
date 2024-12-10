namespace aoc2024.Day10;

public static class Day10
{
    public static long Solve1(string input)
    {
        var map = input.Split(Environment.NewLine).Select(line => line.ToCharArray().Select(c => int.Parse(c.ToString(
            ))).ToList())
            .ToList();
        var startingPositions = map.Index().Aggregate(new List<(int, int)>(), (positions, row) =>
        {
            var data = row.Item;
            for (int i = 0; i < data.Count; i++)
            {
                if (data[i] == 0) positions.Add((i, row.Index));
            }

            return positions;
        });

        return startingPositions.Sum(position => traverse(position, map, new HashSet<(int, int)>()));
    }

    public static long Solve2(string input)
    {
        var map = input.Split(Environment.NewLine).Select(line => line.ToCharArray().Select(c => int.Parse(c.ToString(
            ))).ToList())
            .ToList();
        var startingPositions = map.Index().Aggregate(new List<(int, int)>(), (positions, row) =>
        {
            var data = row.Item;
            for (int i = 0; i < data.Count; i++)
            {
                if (data[i] == 0) positions.Add((i, row.Index));
            }

            return positions;
        });

        return startingPositions.Sum(position => traverse(position, map, new HashSet<(int, int)>(), true));
    }

    public static int traverse((int, int) startingPosition, List<List<int>> map, HashSet<(int, int)> visited,
        bool countPaths = false)
    {
        var current = map[startingPosition.Item2][startingPosition.Item1];
        if (current == 9 && visited.Add(startingPosition))
            return 1;
        if (current == 9) return countPaths ? 1 : 0;
        if (!visited.Add(startingPosition))
        {
            return 0;
        }

        var moves = new[] { (-1, 0), (1, 0), (0, -1), (0, 1) };
        var score = 0;

        foreach (var move in moves)
        {
            int y = startingPosition.Item2 + move.Item2, x = startingPosition.Item1 + move.Item1;
            if (y >= 0 && y < map.Count && x >= 0 && x < map[0].Count)
            {
                var neighbourValue = map[y][x];
                if (neighbourValue - current == 1)
                    score += traverse((x, y), map,
                        countPaths ? [..visited.ToList().AsEnumerable()] : visited, countPaths);
            }
        }

        return score;
    }
}