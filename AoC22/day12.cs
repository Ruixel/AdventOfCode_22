namespace AoC22;

public class Day12
{
	public static void Solve()
	{
		var input = File.ReadLines("../../../day12.txt")
			.Select(line => line.ToCharArray())
			.ToArray();

		var l = input[0].Length - 1;
		var h = input.Length - 1;

		// Hardcoded because I'm lazy
		var sX = 0;
		var sY = 20;

		var endX = 148;
		var endY = 20;
		input[endY][endX] = 'z';

		//for (int y = 0; y < dist.GetLength(0); y++)
		//{
		//	for (int x = 0; x < dist.GetLength(1); x++)
		//	{
		//		dist[y, x] = int.MaxValue;
		//	}	
		//}

		int Check(int startX, int startY)
		{
			input[startY][startX] = 'a';

			var yoDisVisited = new bool[h + 1, l + 1];
			yoDisVisited[startY, startX] = true;

			var queue = new Queue<DPos>();
			queue.Enqueue(new DPos(startX, startY, 0));
			while (queue.Count > 0)
			{
				var pos = queue.Dequeue();
				var ch = input[pos.Y][pos.X];
				//Console.WriteLine($"steps: {pos.steps} ({pos.X},{pos.Y}) '{input[pos.Y][pos.X]}'");

				if (pos.Y == endY && pos.X == endX)
				{
					//Console.WriteLine($"Part1: {pos.steps}");
					return pos.steps;
				}

				//19, 142
				if (pos.X == 19 && pos.Y == 142)
				{
					Console.WriteLine("YO");
				}

				//var visited = new bool[h + 1, l + 1];
				//Array.Copy(pos.Visited, visited, pos.Visited.Length);
				//visited[pos.Y, pos.X] = true;

				if (pos.X > 0 && input[pos.Y][pos.X - 1] <= ch + 1 &&
				    !yoDisVisited[pos.Y, pos.X - 1])
				{
					queue.Enqueue(new DPos(pos.X - 1, pos.Y, pos.steps + 1));
					yoDisVisited[pos.Y, pos.X - 1] = true;
				}

				if (pos.X < l && input[pos.Y][pos.X + 1] <= ch + 1 &&
				    !yoDisVisited[pos.Y, pos.X + 1])
				{
					queue.Enqueue(new DPos(pos.X + 1, pos.Y, pos.steps + 1));
					yoDisVisited[pos.Y, pos.X + 1] = true;
				}

				if (pos.Y > 0 && input[pos.Y - 1][pos.X] <= ch + 1 &&
				    !yoDisVisited[pos.Y - 1, pos.X])
				{
					queue.Enqueue(new DPos(pos.X, pos.Y - 1, pos.steps + 1));
					yoDisVisited[pos.Y - 1, pos.X] = true;
				}

				if (pos.Y < h && input[pos.Y + 1][pos.X] <= ch + 1 &&
				    !yoDisVisited[pos.Y + 1, pos.X])
				{
					queue.Enqueue(new DPos(pos.X, pos.Y + 1, pos.steps + 1));
					yoDisVisited[pos.Y + 1, pos.X] = true;
				}
			}

			return -1;
		}

		Console.WriteLine($"Part1: {Check(sX, sY)}");

		var part2 = int.MaxValue;
		for (int y = 0; y <= h; y++)
		{
			for (int x = 0; x <= l; x++)
			{
				if (input[y][x] == 'a')
				{
					var path = Check(x, y);
					//Console.WriteLine($"({x}, {y}) takes {path} steps.");
					if (path < part2 && path != -1)
					{
						part2 = path;
					}
				}
			}
		}

		Console.WriteLine($"Part2: {part2}");

		//Console.WriteLine($"{yoDisVisited[18, 141]}");
		//for (int y = 0; y < yoDisVisited.GetLength(0); y++)
		//{
		//	for (int x = 0; x < yoDisVisited.GetLength(1); x++)
		//	{
		//		Console.Write(yoDisVisited[y, x] ? ' ' : input[y][x]);
		//	}	
		//	Console.WriteLine();
		//}
	}

	private record DPos(int X, int Y, int steps);
}