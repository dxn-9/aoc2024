using aoc2024;
using aoc2024.Day3;

namespace aoctest;

public partial class Tests
{
    [Test]
    public void Day3Test()
    {
        var sample = Common.ReadFile("Day3/sample.txt");
        var sample2 = Common.ReadFile("Day3/sample2.txt");
        var input = Common.ReadFile("Day3/input.txt");
        Assert.That(Day3.Solve1(sample), Is.EqualTo(161));
        Assert.That(Day3.Solve1(input), Is.EqualTo(183669043));
        Assert.That(Day3.Solve2(sample2), Is.EqualTo(48));
        Assert.That(Day3.Solve2(input), Is.EqualTo(59097164));
    }
}