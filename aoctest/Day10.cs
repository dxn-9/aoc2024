using aoc2024;
using aoc2024.Day10;

namespace aoctest;

public partial class Tests
{
    [Test]
    public void Day10Test()
    {
        var sample = Common.ReadFile("Day10/sample.txt");
        var input = Common.ReadFile("Day10/input.txt");
        Assert.That(Day10.Solve1(sample), Is.EqualTo(36));
        Assert.That(Day10.Solve1(input), Is.EqualTo(825));
        Assert.That(Day10.Solve2(sample), Is.EqualTo(81));
        Assert.That(Day10.Solve2(input), Is.EqualTo(1805));
    }
}