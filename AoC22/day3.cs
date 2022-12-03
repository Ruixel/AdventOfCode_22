namespace AoC22;

public class Day3
{
	public static void Solve()
	{
		var input = System.IO.File.ReadAllText("..\\..\\..\\day3.txt");

		var rucksacks = input.Split("\r\n");

		var part1 = rucksacks
			.Select(r => r
				.Chunk(r.Length / 2)
				.Select(n => n.ToHashSet())
				.Aggregate((st, r) => st.Intersect(r).ToHashSet())
				.First())
			.Select(ch => Char.IsLower(ch) ? ch - 'a' + 1 : ch - 'A' + 27)
			.Sum();

		Console.WriteLine($"Part1: {part1}");

		var part2 = rucksacks
			.Chunk(3)
			.Select(c => c
				.Select(set => set.ToHashSet())
				.Aggregate((st, r) => st.Intersect(r).ToHashSet())
				.First()
			)
			.Select(ch => Char.IsLower(ch) ? ch - 'a' + 1 : ch - 'A' + 27)
			.Sum();

		Console.WriteLine($"Part2: {part2}");
	}
}