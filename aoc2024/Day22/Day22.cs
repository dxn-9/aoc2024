namespace aoc2024.Day22;

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

    const int SecretsGenerated = 2000;
    static string SequenceKey(long[] sequence) => string.Join(",", sequence);

    public static long Solve1(string input)
        => input.Split('\n').Select(long.Parse).AsParallel().Select(num =>
            {
                for (int i = 0; i < SecretsGenerated; i++)
                {
                    num = CalculateNextSecret(num);
                }

                return num;
            }
        ).Sum();

    public static long GetPrice(long secret) =>
        secret % 10;

    public static long Solve2(string input)
    {
        var tuple = input.Split('\n').Select(long.Parse).AsParallel().Select(num =>
        {
            var sequences = new List<long[]>();
            var secrets = new List<long> { num };
            var sequence = new long[4];
            for (int i = 0; i < SecretsGenerated; i++)
            {
                num = CalculateNextSecret(num);
                secrets.Add(num);
            }

            for (int i = 0; i < 4; i++)
            {
                sequence[i] = GetPrice(secrets[i + 1]) - GetPrice(secrets[i]);
            }

            var payForSequence = new Dictionary<string, long> { [SequenceKey(sequence)] = GetPrice(secrets[4]) };
            sequences.Add([..sequence]);
            for (int i = 4; i < secrets.Count - 1; i++)
            {
                sequence = [..sequence[1..4], GetPrice(secrets[i + 1]) - GetPrice(secrets[i])];
                if (!payForSequence.ContainsKey(SequenceKey(sequence)))
                    payForSequence[SequenceKey(sequence)] = GetPrice(secrets[i + 1]);
                sequences.Add([..sequence]);
            }

            return (sequences, payForSequence);
        }).ToList();
        var allSequences = tuple.SelectMany(t => t.sequences.Select(SequenceKey)).ToHashSet();
        return allSequences.AsParallel().Max(seq => tuple.Sum(l => l.payForSequence.GetValueOrDefault(seq, 0)));
    }
}