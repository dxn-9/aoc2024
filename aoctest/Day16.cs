using aoc2024;
using aoc2024.Day16;

namespace aoctest;

public partial class Tests
{
    [Test]
    public void Day16Test()
    {
        var sample = Common.ReadFile("Day16/sample.txt");
        var sample2 = Common.ReadFile("Day16/sample2.txt");
        var input = Common.ReadFile("Day16/input.txt");
        Assert.That(Day16.Solve1(sample), Is.EqualTo(7036));
        Assert.That(Day16.Solve1(sample2), Is.EqualTo(11048));
        Assert.That(Day16.Solve1(input), Is.EqualTo(72428));
        Assert.That(Day16.Solve2(sample), Is.EqualTo(45));
        Assert.That(Day16.Solve2(sample2), Is.EqualTo(64));
        Assert.That(Day16.Solve2(input), Is.EqualTo(456));
    }
}