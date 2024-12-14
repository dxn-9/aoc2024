using aoc2024;
using aoc2024.Day12;

namespace aoctest;

public partial class Tests
{
    [Test]
    public void Day12Test()
    {
        var sample = Common.ReadFile("Day12/sample.txt");
        var input = Common.ReadFile("Day12/input.txt");
        Assert.That(Day12.Solve1(sample), Is.EqualTo(1930));
        Assert.That(Day12.Solve1(input), Is.EqualTo(1375574));
        Assert.That(Day12.Solve2(sample), Is.EqualTo(1206));

        var map = """
                  AAAA
                  BBCD
                  BBCC
                  EEEC
                  """;
        Assert.That(Day12.Solve2(map), Is.EqualTo(80));
        map = """
              OOOOO
              OXOXO
              OOOOO
              OXOXO
              OOOOO
              """;
        Assert.That(Day12.Solve2(map), Is.EqualTo(436));
        map = """
              EEEEE
              EXXXX
              EEEEE
              EXXXX
              EEEEE
              """;
        Assert.That(Day12.Solve2(map), Is.EqualTo(236));
        map = """
              AAAAAA
              AAABBA
              AAABBA
              ABBAAA
              ABBAAA
              AAAAAA
              """;
        Assert.That(Day12.Solve2(map), Is.EqualTo(368));
        Assert.That(Day12.Solve2(input), Is.EqualTo(10));
    }
}