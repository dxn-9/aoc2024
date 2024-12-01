using aoc2024;
using aoc2024.Day1;

namespace aoctest;

public class Tests
{
    [Test]
    public void Day1Test()
    {
        var sample = Common.ReadFile("Day1/Sample");
        Assert.That(Day1.Solve1(sample), Is.EqualTo(11));
        var input = Common.ReadFile("Day1/Input");
        Assert.That(Day1.Solve1(input), Is.EqualTo(765748));

        Assert.That(Day1.Solve2(sample), Is.EqualTo(31));
        Assert.That(Day1.Solve2(input), Is.EqualTo(27732508));
    }
}