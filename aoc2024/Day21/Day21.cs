using System.Numerics;
using System.Text;

namespace aoc2024.Day21;

class Keypad
{
    Dictionary<char, Vector2> map;
    Vector2 armPosition;
    public string executed = "";
    public Keypad? nextKeypad;
    string initString;
    Dictionary<(Vector2, Vector2), long> cache = new();

    public Keypad(string init)
    {
        initString = init;
        map = init.Split('\n').SelectMany((line, y) =>
                line.ToCharArray().Select((ch, x) => new KeyValuePair<char, Vector2>(ch, new Vector2(x, y))))
            .ToDictionary();
        armPosition = map['A'];
    }

    public long ExecuteProgram(string program)
    {
        if (nextKeypad is null) return program.Length;
        long res = 0;
        foreach (var code in program)
        {
            executed += code;
            var to = map[code] - armPosition;
            var key = (to, armPosition);
            if (cache.TryGetValue(key, out var cachedValue))
            {
                res += cachedValue;
                armPosition = map[code];
                continue;
            }

            var possibleTo = PossibleInstructions(to);
            long bestInst = nextKeypad.Copy().ExecuteProgram(possibleTo[0] + "A");
            foreach (var possible in possibleTo.Skip(1))
            {
                var nextInstructions = nextKeypad.Copy().ExecuteProgram(possible + "A");
                bestInst = Math.Min(nextInstructions, bestInst);
            }

            nextKeypad.armPosition = nextKeypad.map['A'];
            res += bestInst;
            cache[key] = bestInst;
            armPosition = map[code];
        }

        return res;
    }

    public Keypad Copy()
    {
        var copy = new Keypad(initString)
        {
            armPosition = armPosition,
            executed = executed,
            nextKeypad = nextKeypad,
            cache = cache
        };
        return copy;
    }


    List<string> PossibleInstructions(Vector2 dir)
    {
        var initial = new List<char>();
        for (var x = 0; x < Math.Abs(dir.X); x++) initial.Add(dir.X > 0 ? '>' : '<');
        for (var y = 0; y < Math.Abs(dir.Y); y++) initial.Add(dir.Y > 0 ? 'v' : '^');
        var combinations = Permutations(initial);
        List<List<char>> legalCombinations = [];
        foreach (var combination in combinations)
        {
            var armPos = armPosition;
            var isLegal = true;
            foreach (var move in combination)
            {
                armPos += dirCharToVec(move);
                if (armPos == map['X']) isLegal = false;
            }

            if (isLegal) legalCombinations.Add(combination);
        }

        return legalCombinations.Select(list => string.Join("", list)).ToList();
    }

    static Vector2 dirCharToVec(char dir) => dir switch
    {
        '<' => new Vector2(-1f, 0f),
        '>' => new Vector2(1f, 0f),
        '^' => new Vector2(0f, -1f),
        'v' => new Vector2(0f, 1f),
        _ => throw new ArgumentException($"Invalid dir char {dir}")
    };

    static List<List<T>> Permutations<T>(List<T> list)
    {
        if (list.Count <= 1) return [list];
        List<List<T>> permutations = [];
        for (int i = 0; i < list.Count; i++)
        {
            var perm = Permutations(list[..i].Concat(list.Slice(i + 1, list.Count - i - 1)).ToList())
                .Select(p => new List<T> { list[i] }.Concat(p).ToList()).ToList();
            permutations.AddRange(perm);
        }

        return permutations;
    }
}

public static class Day21
{
    public static long Solve1(string input, int robotCount)
        => input.Split('\n').Sum(line =>
        {
            var firstRobot = new Keypad("789\n456\n123\nX0A");
            var currRobot = firstRobot;
            for (int i = 0; i < robotCount; i++)
            {
                var nextRobot = new Keypad("X^A\n<v>");
                currRobot.nextKeypad = nextRobot;
                currRobot = nextRobot;
            }

            var result = firstRobot.ExecuteProgram(line);
            return result * long.Parse(line.Split('A')[0]);
        });
}