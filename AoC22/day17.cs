using System.Text;

namespace AoC22;

public class Day17
{
	public static void Solve()
	{
		var wind = File.ReadAllText("../../../day17.txt").ToCharArray();

		var rockA = new char[,]
		{
			{'@', '@', '@', '@'},
			{'.', '.', '.', '.'},
			{'.', '.', '.', '.'},
			{'.', '.', '.', '.'}
		};

		var rockB = new char[,]
		{
			{'.', '@', '.', '.'},
			{'@', '@', '@', '.'},
			{'.', '@', '.', '.'},
			{'.', '.', '.', '.'}
		};

		var rockC = new char[,]
		{
			{'@', '@', '@', '.'},
			{'.', '.', '@', '.'},
			{'.', '.', '@', '.'},
			{'.', '.', '.', '.'}
		};

		var rockD = new char[,]
		{
			{'@', '.', '.', '.'},
			{'@', '.', '.', '.'},
			{'@', '.', '.', '.'},
			{'@', '.', '.', '.'}
		};

		var rockE = new char[,]
		{
			{'@', '@', '.', '.'},
			{'@', '@', '.', '.'},
			{'.', '.', '.', '.'},
			{'.', '.', '.', '.'}
		};

		var rocks = new[] {rockA, rockB, rockC, rockD, rockE};
		var rockHeights = new[] {1, 3, 3, 4, 2};

		var cave = new char[100000000, 7];
		for (int y = 0; y < cave.GetLength(0); y++)
		{
			for (int x = 0; x < cave.GetLength(1); x++)
			{
				cave[y, x] = '.';
			}
		}

		void RockLoop(char[,] rock, int x, int y, Action<int, int> func)
		{
			for (int rx = 0; rx < 4; rx++)
			{
				for (int ry = 0; ry < 4; ry++)
				{
					if (rock[ry, rx] == '@')
					{
						var caveX = rx + x;
						var caveY = ry + y;

						func(caveX, caveY);
					}
				}
			}
		}

		var topY = 3;
		var windIndex = 0;
		var stupidMap = new Dictionary<string, int>();
		for (int i = 0; i < 10001; i++)
		{
			var rock = rocks[i % 5];
			var y = topY;
			var x = 2;

			//Console.WriteLine("Spawned");
			//DrawCave(0, 10, rock, x, y);

			var landed = false;
			while (!landed)
			{
				if (wind[windIndex] == '>')
				{
					var canMoveRight = true;
					RockLoop(rock, x, y, ((caveX, caveY) =>
					{
						if (caveX + 1 >= 7 || cave[caveY, caveX + 1] == '#') canMoveRight = false;
					}));

					if (canMoveRight) x++;
				}

				if (wind[windIndex] == '<')
				{
					var canMoveLeft = true;
					RockLoop(rock, x, y, ((caveX, caveY) =>
					{
						if (caveX - 1 < 0 || cave[caveY, caveX - 1] == '#') canMoveLeft = false;
					}));

					if (canMoveLeft) x--;
				}

				//Console.WriteLine($"After moved by wind {wind[windIndex]}");	
				//DrawCave(0, 10, rock, x, y);

				// Fall
				RockLoop(rock, x, y, ((caveX, caveY) =>
				{
					if (caveY - 1 < 0 || cave[caveY - 1, caveX] == '#')
					{
						landed = true;
					}
				}));

				if (!landed) y--;
				windIndex++;
				if (windIndex >= wind.Length) windIndex = 0;

				//Console.WriteLine("Fall");	
				//DrawCave(0, 10, rock, x, y);
			}

			// Add rock
			RockLoop(rock, x, y, (caveX, caveY) => { cave[caveY, caveX] = '#'; });

			if (y + 3 + rockHeights[i % 5] > topY) topY = y + 3 + rockHeights[i % 5];

			if (i == 2022)
				Console.WriteLine($"Part1: {topY - 3}");

			if (i == 1875)
				Console.WriteLine($"step {i}: {topY - 3}");

			// This is so DUMB 
			//if (i == 2239 || i == 504 || i == 3610)
			if ((i + 504) % (2239 - 504) == 0)
			{
				//Console.WriteLine($"Step: {i}, height: {topY - 3}");
				//DrawCave(topY-20, 20);
				//Console.WriteLine();
			}

			// Top values were found thru this
			//if (i>100)// && i % (5 * 40 * 7) == 0)
			//{
			//	//Console.WriteLine($"Step: {i}");
			//	var cavestr = CaveStr(topY - 100, 100);
			//	if (stupidMap.ContainsKey(cavestr))
			//	{
			//		//Console.WriteLine($"Step: {i} matching with step {stupidMap[cavestr]}");
			//		//DrawCave(topY-20, 20);
			//		//Console.WriteLine();

			//	}
			//	else
			//	{
			//		stupidMap.Add(cavestr, i);
			//	}
			//}
		}

		// Step 221200, h: 354524
		// Step 707000, h: 1133204
		//var stepDiff = 707000 - 2121200

		// Step 504, h: 770
		// Step 2239, h: 3551
		var stepDiff = 2239 - 504;
		var hDiff = 3551 - 770;

		string CaveStr(int y, int length)
		{
			var stringBuilder = new StringBuilder();
			for (int caveY = y + length; caveY >= y; caveY--)
			{
				for (int caveX = 0; caveX < 7; caveX++)
				{
					stringBuilder.Append(cave![caveY, caveX]);
				}

				stringBuilder.Append('\n');
			}

			return stringBuilder.ToString();
		}

		void DrawCave(int y, int length, char[,]? rock = null, int rx = -10, int ry = 0)
		{
			Console.WriteLine("Cave");
			for (int caveY = y + length; caveY >= y; caveY--)
			{
				for (int caveX = 0; caveX < 7; caveX++)
				{
					if (caveX >= rx && caveX < rx + 4 && caveY >= ry && caveY < ry + 4 && rock != null &&
					    rock[caveY - ry, caveX - rx] == '@')
					{
						Console.Write('@');
					}
					else
					{
						Console.Write(cave![caveY, caveX]);
					}
				}

				Console.WriteLine();
			}

			Console.WriteLine();
		}
	}
}