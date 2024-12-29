namespace aoc2024.Day23;

public static class Day23
{
    static Dictionary<string, HashSet<string>> GetConnections(string input) => input.Split('\n')
        .Select(connection => (connection.Split('-')[0], connection.Split('-')[1]))
        .Aggregate(new Dictionary<string, HashSet<string>>(),
            (dict, tuple) =>
            {
                dict[tuple.Item1] = [..dict.GetValueOrDefault(tuple.Item1, []), tuple.Item2];
                dict[tuple.Item2] = [..dict.GetValueOrDefault(tuple.Item2, []), tuple.Item1];
                return dict;
            });

    public static long Solve1(string input)
    {
        var dictionary = GetConnections(input);

        var connectionsThree = dictionary.Aggregate(new HashSet<string>(), (set, kv) =>
        {
            kv.Value.ToList().ForEach(val =>
                kv.Value.Where(other => other != val).ToList().ForEach(other =>
                {
                    var str = string.Join(",", new[] { kv.Key, val, other }.Order());
                    if (dictionary[val].Contains(other))
                        set.Add(str);
                })
            );
            return set;
        });
        return connectionsThree.Count(conn => conn.Split(',').Any(c => c[0] == 't'));
    }

    public static HashSet<string> BiggestCommonConnection(Dictionary<string, HashSet<string>> dict, HashSet<string> seq,
        Dictionary<string, HashSet<string>> memo)
    {
        if (seq.Count < 2) return seq;
        if (memo.TryGetValue(ConnectionString(seq), out var val)) return val;


        var biggest = new HashSet<string>();
        foreach (var str in seq)
        {
            var others = seq.Where(s => s != str);
            var common = others.Where(other => dict[str].Contains(other)).Order().ToHashSet();
            var commonConnection = BiggestCommonConnection(dict, common, memo);
            memo[ConnectionString(common)] = common;
            common.IntersectWith(commonConnection);
            HashSet<string> actual = [..common, str];
            if (actual.Count > biggest.Count) biggest = actual;
        }

        return biggest;
    }

    public static string ConnectionString(HashSet<string> set) => string.Join(",", set.Order());

    public static string Solve2(string input)
    {
        var dictionary = GetConnections(input);
        var memo = new Dictionary<string, HashSet<string>>();
        return dictionary.Aggregate("", (str, kv) =>
        {
            HashSet<string> biggestConnection = [..BiggestCommonConnection(dictionary, kv.Value, memo), kv.Key];
            var conn = string.Join(",", biggestConnection.Order());
            return conn.Length > str.Length ? conn : str;
        });
    }
}