using System.Numerics;

namespace aoc2024.Day16;

using Node = (Vector2 pos, Vector2 dir);

public static class Day16
{
    public static Vector2[] dirs = [new(-1f, 0f), new(1f, 0), new(0, 1f), new(0, -1f)];

    public static List<Node> CalculatePath(Dictionary<Node, Node?> path, Node? start)
    {
        var list = new List<Node>();
        while (start is not null)
        {
            list.Add((Node)start);
            start = path[(Node)start];
        }

        return list;
    }

    public static long CalculatePathCost(List<Node> path)
    {
        var cost = 0;
        var prev = path.First();
        foreach (var node in path.Skip(1))
        {
            if (node.pos != prev.pos)
            {
                cost += 1;
            }
            else
            {
                cost += 1000;
            }

            prev = node;
        }

        return cost;
    }

    public static Node rotate90(Vector2 dir) =>
        (new Vector2((int)Math.Cos(dir.X * Math.PI / 2), (int)Math.Cos(dir.Y * Math.PI / 2)),
            new Vector2((int)-Math.Cos(dir.X * Math.PI / 2), (int)-Math.Cos(dir.Y * Math.PI / 2))
        );

    public static List<Node> CreateNeighbours(Node node)
    {
        return
        [
            (node.pos + node.dir, node.dir), (node.pos, rotate90(node.dir).pos), (node.pos, rotate90(node.dir).dir),
        ];
    }

    public static List<Node> FindCheapestPath(Dictionary<Vector2, char> map, Node startingNode,
        Vector2 targetPosition)
    {
        var nodes = new HashSet<Node>();
        foreach (var key in map.Keys.Where(key => map[key] != '#'))
            dirs.ToList().ForEach(dir => nodes.Add((key, dir)));
        var distances = new Dictionary<Node, long>
            { [startingNode] = 0 };
        var path = new Dictionary<Node, Node?> { [startingNode] = null };


        while (nodes.Count > 0)
        {
            var node = nodes.MinBy(v => distances.GetValueOrDefault(v, long.MaxValue));
            nodes.Remove(node);
            var nodeDistance = distances[node];
            if (node.pos == targetPosition)
            {
                return CalculatePath(path, node);
            }

            var neighbours = CreateNeighbours(node).Where(neigh => nodes.Contains(neigh));
            foreach (var neighbour in neighbours)
            {
                var distance = nodeDistance + (neighbour.pos != node.pos ? 1 : 1000);
                if (distance < distances.GetValueOrDefault(neighbour, long.MaxValue))
                {
                    distances[neighbour] = distance;
                    path[neighbour] = node;
                }
            }
        }

        return [];
    }

    public static List<List<Node>> CalculateVariations(List<Node> path, Dictionary<Vector2, char> map)
    {
        path.Reverse();
        var allPaths = new List<List<Node>> { path };
        var cost = CalculatePathCost(path);
        Node endNode = path.Last();
        var newPaths = path.Zip(path.Skip(1)).AsParallel().SelectMany(tuple =>
        {
            var currNode = tuple.First;
            var nextNode = tuple.Second;
            var variations = CreateNeighbours(currNode).Where(neigh => neigh != nextNode)
                .Where(neigh => map[neigh.pos] != '#').ToList();
            var newPaths = variations.Select(variation =>
                FindCheapestPath(map, variation, endNode.pos)).ToList();
            return newPaths.Where((newPath, index) =>
            {
                newPath.Reverse();
                var until = path.Slice(0, path.IndexOf(nextNode));
                var np = until.Concat(newPath).ToList();
                return CalculatePathCost(np) == cost;
            }).ToList();
        }).ToList();
        allPaths.AddRange([..newPaths]);

        return allPaths;
    }

    public static long Solve1(string input)
    {
        var map = input.Split('\n')
            .SelectMany((line, y) =>
                line.ToCharArray().Select((ch, x) => new KeyValuePair<Vector2, char>(new Vector2(x, y), ch)))
            .ToDictionary();
        return CalculatePathCost(FindCheapestPath(map, (map.Single(kv => kv.Value == 'S').Key, new Vector2(-1f, 0f)),
            map.Single(kv => kv.Value == 'E').Key));
    }

    // This is by far the least optimal solution..
    public static long Solve2(string input)
    {
        var map = input.Split('\n')
            .SelectMany((line, y) =>
                line.ToCharArray().Select((ch, x) => new KeyValuePair<Vector2, char>(new Vector2(x, y), ch)))
            .ToDictionary();
        var cheapestPath = FindCheapestPath(map, (map.Single(kv => kv.Value == 'S').Key, new Vector2(-1f, 0f)),
            map.Single(kv => kv.Value == 'E').Key);
        var allPaths = CalculateVariations(cheapestPath, map);
        var allPositions = new HashSet<Vector2>();
        foreach (var path in allPaths)
        {
            foreach (var node in path) allPositions.Add(node.pos);
        }

        return allPositions.Count;
    }
}