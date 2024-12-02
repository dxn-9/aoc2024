using aoc2024;
using aoc2024.Day2;

namespace aoctest;

public partial class Tests
{
    [Test]
    public void Day2Test()
    {
        var sample = Common.ReadFile("Day2/sample.txt");
        var input = Common.ReadFile("Day2/input.txt");
        Assert.That(Day2.Solve1(sample), Is.EqualTo(2));
        Assert.That(Day2.Solve1(input), Is.EqualTo(624));

        Assert.That(Day2.Solve2(sample), Is.EqualTo(4));
        Assert.That(Day2.Solve2(input), Is.EqualTo(4));
    }
}