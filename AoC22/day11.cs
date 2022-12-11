namespace AoC22;

public class Day11
{
	public static void Solve()
	{
		var monkes = File.ReadAllText("../../../day11.txt")
			.Split("\r\n\r\n")
			.Select(data =>
			{
				var lines = data.Split("\r\n");
				var id = int.Parse(lines[0].Substring(7, 1));
				var startingItems = lines[1].Replace(",", "")
					.Split(" ")
					.Skip(4)
					.Select(ulong.Parse)
					.ToList();
				var op = lines[2].Substring(23, 1);
				int opValue;
				if (!int.TryParse(lines[2].Substring(25), out opValue))
				{
					opValue = int.MaxValue;
				}

				var divisibleBy = int.Parse(lines[3].Substring(21));
				var trueMonke = int.Parse(lines[4].Substring(29));
				var falseMonkey = int.Parse(lines[5].Substring(30));

				return new Monkey(id, startingItems, op, opValue, divisibleBy, trueMonke, falseMonkey);
			})
			.ToList();

		for (int round = 0; round < 10000; round++)
		{
			foreach (var monke in monkes)
			{
				monke.Inspect(monkes);
			}

			if (round == 20)
			{
				var part1 = monkes
					.OrderByDescending(m => m)
					.Take(2)
					.Select(m => m.Inspections)
					.Aggregate((m1, m2) => m1 * m2);
				Console.WriteLine($"Part1: {part1}");
			}

			//if (round % 1000 == 0)
			//{
			//	Console.WriteLine($"Round: {round}");
			//	foreach (var monke in monkes)
			//	{
			//		Console.WriteLine($"Monkey {monke.Id} inspected items {monke.Inspections} times");
			//	}

			//	Console.WriteLine();
			//}

			//foreach (var monke in monkes)
			//{
			//	Console.Write($"Monkey {monke.Id}: ");
			//	foreach (var item in monke.Items)
			//	{
			//		Console.Write($"{item}, ");
			//	}
			//	Console.WriteLine("");
			//}
		}

		var part2 = monkes
			.OrderByDescending(m => m)
			.Take(2)
			.Select(m => (ulong)m.Inspections)
			.Aggregate((m1, m2) => m1 * m2);
		Console.WriteLine($"Part2: {part2}");
	}

	internal class Monkey : IComparable<Monkey>
	{
		public int Id { get; }
		public List<ulong> Items;
		private string _op;
		private int _secondOperand;
		private int _divisibleBy;
		private int _trueMonkey;
		private int _falseMonkey;
		public int Inspections { get; private set; }

		private static ulong _commonFactor = 3;

		public Monkey(int id, List<ulong> items, string op, int secondOperand, int divisibleBy, int trueMonkey,
			int falseMonkey)
		{
			Id = id;
			Items = items;
			_op = op;
			_secondOperand = secondOperand;
			_divisibleBy = divisibleBy;
			_trueMonkey = trueMonkey;
			_falseMonkey = falseMonkey;

			Inspections = 0;
			_commonFactor *= (ulong) _divisibleBy;
		}

		public void Inspect(List<Monkey> monkes)
		{
			//Console.WriteLine($"Monkey {Id}:");
			for (int i = 0; i < Items.Count; i++)
			{
				Inspections++;
				//Console.WriteLine($"  Monkey inspects and item with a worry level of {Items[i]}");

				var operand = (_secondOperand == int.MaxValue) ? Items[i] : (ulong) _secondOperand;
				if (_op == "+")
					Items[i] += operand;
				else
					Items[i] *= operand;
				//Console.WriteLine($"    Worry level is multiplied by {operand} to {Items[i]}.");

				//Items[i] = Items[i] / 3;
				Items[i] = Items[i] % _commonFactor;
				//Console.WriteLine($"    Monkey gets bored with item. Worry level is divided by 3 to {Items[i]}.");

				if (Items[i] % (ulong) _divisibleBy == 0)
				{
					//Console.WriteLine($"    Current worry level is divisible by {_divisibleBy}.");
					//Console.WriteLine($"    Item with worry level {Items[i]} is thrown to monkey {_trueMonkey}.");
					monkes[_trueMonkey].Items.Add(Items[i]);
				}
				else
				{
					//Console.WriteLine($"    Current worry level is not divisible by {_divisibleBy}.");
					//Console.WriteLine($"    Item with worry level {Items[i]} is thrown to monkey {_falseMonkey}.");
					monkes[_falseMonkey].Items.Add(Items[i]);
				}
			}

			Items.Clear();
		}

		public int CompareTo(Monkey? obj)
		{
			return Inspections - obj!.Inspections;
		}
	}
}