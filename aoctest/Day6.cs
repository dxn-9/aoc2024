using aoc2024;
using aoc2024.Day6;

namespace aoctest;

public partial class Tests
{
    [Test]
    public void Day6Test()
    {
        var sample = Common.ReadFile("Day6/sample.txt");
        var input = Common.ReadFile("Day6/input.txt");
        Assert.That(Day6.Solve1(sample), Is.EqualTo(41));
        Assert.That(Day6.Solve1(input), Is.EqualTo(5145));
        Assert.That(Day6.Solve2(sample), Is.EqualTo(6));
        Assert.That(Day6.Solve2(input), Is.EqualTo(1523));
    }
}