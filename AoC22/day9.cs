namespace AoC22;

public class Day9
{
	public static void Solve()
	{
		var input = File.ReadLines("../../../day9.txt")
			.Select(line => line.Split(" "));

		var rope = new List<Pos>();
		for (int i = 0; i < 10; i++)
		{
			rope.Add(new Pos(0, 0));
		}

		var remember1 = new HashSet<int>();
		var remember9 = new HashSet<int>();

		foreach (var line in input)
		{
			var dirName = line[0];
			var steps = int.Parse(line[1]);

			var dir = new Pos(0, 1);
			if (dirName.Equals("D")) dir = new Pos(0, -1);
			if (dirName.Equals("R")) dir = new Pos(1, 0);
			if (dirName.Equals("L")) dir = new Pos(-1, 0);

			for (int i = 0; i < steps; i++)
			{
				rope[0].X += dir.X;
				rope[0].Y += dir.Y;

				for (int j = 1; j < 10; j++)
				{
					var dX = rope[j - 1].X - rope[j].X;
					var dY = rope[j - 1].Y - rope[j].Y;

					if (dX == 2 && dY == 0) rope[j].X++;
					else if (dX == -2 && dY == 0) rope[j].X--;
					else if (dX == 0 && dY == 2) rope[j].Y++;
					else if (dX == 0 && dY == -2) rope[j].Y--;
					else if (Math.Abs(dX) + Math.Abs(dY) >= 3)
					{
						if (dX > 0) rope[j].X++;
						else rope[j].X--;

						if (dY > 0) rope[j].Y++;
						else rope[j].Y--;
					}
				}

				remember1.Add(rope[1].Y * 10000 + rope[1].X);
				remember9.Add(rope[9].Y * 10000 + rope[9].X);
			}
		}

		Console.WriteLine($"Part1: {remember1.Count}");
		Console.WriteLine($"Part2: {remember9.Count}");
	}

	public record Pos(int X, int Y)
	{
		public int X { get; set; } = X;
		public int Y { get; set; } = Y;

		public void Copy(Pos other)
		{
			X = other.X;
			Y = other.Y;
		}
	}
}