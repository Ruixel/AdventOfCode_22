using System.Runtime.InteropServices.ComTypes;

var wind = File.ReadAllText("../../../day15.txt").ToCharArray();

// L = Left Collision
// R = Right Collision
//
// X = Left/Down Collision
// Y = Right/Down Collision
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

var cave = new char[25000, 7];
//var caveA = new char[10000, 7];
//var caveB = new char[10000, 7];
var testSet = new HashSet<(int, int, int, int, int, int, int, int, int)>();
var anotherMap = new Dictionary<int, int>();

//var cave = caveA;
for (int y = 0; y < cave.GetLength(0); y++)
{
	for (int x = 0; x < cave.GetLength(1); x++)
	{
		cave[y, x] = '.';
	}
}

void RockLoop(char[,] rock, int x, long y, Action<int, long> func)
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

var topY = 3L;
var windIndex = 0;
var check = 50;
for (long i = 0; i < 2000; i++)
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
	
	//DrawCave(Math.Max((int)topY - 15, 0), 20);
	
	// how far 
	//var yum = Math.Max((int) topY  5, 0);
	var distToBottom = new int[7];
	for (int caveY = (int)topY; caveY >= Math.Max((int)topY - check, 0); caveY--)
	{
		var c = false;
		for (int caveX = 0; caveX < 7; caveX++)
		{
			if (distToBottom[caveX] == 0) c = true;
			if (cave[caveY, caveX] == '#' && distToBottom[caveX] == 0)
			{
				distToBottom[caveX] = (int)topY - caveY;
			}
		}

		if (!c) break;
		//Console.WriteLine();
	}

	var min = distToBottom.Min();
	var normalised = distToBottom.Select(p => p - min).ToArray();
	
	var hashThing = 0L;
	for (var j = 0; j < 7; j++)
	{
		//if (distToBottom[j] == 0 && topY > 400)
		//{
		//	throw new Exception("Not enough");
		//}

		hashThing += normalised[j] * (j * (long)check);
		//Console.Write($"{normalised[j]}, ");
	}
	//Console.WriteLine();

	//foreach (var dist in distToBottom)
	//{
	//	Console.Write($"{dist}, ");
	//}
	//Console.WriteLine(hashThing);

	//var hash = (normalised[0], normalised[1], normalised[2], normalised[3], normalised[4], normalised[5], normalised[6], windIndex, (int)i % 5);
	var hashy = normalised[0].GetHashCode() ^ normalised[1].GetHashCode() ^ normalised[2].GetHashCode() ^ normalised[3].GetHashCode() ^
		normalised[4].GetHashCode() ^ normalised[5].GetHashCode() ^ normalised[6].GetHashCode() ^ (1000 + windIndex.GetHashCode()) ^ (((int)i % 5).GetHashCode() + 1000000);
	Console.WriteLine($"hashy: {hashy}");
	if (anotherMap.ContainsKey(hashy))
	{
		Console.WriteLine($"Found something at {i}");
		Console.WriteLine($"{anotherMap[hashy]}");
	}
	else
	{
		anotherMap.Add( hashy, (int) i );
	}
		
}

Console.WriteLine($"Part1: {topY - 3}");

void DrawCave(int y, int length, char[,]? rock = null, int rx = -10, int ry = 0)
{
	Console.WriteLine("Cave");
	for (int caveY = y + length; caveY >= y; caveY--)
	{
		for (int caveX = 0; caveX < 7; caveX++)
		{
			if (caveX >= rx && caveX < rx + 4 && caveY >= ry && caveY < ry + 4 && rock != null && rock[caveY - ry, caveX - rx] == '@')
			{
					Console.Write('@');
			}
			else
			{
				Console.Write(cave[caveY, caveX]);
			}
		}

		Console.WriteLine();
	}

	Console.WriteLine();
}