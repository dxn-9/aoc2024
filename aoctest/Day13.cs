using aoc2024;
using aoc2024.Day13;

namespace aoctest;

public partial class Tests
{
    [Test]
    public void Day13Test()
    {
        var sample = Common.ReadFile("Day13/sample.txt");
        var input = Common.ReadFile("Day13/input.txt");
        Assert.That(Day13.Solve1(sample), Is.EqualTo(480));
        Assert.That(Day13.Solve1(input), Is.EqualTo(26599));
        Assert.That(Day13.Solve2(input), Is.EqualTo(106228669504887));
    }
}