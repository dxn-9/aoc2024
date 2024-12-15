using System.Text.RegularExpressions;

namespace aoc2024.Day14;

record Robot(Coord2I position, Coord2I velocity);

public static class Day14
{
    public static long CountQuadrant(List<List<int>> space)
    {
        space.RemoveAt(space.Count / 2);
        foreach (var line in space)
        {
            line.RemoveAt(line.Count / 2);
        }

        Coord2I[] quadrants = [(0, 0), (1, 0), (0, 1), (1, 1)];

        var r = 1;

        foreach (var (x, y) in quadrants)
        {
            int xMin = x * space[0].Count / 2;
            int xMax = (x + 1) * space[0].Count / 2;
            int yMin = y * space.Count / 2;
            int yMax = (y + 1) * space.Count / 2;
            var f = 0;


            for (int row = yMin; row < yMax; row++)
            {
                for (int col = xMin; col < xMax; col++)
                {
                    f += space[row][col];
                }
            }

            r *= f;
        }

        return r;
    }

    public static long Solve1(string input, int w, int h)
    {
        var time = 100;
        var robots = input.Split(Environment.NewLine).Select(line =>
            {
                var g = Regex.Match(line, @"p=(\d*),(\d*) v=([-]*\d*),([-]*\d*)").Groups;
                return new Robot((int.Parse(g[1].ToString()), int.Parse(g[2].ToString())),
                    (int.Parse(g[3].ToString()), int.Parse(g[4].ToString())));
            }
        ).ToList().Select(robot =>
            {
                var x = (robot.velocity.x * time + robot.position.x) % w;
                if (x < 0) x = w + x;
                var y = (robot.velocity.y * time + robot.position.y) % h;
                if (y < 0) y = h + y;
                return (x, y);
            }
        ).ToList();
        var space = Enumerable.Range(0, h).Select(_ =>
            Enumerable.Range(0, w).Select(_ =>
                0
            ).ToList()
        ).ToList();
        foreach (var robot in robots)
        {
            space[robot.y][robot.x] += 1;
        }

        return CountQuadrant(space);
    }

    public static long Solve2(string input, int w, int h)
    {
        var time = 10000;
        var robots = input.Split(Environment.NewLine).Select(line =>
            {
                var g = Regex.Match(line, @"p=(\d*),(\d*) v=([-]*\d*),([-]*\d*)").Groups;
                return new Robot((int.Parse(g[1].ToString()), int.Parse(g[2].ToString())),
                    (int.Parse(g[3].ToString()), int.Parse(g[4].ToString())));
            }
        );

        var close = int.MinValue;
        var bestTime = 0;
        for (int i = 1; i < time; i++)
        {
            var positions = robots.Select(robot =>
                {
                    var x = (robot.velocity.x * i + robot.position.x) % w;
                    if (x < 0) x = w + x;
                    var y = (robot.velocity.y * i + robot.position.y) % h;
                    if (y < 0) y = h + y;
                    return (x, y);
                }
            );

            var space = Enumerable.Range(0, h).Select(_ =>
                Enumerable.Range(0, w).Select(_ =>
                    0
                ).ToList()
            ).ToList();
            foreach (var position in positions)
            {
                space[position.y][position.x] += 1;
            }

            var totConsecutive = 0;
            foreach (var line in space)
            {
                var consecutive = 0;
                for (int j = 1; j < line.Count; j++)
                {
                    if (line[j - 1] > 0 && line[j] > 0) consecutive += 1;
                    else consecutive -= 1;
                }

                totConsecutive += consecutive;
            }

            if (totConsecutive > close)
            {
                close = totConsecutive;
                bestTime = i;
            }
        }


        return bestTime;
    }
}