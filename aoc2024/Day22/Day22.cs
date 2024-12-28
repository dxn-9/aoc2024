using System.Collections.Concurrent;
using System.Numerics;

namespace aoc2024.Day22;

// Memoize the next 500th sequence.
using Memo = Dictionary<long, long>;

public static class Day22
{
    static long Mix(long secret, long value) => secret ^ value;
    static long Prune(long secret) => secret % 16777216;

    static long CalculateNextSecret(long secret)
    {
        secret = Prune(Mix(secret, secret * 64));
        secret = Mix(secret, (long)Math.Floor(secret / 32f));
        secret = Prune(secret);
        secret = Mix(secret, secret * 2048);
        secret = Prune(secret);
        return secret;
    }

    public static long Solve1(string input)
    {
        var memo = new Dictionary<long, Dictionary<int, long>>();

        var r = input.Split('\n').Select(long.Parse).Select(num =>
            {
                var current = new Dictionary<long, long>();
                for (int i = 0; i < 2000; i++)
                {
                    if (memo.TryGetValue(num, out var jumpDict))
                    {
                        var bestKey = -1;
                        foreach (var key in jumpDict.Keys)
                        {
                            if (key + i >= 2000) continue;
                            bestKey = Math.Max(key, bestKey);
                        }

                        bool shouldSkip = bestKey != -1;


                        if (shouldSkip)
                        {
                            num = jumpDict[bestKey];
                            i += bestKey - 1;
                            continue;
                        }
                    }

                    current[i] = CalculateNextSecret(num);
                    if (i >= 1980)
                    {
                        for (int j = 0; j < i - 1980; j++)
                        {
                            if (current.ContainsKey(j))
                            {
                                var dict = memo.GetValueOrDefault(current[j], new Dictionary<int, long>());
                                dict[i - j] = current[i];
                                memo[current[j]] = dict;
                            }
                        }
                    }

                    num = current[i];
                }

                return num;
            }
        ).Sum();
        return r;
    }
}