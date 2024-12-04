using aoc2024;
using aoc2024.Day4;

namespace aoctest;

public partial class Tests
{
    [Test]
    public void Day4Test()
    {
        var sample = Common.ReadFile("Day4/sample.txt");
        var input = Common.ReadFile("Day4/input.txt");
        Assert.That(Day4.Solve1(sample), Is.EqualTo(18));
        Assert.That(Day4.Solve1(input), Is.EqualTo(2578));
        Assert.That(Day4.Solve2(sample), Is.EqualTo(9));
        Assert.That(Day4.Solve2(input), Is.EqualTo(1972));
    }
}