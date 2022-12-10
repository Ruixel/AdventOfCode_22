namespace AoC22;

public class Day8
{
	public static void Solve()
	{
		var input = File.ReadAllText("..\\..\\..\\day8.txt");
		var trees = input.Split("\r\n")
			.Select(line => line.ToCharArray().Select(ch => int.Parse(ch.ToString())).ToArray())
			.ToArray();

		var part1 = 0;
		var part2 = 0;
		for (var y = 1; y < trees.Length - 1; y++)
		{
			for (var x = 1; x < trees[y].Length - 1; x++)
			{
				if (IsVisible(trees, x, y, -1, 0) ||
				    IsVisible(trees, x, y, 1, 0) ||
				    IsVisible(trees, x, y, 0, -1) ||
				    IsVisible(trees, x, y, 0, 1))
				{
					part1++;

					var treeScore =
						GetScore(trees, x, y, -1, 0) *
						GetScore(trees, x, y, 1, 0) *
						GetScore(trees, x, y, 0, -1) *
						GetScore(trees, x, y, 0, 1);

					Console.WriteLine($"{y},{x} has score {treeScore}");

					if (treeScore > part2)
						part2 = treeScore;
				}
			}
		}

		part1 += trees.Length * 2;
		part1 += trees[0].Length * 2;
		part1 -= 4;

		Console.WriteLine($"Part1: {part1}");
		Console.WriteLine($"Part2: {part2}");

		bool IsVisible(int[][] trees, int x, int y, int dx, int dy)
		{
			var prev = trees[y][x];
			x += dx;
			y += dy;

			while (x >= 0 && y >= 0 && x < trees[0].Length && y < trees.Length)
			{
				var newTree = trees[y][x];
				if (newTree >= prev)
					return false;

				//prev = newTree;
				x += dx;
				y += dy;
			}

			return true;
		}

		int GetScore(int[][] trees, int x, int y, int dx, int dy)
		{
			var prev = trees[y][x];
			var score = 0;
			x += dx;
			y += dy;

			while (x >= 0 && y >= 0 && x < trees[0].Length && y < trees.Length)
			{
				score++;

				var newTree = trees[y][x];
				if (newTree >= prev)
					break;

				//prev = newTree;
				x += dx;
				y += dy;
			}

			return score;
		}
	}
}