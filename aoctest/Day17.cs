using aoc2024;
using aoc2024.Day17;

namespace aoctest;

public partial class Tests
{
    [Test]
    public void Day17Test()
    {
        var sample = Common.ReadFile("Day17/sample.txt");
        var input = Common.ReadFile("Day17/input.txt");
        Assert.That(Day17.Solve1(sample), Is.EqualTo("4,6,3,5,6,3,5,2,1,0"));
        Assert.That(Day17.Solve1(input), Is.EqualTo("7,3,1,3,6,3,6,0,2"));

        var s2 = """
                 Register A: 2024
                 Register B: 0
                 Register C: 0

                 Program: 0,3,5,4,3,0
                 """;
        Assert.That(Day17.Solve2(s2), Is.EqualTo(117440));
        Assert.That(Day17.Solve2(input), Is.EqualTo(0));
    }
}