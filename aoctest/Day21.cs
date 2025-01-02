using aoc2024;
using aoc2024.Day21;

namespace aoctest;

public partial class Tests
{
    [Test]
    public void Day21Test()
    {
        var sample = Common.ReadFile("Day21/sample.txt");
        var input = Common.ReadFile("Day21/input.txt");
        Assert.That(Day21.Solve1(sample, 3), Is.EqualTo(126384));
        Assert.That(Day21.Solve1(input, 3), Is.EqualTo(222670));
        Assert.That(Day21.Solve1(input, 26), Is.EqualTo(271397390297138));
    }
}