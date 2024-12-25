using aoc2024;
using aoc2024.Day20;

namespace aoctest;

public partial class Tests
{
    [Test]
    public void Day20Test()
    {
        var sample = Common.ReadFile("Day20/sample.txt");
        var input = Common.ReadFile("Day20/input.txt");
        Assert.That(Day20.Solve1(sample, 1), Is.EqualTo(44));
        Assert.That(Day20.Solve1(input, 100), Is.EqualTo(1338));
        Assert.That(Day20.Solve2(sample, 50), Is.EqualTo(285));
        Assert.That(Day20.Solve2(input, 100), Is.EqualTo(1338));
    }
}