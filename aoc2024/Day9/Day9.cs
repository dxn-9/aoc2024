namespace aoc2024.Day9;

class Memory
{
    public int id;
    public int size;

    public override string ToString()
    {
        return $"({id},{size})";
    }
};

public static class Day9
{
    public static long Solve1(string input)
    {
        var list = input.Select((c, index) =>
                new Memory { id = index % 2 == 0 ? index / 2 : -1, size = int.Parse(c.ToString()) })
            .ToList();
        int l = 1, r = list.Count - 1;
        while (l < r)
        {
            Memory file = list[r], freeSpace = list[l];
            if (freeSpace.id != -1)
            {
                l++;
                continue;
            }

            if (file.id == -1 || file.size == 0)
            {
                r--;
                continue;
            }

            var filesSize = file.size;
            file.size = Math.Max(file.size - freeSpace.size, 0);
            freeSpace.size = Math.Max(freeSpace.size - filesSize, 0);

            if (freeSpace.size == 0)
            {
                freeSpace.id = file.id;
                freeSpace.size = filesSize - file.size;
            }
            else
            {
                list.Insert(l, new Memory { id = file.id, size = filesSize });
            }

            l++;
        }

        return list.Where(node => node.id != -1 && node.size != 0).Aggregate((sum: 0L, index: 0), (tuple, memory) =>
        {
            for (var i = 0; i < memory.size; i++, tuple.index++)
            {
                tuple.sum += memory.id * tuple.index;
            }

            return (tuple.sum, tuple.index);
        }).sum;
    }


    public static long Solve2(string input)
    {
        var list = input.Select((c, index) =>
                new Memory { id = index % 2 == 0 ? index / 2 : -1, size = int.Parse(c.ToString()) })
            .ToList();
        var files = list.Where(memory => memory.id != -1).ToList();
        var spaces = list.Where(memory => memory.id == -1).ToList();
        var result = new List<Memory>();
        foreach (var space in spaces)
        {
            result.Add(files.First());
            files.RemoveAt(0);
            var shouldLoop = true;
            while (shouldLoop)
            {
                shouldLoop = false;
                for (int r = files.Count - 1; r >= 0; r--)
                {
                    var file = files[r];
                    if (file.id == -1) continue;
                    if (file.size > space.size) continue;

                    shouldLoop = true;
                    var fileId = file.id;
                    file.id = -1;
                    result.Add(new Memory { id = fileId, size = file.size });
                    space.size -= file.size;
                }
            }

            if (space.size > 0)
            {
                result.Add(space);
            }
        }

        return result.Where(node => node.size != 0).Aggregate((sum: 0L, index: 0), (tuple, memory) =>
        {
            if (memory.id == -1) return (tuple.sum, tuple.index + memory.size);
            for (var i = 0; i < memory.size; i++, tuple.index++)
            {
                tuple.sum += memory.id * tuple.index;
            }

            return (tuple.sum, tuple.index);
        }).sum;
    }
}