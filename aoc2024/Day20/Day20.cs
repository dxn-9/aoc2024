using System.Numerics;

namespace aoc2024.Day20;

public static class Day20
{
    static Vector2[] dirs = { new Vector2(1, 0), new Vector2(-1, 0), new Vector2(0, 1), new Vector2(0, -1) };

    // Idea: calculate first the normal path. Then go through the generated path and 
    // look for possible cheats and each time save.
    // (we get the time saved by comparing the time it took to reach that node in the first place)
    public static int CalcCheats(Dictionary<Vector2, char> map, int minSave, int cheatTime)
    {
        Vector2 playerPosition = map.Single(kv => kv.Value == 'S').Key,
            targetPosition = map.Single(kv => kv.Value == 'E').Key;

        var nodes = new HashSet<Vector2> { playerPosition };
        // Map : Node to pico seconds
        var visited = new Dictionary<Vector2, int> { [playerPosition] = 0 };
        var path = new Dictionary<Vector2, Vector2?> { [playerPosition] = null };

        while (nodes.Count > 0)
        {
            var node = nodes.MinBy(v => visited.GetValueOrDefault(v, int.MaxValue));
            if (node == targetPosition)
                break;

            nodes.Remove(node);
            dirs.Select(v => node + v)
                .Where(k => map.TryGetValue(k, out var c) && (c == '.' || c == 'E'))
                .Where(neighbour => visited[node] + 1 < visited.GetValueOrDefault(neighbour, int.MaxValue)).ToList()
                .ForEach(neighbour =>
                {
                    visited[neighbour] = visited[node] + 1;
                    path[neighbour] = node;
                    nodes.Add(neighbour);
                });
        }


        Vector2? current = path[targetPosition];
        var possibleCheats = 0;
        while (current != null)
        {
            var _current = (Vector2)current;
            var _currentSeconds = visited[_current];
            var initial = new Dictionary<Vector2, int> { [_current] = _currentSeconds };
            for (int i = 0; i < cheatTime; i++)
            {
                initial.Keys.SelectMany(k => dirs.Select(d => (from: k, to: d + k))).ToList().ForEach(v =>
                {
                    if (!initial.ContainsKey(v.to)) initial.Add(v.to, initial[v.from] + 1);
                });
            }

            possibleCheats +=
                initial.Where(cheatedPosition =>
                        map.TryGetValue(cheatedPosition.Key, out var c) && (c == '.' || c == 'E'))
                    .Count(kv =>
                        visited.TryGetValue(kv.Key, out var pSeconds) && kv.Value < pSeconds &&
                        pSeconds - kv.Value >= minSave
                    );


            current = path[(Vector2)current];
        }

        return possibleCheats;
    }

    public static int Solve1(string input, int minSave)
    {
        var map = input.Split('\n')
            .SelectMany((l, y) => l.ToCharArray().Select((p, x) => KeyValuePair.Create(new Vector2(x, y), p)))
            .ToDictionary();
        return CalcCheats(map, minSave, 2);
    }

    public static int Solve2(string input, int minSave)
    {
        var map = input.Split('\n')
            .SelectMany((l, y) => l.ToCharArray().Select((p, x) => KeyValuePair.Create(new Vector2(x, y), p)))
            .ToDictionary();
        return CalcCheats(map, minSave, 20);
    }
}