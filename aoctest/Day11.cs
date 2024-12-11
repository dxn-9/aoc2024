using aoc2024;
using aoc2024.Day11;

namespace aoctest;

public partial class Tests
{
    [Test]
    public void Day11Test()
    {
        var sample = Common.ReadFile("Day11/sample.txt");
        var input = Common.ReadFile("Day11/input.txt");
        Assert.That(Day11.Solve1(sample), Is.EqualTo(55312));
        Assert.That(Day11.Solve1(input), Is.EqualTo(203228));
        Assert.That(Day11.Solve2(input), Is.EqualTo(240884656550923));
    }
}