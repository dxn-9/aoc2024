using aoc2024;
using aoc2024.Day14;

namespace aoctest;

public partial class Tests
{
    [Test]
    public void Day14Test()
    {
        var sample = Common.ReadFile("Day14/sample.txt");
        var input = Common.ReadFile("Day14/input.txt");
        Assert.That(Day14.Solve1(sample, 11, 7), Is.EqualTo(12));
        Assert.That(Day14.Solve1(input, 101, 103), Is.EqualTo(231852216));
        Assert.That(Day14.Solve2(input, 101, 103), Is.EqualTo(8159));
    }
}