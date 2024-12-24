using aoc2024;
using aoc2024.Day19;

namespace aoctest;

public partial class Tests
{
    [Test]
    public void Day19Test()
    {
        var sample = Common.ReadFile("Day19/sample.txt");
        var input = Common.ReadFile("Day19/input.txt");
        Assert.That(Day19.Solve1(sample), Is.EqualTo(6));
        Assert.That(Day19.Solve1(input), Is.EqualTo(347));
        Assert.That(Day19.Solve2(sample), Is.EqualTo(16));
        Assert.That(Day19.Solve2(input), Is.EqualTo(16));
    }
}