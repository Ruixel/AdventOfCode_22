using System.Text.RegularExpressions;

var rx = new Regex(@"(\d+),(\d+)");
var lines = File.ReadLines("../../../day14.txt")
    .Select(line =>
    {
        var matches = rx.Match(line);
        var coords = new List<Pos>();
        foreach (Match match in rx.Matches(line))
        {
            var split = match.Value.Split(",");
            coords.Add(new Pos(int.Parse(split[0]), int.Parse(split[1])));
        }

        return coords;
    })
    .ToList();

var grid = new char[1000, 1000];
for (var y = 0; y < grid.GetLength(0); y++)
{
    for (var x = 0; x < grid.GetLength(1); x++)
    {
        grid[y, x] = '.';
    }
}

void drawGrid(int x, int y, int w, int h)
{
    for (int gy = y; gy < y + h; gy++)
    {
        for (int gx = x; gx < x + w; gx++)
        {
            Console.Write(grid![gy, gx]);
        }

        Console.WriteLine();
    }

    Console.WriteLine();
}

var diff = 0;
foreach (var lineChain in lines)
{
    var x = lineChain[0].X;
    var y = lineChain[0].Y;
    for (int i = 1; i < lineChain.Count; i++)
    {
        var dx = lineChain[i].X - x;
        var dy = lineChain[i].Y - y;
        if (dx < 0) x = lineChain[i].X;
        if (dy < 0) y = lineChain[i].Y;

        for (int lx = x; lx <= x + Math.Abs(dx); lx++)
        {
            for (int ly = y; ly <= y + Math.Abs(dy); ly++)
            {
                grid[ly, lx] = '#';
            }
        }

        x = lineChain[i].X;
        y = lineChain[i].Y;
    }
}

    drawGrid(450, 0, 150, 50);

var sand = new List<SPos>();
var part1 = 0;
//for (int step = 0; step < 10; step++)
var step = 0;
while (true)
{
    for (int i = 0; i < sand.Count; i++)
    {
        var s = sand[i];
        if (s.Done) continue;
        
        if (!(grid[s.Y + 1, s.X] == '#' || grid[s.Y + 1, s.X] == 'o'))
        {
            s.Y++;
            grid[s.Y, s.X] = '~';
        } else if (!(grid[s.Y + 1, s.X - 1] == '#' || grid[s.Y + 1, s.X - 1] == 'o'))
        {
            s.Y++;
            s.X--;
            grid[s.Y, s.X] = '~';
        } else if (!(grid[s.Y + 1, s.X + 1] == '#' || grid[s.Y + 1, s.X + 1] == 'o'))
        {
            s.Y++;
            s.X++;
            grid[s.Y, s.X] = '~';
        }
        else
        {
            grid[s.Y, s.X] = 'o';
            part1++;
            s.Done = true;
        }

        sand[i] = s;
    }

    if (grid[0, 500] == 'o')
    {
        break;
    }
    sand.Add(new SPos(500, 0));
    //drawGrid(450, 0, 100, 20);
    //Thread.Sleep(100);
    if (step % 500 == 0)
    {
        Console.WriteLine($"Part1: {part1}");
    }

    step++;
}

Console.WriteLine($"Part1: {part1}");
//drawGrid(0, 0, 1000, 1000);
    drawGrid(450, 0, 150, 20);

record Pos(int X, int Y);
struct SPos
{
    public int X;
    public int Y;
    public bool Done = false;

    public SPos(int x, int y)
    {
        X = x;
        Y = y;
    }
}