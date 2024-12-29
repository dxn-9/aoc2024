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
        // Assert.That(Day22.Solve1(sample), Is.EqualTo(37327623));
        // Assert.That(Day22.Solve1(input), Is.EqualTo(20401393616));
        var p2Sample = """
                       1
                       2
                       3
                       2024
                       """;
        Assert.That(Day22.Solve2(p2Sample), Is.EqualTo(23));
        Assert.That(Day22.Solve2(input), Is.EqualTo(23));
    }
}