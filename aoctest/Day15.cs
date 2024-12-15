using aoc2024;
using aoc2024.Day15;

namespace aoctest;

public partial class Tests
{
    [Test]
    public void Day15Test()
    {
        var sample = Common.ReadFile("Day15/sample.txt");
        var sample2 = Common.ReadFile("Day15/sample2.txt");
        var input = Common.ReadFile("Day15/input.txt");
        Assert.That(Day15.Solve1(sample), Is.EqualTo(2028));
        Assert.That(Day15.Solve1(sample2), Is.EqualTo(10092));
        Assert.That(Day15.Solve1(input), Is.EqualTo(1485257));
        Assert.That(Day15.Solve2(sample2), Is.EqualTo(9021));
        Assert.That(Day15.Solve2(input), Is.EqualTo(1475512));
    }
}