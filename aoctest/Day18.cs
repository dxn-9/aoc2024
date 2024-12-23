using aoc2024;
using aoc2024.Day18;

namespace aoctest;

public partial class Tests
{
    [Test]
    public void Day18Test()
    {
        var sample = Common.ReadFile("Day18/sample.txt");
        var input = Common.ReadFile("Day18/input.txt");
        Assert.That(Day18.Solve1(sample), Is.EqualTo(22));
        Assert.That(Day18.Solve1(input, 71, 1024), Is.EqualTo(310));
        Assert.That(Day18.Solve2(sample), Is.EqualTo("6,1"));
        Assert.That(Day18.Solve2(input, 71, 1024), Is.EqualTo("16,46"));
    }
}