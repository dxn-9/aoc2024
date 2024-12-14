using System.Text.RegularExpressions;

namespace aoc2024.Day13;

record Block(Coord2 ButtonA, Coord2 ButtonB, Coord2 Prize);

public static class Day13
{
    /**
     * This puzzles can be solved with this system of equation
    * Button A: X+94, Y+34
        Button B: X+22, Y+67
        Prize: X=8400, Y=5400
        { 94x + 22y = 8400, 34x + 67y = 5400 }

        Find the intersection between the two lines.
     */
    public static long Solve1(string input)
    {
        var r = input.Split($"{Environment.NewLine}{Environment.NewLine}").Select(block =>
            {
                var r = Regex.Match(block.Split(Environment.NewLine)[0], @"X\+(\d*), Y\+(\d*)");
                var buttonA = (x: int.Parse(r.Groups[1].ToString()), y: int.Parse(r.Groups[2].ToString()));
                r = Regex.Match(block.Split(Environment.NewLine)[1], @"X\+(\d*), Y\+(\d*)");
                var buttonB = (x: int.Parse(r.Groups[1].ToString()), y: int.Parse(r.Groups[2].ToString()));
                r = Regex.Match(block.Split(Environment.NewLine)[2], @"X=(\d*), Y=(\d*)");
                var prize = (x: int.Parse(r.Groups[1].ToString()), y: int.Parse(r.Groups[2].ToString()));
                return new Block(buttonA, buttonB, prize);
            }
        ).ToList();
        var v = 0L;
        foreach (var block in r)
        {
            var s1 = block.Prize.x / (double)block.ButtonB.x;
            var x1 = -block.ButtonA.x / (double)block.ButtonB.x;

            var s2 = block.Prize.y / (double)block.ButtonB.y;
            var x2 = -block.ButtonA.y / (double)block.ButtonB.y;

            double nx = x1 - x2;
            double ns = s2 - s1;
            long aPresses = (long)double.Round(ns / nx);
            long bPresses =
                (block.Prize.x - block.ButtonA.x * aPresses) / block.ButtonB.x;
            //
            if (block.Prize.x == block.ButtonA.x * aPresses + block.ButtonB.x * bPresses &&
                block.Prize.y == block.ButtonA.y * aPresses + block.ButtonB.y * bPresses)
                v += (aPresses, bPresses).ComputeCost();
        }

        return v;
    }

    public static long Solve2(string input)
    {
        var prizeOffset = 10000000000000L;
        var r = input.Split($"{Environment.NewLine}{Environment.NewLine}").Select(block =>
            {
                var r = Regex.Match(block.Split(Environment.NewLine)[0], @"X\+(\d*), Y\+(\d*)");
                var buttonA = (x: int.Parse(r.Groups[1].ToString()), y: int.Parse(r.Groups[2].ToString()));
                r = Regex.Match(block.Split(Environment.NewLine)[1], @"X\+(\d*), Y\+(\d*)");
                var buttonB = (x: int.Parse(r.Groups[1].ToString()), y: int.Parse(r.Groups[2].ToString()));
                r = Regex.Match(block.Split(Environment.NewLine)[2], @"X=(\d*), Y=(\d*)");
                var prize = (x: prizeOffset + int.Parse(r.Groups[1].ToString()),
                    y: prizeOffset + int.Parse(r.Groups[2].ToString()));
                return new Block(buttonA, buttonB, prize);
            }
        ).ToList();
        var v = 0L;
        foreach (var block in r)
        {
            var s1 = block.Prize.x / (double)block.ButtonB.x;
            var x1 = -block.ButtonA.x / (double)block.ButtonB.x;

            var s2 = block.Prize.y / (double)block.ButtonB.y;
            var x2 = -block.ButtonA.y / (double)block.ButtonB.y;

            double nx = x1 - x2;
            double ns = s2 - s1;
            Console.WriteLine($"{nx} {ns} {s1} {x1} block {block}");
            long aPresses = (long)double.Round(ns / nx);
            long bPresses =
                (block.Prize.x - block.ButtonA.x * aPresses) / block.ButtonB.x;
            if (block.Prize.x == block.ButtonA.x * aPresses + block.ButtonB.x * bPresses &&
                block.Prize.y == block.ButtonA.y * aPresses + block.ButtonB.y * bPresses)
                v += (aPresses, bPresses).ComputeCost();
        }

        return v;
    }

    static long ComputeCost(this Coord2 coord) => coord.x * 3L + coord.y;
}