using aoc2024;
using aoc2024.Day8;

namespace aoctest;

public partial class Tests
{
    [Test]
    public void Day8Test()
    {
        var sample = Common.ReadFile("Day8/sample.txt");
        var input = Common.ReadFile("Day8/input.txt");
        Assert.That(Day8.Solve1(sample), Is.EqualTo(14));
        Assert.That(Day8.Solve1(input), Is.EqualTo(299));
        Assert.That(Day8.Solve2(sample), Is.EqualTo(34));
        Assert.That(Day8.Solve2(input), Is.EqualTo(1032));
    }
}