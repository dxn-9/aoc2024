using aoc2024;
using aoc2024.Day5;

namespace aoctest;

public partial class Tests
{
    [Test]
    public void Day5Test()
    {
        var sample = Common.ReadFile("Day5/sample.txt");
        var input = Common.ReadFile("Day5/input.txt");
        Assert.That(Day5.Solve1(sample), Is.EqualTo(143));
        Assert.That(Day5.Solve1(input), Is.EqualTo(6267));
        Assert.That(Day5.Solve2(sample), Is.EqualTo(123));
        Assert.That(Day5.Solve2(input), Is.EqualTo(5184));
    }
}