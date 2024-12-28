using aoc2024;
using aoc2024.Day22;

namespace aoctest;

public partial class Tests
{
    [Test]
    public void Day22Test()
    {
        var sample = Common.ReadFile("Day22/sample.txt");
        var input = Common.ReadFile("Day22/input.txt");
        Assert.That(Day22.Solve1(sample), Is.EqualTo(37327623));
        Assert.That(Day22.Solve1(input), Is.EqualTo(20401393616));
    }
}