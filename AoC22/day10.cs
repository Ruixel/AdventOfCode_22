namespace AoC22;

public class Day10
{
	public static void Solve()
	{
		var input = File.ReadLines("../../../day10.txt")
			.Select(line => line.Split(" ").ToArray());

		var regX = 1;
		var cycle = 0;
		var part1 = 0;
		var image = new char[8, 40];
		RunCycle();
		foreach (var line in input)
		{
			if (line[0].Equals("noop"))
			{
				// Nothing
				RunCycle();
			}
			else if (line[0].Equals("addx"))
			{
				var v = int.Parse(line[1]);

				RunCycle();
				regX += v;
				RunCycle();
			}
		}

		void RunCycle()
		{
			DrawSprite();
			cycle++;
			CheckCycle();
		}

		void CheckCycle()
		{
			if ((cycle + 20) % 40 == 0)
			{
				Console.WriteLine($"{cycle} spotted {regX}");
				part1 += cycle * regX;
			}
		}

		void DrawSprite()
		{
			var y = cycle / 40;
			var x = cycle % 40;
			var sprite = (x >= regX - 1 && x <= regX + 1) ? '#' : '.';
			image[y, x] = sprite;
		}

		Console.WriteLine($"Part1: {part1}");

// Draw image
		Console.WriteLine("Part2:");
		for (int y = 0; y < image.GetLength(0); y++)
		{
			for (int x = 0; x < image.GetLength(1); x++)
			{
				Console.Write(image[y, x]);
			}

			Console.Write("\r\n");
		}
	}
}