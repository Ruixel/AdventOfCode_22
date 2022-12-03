namespace AoC22;

public class Day3
{
	public static void Solve()
	{
		var input = System.IO.File.ReadAllText("..\\..\\..\\day3.txt");

		var rucksacks = input.Split("\r\n");
		var part1 = 0;
		foreach (var rucksack in rucksacks)
		{
			var halfLength = rucksack.Length / 2;
			var r1 = rucksack.Take(halfLength).ToHashSet();
			var r2 = rucksack.Skip(halfLength).ToHashSet();

			r1.IntersectWith(r2);
			var duplicate = r1.First();
			if (r1.Count > 1)
				Console.WriteLine($"more than one duplicate {rucksack}");

			if (Char.IsLower(duplicate))
				part1 += duplicate - (int) 'a' + 1;
			else
				part1 += duplicate - (int) 'A' + 27;
		}

		var part2 = 0;
		for (int i = 0; i < rucksacks.Length; i += 3)
		{
			var r1 = rucksacks[i].ToHashSet();
			var r2 = rucksacks[i + 1].ToHashSet();
			var r3 = rucksacks[i + 2].ToHashSet();

			r1.IntersectWith(r2);
			r1.IntersectWith(r3);
			var badge = r1.First();

			if (Char.IsLower(badge))
				part2 += badge - (int) 'a' + 1;
			else
				part2 += badge - (int) 'A' + 27;
		}

		Console.WriteLine($"Part1: {part1}");
		Console.WriteLine($"Part2: {part2}");
	}
}