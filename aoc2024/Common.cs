global using Coord2 = (long x, long y);
global using Coord2I = (int x, int y);

namespace aoc2024;

public static class Common
{
    public static string ReadFile(string path)
        => File.OpenText(Path.Combine("..", "..", "..", "..", "aoc2024", path)).ReadToEnd();
}