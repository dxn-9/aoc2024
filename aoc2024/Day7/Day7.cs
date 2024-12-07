using System.Text.RegularExpressions;

namespace aoc2024.Day7;

public static class Day7
{
    public static long Solve1(string input)
        =>
            input.Split(Environment.NewLine).Select(line =>
                {
                    var target = long.Parse(line.Split(':')[0]);
                    var numbers = line.Split(':')[1].Split(" ").Skip(1).Select(long.Parse).GetEnumerator();
                    numbers.MoveNext();

                    var stack = new List<long> { numbers.Current };
                    while (numbers.MoveNext())
                    {
                        var nums = stack[..stack.Count];
                        stack.Clear();
                        foreach (var num in nums)
                        {
                            stack.Add(numbers.Current * num);
                            stack.Add(numbers.Current + num);
                        }
                    }

                    return stack.Contains(target) ? target : 0;
                }
            ).Sum();

    public static long Solve2(string input)
        =>
            input.Split(Environment.NewLine).Select(line =>
                {
                    var target = long.Parse(line.Split(':')[0]);
                    var numbers = line.Split(':')[1].Split(" ").Skip(1).Select(long.Parse).GetEnumerator();
                    numbers.MoveNext();

                    var stack = new List<long> { numbers.Current };
                    while (numbers.MoveNext())
                    {
                        var nums = stack[..stack.Count];
                        stack.Clear();
                        foreach (var num in nums)
                        {
                            stack.Add(numbers.Current * num);
                            stack.Add(numbers.Current + num);
                            stack.Add(long.Parse($"{num}{numbers.Current}"));
                        }
                    }

                    return stack.Contains(target) ? target : 0;
                }
            ).Sum();
}