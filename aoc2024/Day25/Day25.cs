namespace aoc2024.Day25;

public static class Day25
{
    enum Kind
    {
        Lock,
        Key
    };

    public static bool KeyFits(int[] Lock, int[] Key, int size = 5)
    {
        Console.WriteLine($"Testing {string.Join(",", Lock)} | {string.Join(",", Key)}");
        for (int i = 0; i < Lock.Length; i++)
        {
            if (Lock[i] + Key[i] > size)
                return false;
        }

        return true;
    }

    public static long Solve1(string input)
    {
        var schematics = input.Split("\n\n").Select(s =>
        {
            var rows = s.Split('\n');
            Kind kind = rows[0][0] == '#' ? Kind.Lock : Kind.Key;
            int[] cols = Enumerable.Range(0, 5).Select(col =>
                Enumerable.Range(0, rows.Length).Select(row => rows[row][col])
                    .Where(point => point == '#').ToList().Count - 1
            ).ToArray();
            return (kind, cols);
        }).ToList();


        var locks = schematics.Where(tuple => tuple.kind == Kind.Lock);
        var keys = schematics.Where(tuple => tuple.kind == Kind.Key);


        return locks.Sum(Lock =>
            keys.Count(Key => KeyFits(Lock.cols, Key.cols))
        );
    }
}