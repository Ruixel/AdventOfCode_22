namespace AoC22;

public class Day1
{
	public static void Solve()
	{
		var input = System.IO.File.ReadAllText("..\\..\\..\\day1.txt");
		var elves = input.Split("\r\n\r\n")
			.Select(elf => elf.Split("\r\n").Select(n => int.Parse(n)).Sum()).ToList();

		Console.WriteLine($"Part1: {elves.Max()}");

		elves.Sort();
		Console.WriteLine($"Part2: {elves.TakeLast(3).Sum()}");
	}
}