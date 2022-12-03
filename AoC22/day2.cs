namespace AoC22;

public class Day2
{
	public static void Solve()
	{
		var input = System.IO.File.ReadAllText("..\\..\\..\\day2.txt");

		var matches = from line in input.Split("\r\n")
			select line.Split(" ");

		// A, X = rock
		// B, Y = paper
		// C, Z = scissors

		var score = (from s in matches select (int) s[1][0] - (int) 'W').Sum();
		var total_score = score;
		foreach (var m in matches)
		{
			if (m[0] == "A" && m[1] == "Z" || m[0] == "B" && m[1] == "X" || m[0] == "C" && m[1] == "Y")
				total_score += 0;

			if (m[0] == "A" && m[1] == "X" || m[0] == "B" && m[1] == "Y" || m[0] == "C" && m[1] == "Z")
				total_score += 3;

			if (m[0] == "A" && m[1] == "Y" || m[0] == "B" && m[1] == "Z" || m[0] == "C" && m[1] == "X")
				total_score += 6;
		}

		Console.WriteLine($"Part1: {total_score}");

		var part2 = 0;
		var choises = new List<string>();
		foreach (var m in matches)
		{
			if (m[1] == "X")
			{
				part2 += 0;
				if (m[0] == "A") part2 += 3;
				if (m[0] == "B") part2 += 1;
				if (m[0] == "C") part2 += 2;
			}

			if (m[1] == "Y")
			{
				part2 += 3;
				if (m[0] == "A") part2 += 1;
				if (m[0] == "B") part2 += 2;
				if (m[0] == "C") part2 += 3;
			}

			if (m[1] == "Z")
			{
				part2 += 6;
				if (m[0] == "A") part2 += 2;
				if (m[0] == "B") part2 += 3;
				if (m[0] == "C") part2 += 1;
			}
		}

		Console.WriteLine($"Part2: {part2}");
	}
}