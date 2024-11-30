using aoc2024;
using aoc2024.Day1;

namespace aoctest;

public class Tests
{
    [Test]
    public void Test1()
    {
        var sample = Common.ReadFile("Day1/sample");
        Assert.That(Day1.Solve1(sample), Is.EqualTo(2));
    }
}