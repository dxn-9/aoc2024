using System.Numerics;

namespace aoc2024.Day18;

public static class Day18
{
    static Vector2[] dirs = { new Vector2(1, 0), new Vector2(-1, 0), new Vector2(0, 1), new Vector2(0, -1) };

    public static int CountSteps(Dictionary<Vector2, Vector2?> prevMap, Vector2 s)
    {
        int count = 0;
        Vector2? c = s;
        while (c != null)
        {
            c = prevMap[(Vector2)c];
            count++;
        }

        return count - 1;
    }

    public static int FindShortestPathSteps(Dictionary<Vector2, char> map, Vector2 start, Vector2 goal)
    {
        var unvisitedSet = new HashSet<Vector2> { start };
        var dist = new Dictionary<Vector2, float> { [start] = 0 };
        var prev = new Dictionary<Vector2, Vector2?> { [start] = null };

        while (unvisitedSet.Count > 0)
        {
            var min = unvisitedSet.MinBy(v => dist.GetValueOrDefault(v, float.PositiveInfinity));
            if (min == goal) // TOD71O
                return CountSteps(prev, goal);

            unvisitedSet.Remove(min);
            dirs
                .Select(dir => dir + min)
                .Where(neigh => map.TryGetValue(neigh, out var v) && v == '.')
                .Where(neigh => dist[min] + 1 < dist.GetValueOrDefault(neigh, float.PositiveInfinity)).ToList()
                .ForEach(neigh =>
                {
                    dist[neigh] = dist[min] + 1;
                    prev[neigh] = min;
                    unvisitedSet.Add(neigh);
                });
        }


        return -1;
    }

    public static int Solve1(string input, int gridSize = 7, int bytesFallen = 12)
    {
        var map = Enumerable.Range(0, gridSize).SelectMany(x =>
                Enumerable.Range(0, gridSize).Select(y => KeyValuePair.Create<Vector2, char>(new Vector2(x, y), '.')))
            .ToDictionary();
        input.Split('\n').Select(coord =>
                new Vector2(float.Parse(coord.Split(',')[0]), float.Parse(coord.Split(',')[1])))
            .Take(bytesFallen).ToList().ForEach(v => map[v] = '#');

        return FindShortestPathSteps(map, Vector2.Zero, new Vector2(gridSize - 1, gridSize - 1));
    }

    public static string Solve2(string input, int gridSize = 7, int bytesFallen = 12)
    {
        var map = Enumerable.Range(0, gridSize).SelectMany(x =>
                Enumerable.Range(0, gridSize).Select(y => KeyValuePair.Create(new Vector2(x, y), '.')))
            .ToDictionary();
        input.Split('\n').Select(coord =>
                new Vector2(float.Parse(coord.Split(',')[0]), float.Parse(coord.Split(',')[1])))
            .Take(bytesFallen).ToList().ForEach(v => map[v] = '#');

        var v = input.Split('\n').Select(coord =>
                new Vector2(float.Parse(coord.Split(',')[0]), float.Parse(coord.Split(',')[1])))
            .Skip(bytesFallen).ToList().First(v =>
            {
                map[v] = '#';
                return FindShortestPathSteps(map, Vector2.Zero, new Vector2(gridSize - 1, gridSize - 1)) == -1;
            });
        return $"{v.X},{v.Y}";
    }
}