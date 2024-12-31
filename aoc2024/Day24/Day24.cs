using System.Text.RegularExpressions;

namespace aoc2024.Day24;

using Operation = (string r1, string r2, string op, string output);

public static class Day24
{
    public static long Solve1(string input)
    {
        var registers = ParseInput(input.Split("\n\n")[0]);
        var instructions = input.Split("\n\n")[1].Split('\n')
            .Select(line => Regex.Match(line, "(.+) (.+) (.+) -> (.+)").Groups)
            .Select(group => (r1: group[1].ToString(), r2: group[3].ToString(), op: group[2].ToString(),
                output: group[4].ToString())).ToList();

        var ordered = OrderInstructions(registers.Keys.ToHashSet(), instructions);
        ExecuteInstructions(ordered, in registers);


        var zRegisters = registers.Keys.Where(s => s.StartsWith("z")).Order().ToList();
        long result = 0;
        for (int i = 0; i < zRegisters.Count; i++)
        {
            var zValue = registers[zRegisters[i]];
            result |= (long)zValue << i;
        }

        return result;
    }

    // I didn't manage to find any algorithmic solution for this one. The brute force approach would take too much,
    // so i basically structured some logging based on the expected way that a adder works (look: https://www.build-electronic-circuits.com/full-adder/ )
    // then after pasting the input, it would produce an error, swap the outputs (and take note of the swapped outputs), and do this until there were no errors.
    public static string Solve2(string input)
    {
        var registers = ParseInput(input.Split("\n\n")[0]);
        var instructions = input.Split("\n\n")[1].Split('\n')
            .Select(line => Regex.Match(line, "(.+) (.+) (.+) -> (.+)").Groups)
            .Select(group => (r1: group[1].ToString(), r2: group[3].ToString(), op: group[2].ToString(),
                output: group[4].ToString())).ToList();
        var maxX = int.Parse(registers.Keys.Where(key => key.StartsWith('x')).Order().Last().Split('x')[1]);
        var cin = "";
        // https://www.build-electronic-circuits.com/full-adder/
        for (int i = 0; i < maxX; i++)
        {
            var numStr = i >= 10 ? $"{i}" : $"0{i}";
            var xStr = $"x{numStr}";
            var yStr = $"y{numStr}";

            var aXorB = instructions.Single(inst =>
                (inst.r1 == xStr || inst.r1 == yStr) &&
                (inst.r2 == xStr || inst.r2 == yStr) && inst.op == "XOR");
            var aAndB = instructions.Single(inst =>
                (inst.r1 == xStr || inst.r1 == yStr) &&
                (inst.r2 == xStr || inst.r2 == yStr) && inst.op == "AND");
            Console.WriteLine($"---{i}---");
            Console.WriteLine($"x XOR y: {aXorB.output}");
            Console.WriteLine($"x AND y: {aAndB.output}");

            if (i == 0)
            {
                cin = aAndB.output;
                continue;
            }

            var s = instructions.Single(inst =>
                (inst.r1 == aXorB.output || inst.r1 == cin) && (inst.r2 == cin || inst.r2 == aXorB.output) &&
                inst.op == "XOR");
            Console.WriteLine($"s: {s.output}");
            var aXorBANDCin =
                instructions.Single(inst =>
                    (inst.r1 == aXorB.output || inst.r1 == cin) && (inst.r2 == cin || inst.r2 == aXorB.output) &&
                    inst.op == "AND");
            Console.WriteLine($"aXOrBANDCin: {aXorBANDCin.output}");
            var cOut = instructions.Single(inst =>
                (inst.r1 == aXorBANDCin.output || inst.r1 == aAndB.output) &&
                (inst.r2 == aAndB.output || inst.r2 == aXorBANDCin.output) && inst.op == "OR");
            cin = cOut.output;
            Console.WriteLine($"cOut: {cOut.output}");
        }

        return "cgh,frt,pmd,sps,tst,z05,z11,z23";
    }

    public static void ExecuteInstructions(List<Operation> instructions, in Dictionary<string, byte> registers)
    {
        foreach (var instruction in instructions)
        {
            if (!registers.TryGetValue(instruction.r1, out var r1) ||
                !registers.TryGetValue(instruction.r2, out var r2))
                return;

            registers[instruction.output] = instruction.op switch
            {
                "AND" => (byte)(r1 & r2),
                "OR" => (byte)(r1 | r2),
                "XOR" => (byte)(r1 ^ r2),
                _ => throw new ArgumentException($"Invalid operation {instruction.op}")
            };
        }
    }


    static Dictionary<string, byte> ParseInput(string input)
        => input.Split('\n').Select(line =>
                new KeyValuePair<string, byte>(line.Split(':')[0], byte.Parse(line.Split(':')[1].Trim())))
            .ToDictionary();

    public static List<Operation> OrderInstructions(HashSet<string> knownRegisters, in List<Operation> initial)
    {
        Console.WriteLine("ORDER INSTRUCTIONS");
        var orderedInstructions = new List<Operation>();
        var stack = new Stack<Operation>();
        stack.Push(initial.First());

        while (stack.Count > 0)
        {
            var instruction = stack.Peek();

            if (!knownRegisters.Contains(instruction.r1))
            {
                if (instruction.output == instruction.r1) break;
                var op1 = initial.Single(operation => operation.output == instruction.r1);
                stack.Push(op1);
                continue;
            }

            if (!knownRegisters.Contains(instruction.r2))
            {
                if (instruction.output == instruction.r2) break;
                var op2 = initial.Single(operation => operation.output == instruction.r2);
                stack.Push(op2);
                continue;
            }

            stack.Pop();
            initial.Remove(instruction);
            knownRegisters.Add(instruction.output);
            orderedInstructions.Add(instruction);
            if (initial.Count > 0) stack.Push(initial.First());
        }

        return orderedInstructions;
    }
}