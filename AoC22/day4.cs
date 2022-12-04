using System.Text.RegularExpressions;

namespace AoC22;

public class Day4
{
	public static void Solve()
	{
		var input = System.IO.File.ReadAllText("..\\..\\..\\day4.txt");

		var rx = new Regex(@"^(\d+)-(\d+),(\d+)-(\d+)$");
		var lines = input.Split("\r\n");

		var part1 = 0;
		var part2 = 0;
		foreach (var tasks in lines)
		{
			var m = rx.Matches(tasks)[0].Groups.Values
				.Skip(1)
				.Select(v => int.Parse(v.Value)).ToList();

			if (m[0] <= m[2] && m[1] >= m[3] ||
			    m[0] >= m[2] && m[1] <= m[3])
			{
				part1++;
			}

			if (m[0] >= m[2] && m[0] <= m[3] ||
			    m[2] >= m[0] && m[2] <= m[1])
			{
				part2++;
			}
		}

		Console.WriteLine($"Part1: {part1}");
		Console.WriteLine($"Part2: {part2}");
	}
}