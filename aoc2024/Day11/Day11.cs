namespace aoc2024.Day11;

public static class Day11
{
    public static long Solve1(string input)
        =>
            input.Split(" ").Select(int.Parse)
                .Sum(stone => GetStonesGenerated(stone, 25, new Dictionary<(long, int), long>()));

    public static long Solve2(string input)
        => input.Split(" ").Select(int.Parse)
            .Sum(stone => GetStonesGenerated(stone, 75, new Dictionary<(long, int), long>()));

    static long HandleEvenStone(string stone, int depth, Dictionary<(long, int), long> memo)
        => GetStonesGenerated(long.Parse(stone[..(stone.Length / 2)]), depth, memo)
           + GetStonesGenerated(long.Parse(stone[(stone.Length / 2)..]), depth, memo);


    static long GetStonesGenerated(long stone, int depth, Dictionary<(long, int), long> memo)
    {
        if (depth == 0) return 1L;
        if (memo.TryGetValue((stone, depth), out var generated)) return generated;
        var nextDepth = depth - 1;
        var result = stone switch
        {
            0 => GetStonesGenerated(1L, nextDepth, memo),
            _ when stone.ToString().Length % 2 == 0 => HandleEvenStone(stone.ToString(), nextDepth, memo),
            _ => GetStonesGenerated(stone * 2024, nextDepth, memo),
        };
        memo[(stone, depth)] = result;
        return result;
    }
}