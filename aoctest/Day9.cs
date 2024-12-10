using aoc2024;
using aoc2024.Day9;

namespace aoctest;

public partial class Tests
{
    [Test]
    public void Day9Test()
    {
        var sample = Common.ReadFile("Day9/sample.txt");
        var input = Common.ReadFile("Day9/input.txt");
        Assert.That(Day9.Solve1(sample), Is.EqualTo(1928));
        Assert.That(Day9.Solve1(input), Is.EqualTo(6307275788409));
        Assert.That(Day9.Solve2(sample), Is.EqualTo(2858));
        Assert.That(Day9.Solve2(input), Is.EqualTo(6327174563252));
    }
}