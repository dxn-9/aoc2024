using aoc2024;
using aoc2024.Day24;

namespace aoctest;

public partial class Tests
{
    [Test]
    public void Day24Test()
    {
        var sample = Common.ReadFile("Day24/sample.txt");
        var input = Common.ReadFile("Day24/input.txt");
        Assert.That(Day24.Solve1(sample), Is.EqualTo(2024));
        Assert.That(Day24.Solve1(input), Is.EqualTo(60439554459366));
        Assert.That(Day24.Solve2(input), Is.EqualTo("cgh,frt,pmd,sps,tst,z05,z11,z23"));
    }
}