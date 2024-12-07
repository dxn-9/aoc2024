using System.Text.RegularExpressions;

namespace aoc2024.Day6;

public static class Day6
{
    public static int Solve1(string input)
    {
        var lines = input.Split(Environment.NewLine);
        var obstacles = lines.SelectMany((line, y) =>
            Regex.Matches(line, "#").Select(match => match.Index).Select(x => (x, y))).ToHashSet();
        var p = lines.Select((line, y) => (x: line.IndexOf('^'), y))
            .First(c => c.x != -1);
        var dir = 0;
        var yMax = lines.Length;
        var xMax = lines[0].Length;
        var visited = new HashSet<(int, int)>();
        while (p.x >= 0 && p.x < xMax && p.y >= 0 && p.y < yMax)
        {
            visited.Add(p);
            var direction = GetForwardDir(dir);
            var fP = (p.x + direction.x, p.y + direction.y);
            if (obstacles.Contains(fP))
            {
                dir.NextDir();
            }
            else
            {
                p = fP;
            }
        }

        return visited.Count;
    }

    static (int x, int y) GetForwardDir(int i) =>
        i switch
        {
            0 => (x: 0, y: -1),
            1 => (x: 1, y: 0),
            2 => (x: 0, y: 1),
            3 => (x: -1, y: 0),
            _ => throw new ArgumentException()
        };

    static void NextDir(this ref int dir)
    {
        dir = (dir + 1) % 4;
    }


    // This could be a lot better.
    public static int Solve2(string input)
    {
        var lines = input.Split(Environment.NewLine);
        var coords = lines.Select((line, y) => (x: line.IndexOf('^'), y))
            .First(c => c.x != -1);
        var startingPosition = (coords.x, coords.y, dir: 0);
        var yMax = lines.Length;
        var xMax = lines[0].Length;
        var loopCount = 0;

        for (int x = 0; x < xMax; x++)
        {
            for (int y = 0; y < yMax; y++)
            {
                if (x == startingPosition.x && y == startingPosition.y) continue;
                var position = startingPosition;
                var obstacles = lines.SelectMany((line, y) =>
                    Regex.Matches(line, "#").Select(match => match.Index).Select(x => (x, y))).ToHashSet();
                obstacles.Add((x, y));
                var visited = new HashSet<(int x, int y, int dir)>();
                while (position.x >= 0 && position.x < xMax && position.y >= 0 && position.y < yMax)
                {
                    if (!visited.Add(position))
                    {
                        loopCount++;
                        break;
                    }

                    var direction = GetForwardDir(position.dir);
                    var nextPosition = (x: position.x + direction.x, y: position.y + direction.y, position.dir);
                    if (obstacles.Contains((nextPosition.x, nextPosition.y)))
                    {
                        position.dir.NextDir();
                    }
                    else
                    {
                        position = nextPosition;
                    }
                }
            }
        }

        return loopCount;
    }
}