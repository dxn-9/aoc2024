using aoc2024;
using aoc2024.Day23;

namespace aoctest;

public partial class Tests
{
    [Test]
    public void Day23Test()
    {
        var sample = Common.ReadFile("Day23/sample.txt");
        var input = Common.ReadFile("Day23/input.txt");
        Assert.That(Day23.Solve1(sample), Is.EqualTo(7));
        Assert.That(Day23.Solve1(input), Is.EqualTo(1175));
        Assert.That(Day23.Solve2(sample), Is.EqualTo("co,de,ka,ta"));
        Assert.That(Day23.Solve2(input), Is.EqualTo("bw,dr,du,ha,mm,ov,pj,qh,tz,uv,vq,wq,xw"));
    }
}