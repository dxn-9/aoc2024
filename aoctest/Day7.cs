using aoc2024;
using aoc2024.Day7;

namespace aoctest;

public partial class Tests
{
    [Test]
    public void Day7Test()
    {
        var sample = Common.ReadFile("Day7/sample.txt");
        var input = Common.ReadFile("Day7/input.txt");
        Assert.That(Day7.Solve1(sample), Is.EqualTo(3749));
        Assert.That(Day7.Solve1(input), Is.EqualTo(5702958180383));
        Assert.That(Day7.Solve2(sample), Is.EqualTo(11387));
        Assert.That(Day7.Solve2(input), Is.EqualTo(11387));
    }
}