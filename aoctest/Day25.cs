using aoc2024;
using aoc2024.Day25;

namespace aoctest;

public partial class Tests
{
    [Test]
    public void Day25Test()
    {
        var sample = Common.ReadFile("Day25/sample.txt");
        var input = Common.ReadFile("Day25/input.txt");
        Assert.That(Day25.Solve1(sample), Is.EqualTo(3));
        Assert.That(Day25.Solve1(input), Is.EqualTo(60439554459366));
    }
}