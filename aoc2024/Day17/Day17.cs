namespace aoc2024.Day17;

public static class Day17
{
    class Program
    {
        long A, B, C;
        int pc;
        public List<int> stdout = [];
        List<int> stdin = [];

        public Program(long A, long B, long C, List<int> stdin)
        {
            this.A = A;
            this.B = B;
            this.C = C;
            this.stdin = stdin;
        }

        enum OpCodes : byte
        {
            adv,
            bxl,
            bst,
            jnz,
            bxc,
            _out,
            bdv,
            cdv
        }

        long comboOperand(byte operand)
        {
            return operand switch
            {
                <= 3 => operand,
                4 => A,
                5 => B,
                6 => C,
                _ => throw new ArgumentException("Invalid operand")
            };
        }

        int performInstruction(OpCodes opcode, byte operand)
        {
            var nextInstruction = pc + 2;
            switch (opcode)
            {
                case OpCodes.adv:
                    A = (long)(A / Math.Pow(2, comboOperand(operand))); break;
                case OpCodes.bxl: B ^= operand; break;
                case OpCodes.bst: B = comboOperand(operand) & 0b111; break;
                case OpCodes.jnz:
                    if (A != 0) nextInstruction = operand;
                    break;
                case OpCodes.bxc:
                    B ^= C; break;
                case OpCodes._out:
                    stdout.Add((int)comboOperand(operand) & 0b111); break;
                case OpCodes.bdv:
                    B = (long)(A / Math.Pow(2, comboOperand(operand))); break;
                case OpCodes.cdv:
                    C = (long)(A / Math.Pow(2, comboOperand(operand))); break;
                default:
                    throw new ArgumentException("Invalid opcode");
            }

            return nextInstruction;
        }

        (byte, byte, bool) parseInput()
        {
            if (pc + 1 >= stdin.Count) return (0, 0, true);
            return ((byte)stdin[pc], (byte)stdin[pc + 1], false);
        }

        public void Eval()
        {
            var (instruction, operand, halt) = parseInput();
            if (halt) return;
            pc = performInstruction((OpCodes)instruction, operand);
            Eval();
        }
    }

    public static string Solve1(string input)
    {
        var registers = input.Split("\n\n")[0].Split('\n').Select(line => int.Parse(line.Split(":")[1].Trim()))
            .ToList();
        var stdin = input.Split("\n\n")[1].Split(":")[1].Trim().Split(",").Select(int.Parse).ToList();

        var program = new Program(registers[0], registers[1], registers[2], stdin);
        program.Eval();

        return string.Join(",", program.stdout);
    }


    static IEnumerable<long> GenerateA(List<int> program, List<int> output)
    {
        if (!output.Any())
        {
            yield return 0;
            yield break;
        }

        foreach (var ah in GenerateA(program, output[1..]))
        {
            for (var al = 0; al < 8; al++)
            {
                var a = ah * 8 + al;
                var p = new Program(a, 0, 0, program);
                p.Eval();
                if (p.stdout.SequenceEqual(output))
                {
                    yield return a;
                }
            }
        }
    }

    public static long Solve2(string input)
    {
        var stdin = input.Split("\n\n")[1].Split(":")[1].Trim().Split(",").Select(int.Parse).ToList();

        return GenerateA(stdin, stdin).Min();
    }
}