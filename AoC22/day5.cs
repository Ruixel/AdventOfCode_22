using System.Text.RegularExpressions;

namespace AoC22;

public class Day5
{
	public static void Solve()
	{
		var input = File.ReadAllText("..\\..\\..\\day5.txt").Split("\r\n\r\n");

		var crates = input[0].Split("\r\n").Reverse().Skip(1)
			.Select(line =>
			{
				var crates = (line.Length - 2) / 4;
				line = line.Substring(1, line.Length - 2);
				for (int i = 0; i < crates; i++)
				{
					line = line.Substring(0, i + 1) + line.Substring(i + 4);
				}

				return line.ToList();
			}).ToList();

		var stacks = new List<Stack<char>>();
		for (int i = 0; i < crates[0].Count; i++)
		{
			stacks.Add(new Stack<char>());
		}

		foreach (var level in crates)
		{
			for (int i = 0; i < level.Count; i++)
			{
				var crate = level[i];
				if (!char.IsWhiteSpace(crate))
					stacks[i].Push(crate);
			}
		}

		var rx = new Regex(@"^move (\d+) from (\d+) to (\d+)$");
		var instructions = input[1].Split("\r\n")
			.Select(line =>
				rx.Matches(line)[0].Groups.Values
					.Skip(1)
					.Select(v => int.Parse(v.Value)).ToList()
			);

		// Part 1
		//foreach (var inst in instructions)
		//{
		//	for (int i = 0; i < inst[0]; i++)
		//	{
		//		var crate = stacks[inst[1] - 1].Pop();
		//		stacks[inst[2]-1].Push(crate);
		//	}
		//}

		// Part 2
		foreach (var inst in instructions)
		{
			var temp = new Stack<char>();

			for (int i = 0; i < inst[0]; i++)
			{
				var crate = stacks[inst[1] - 1].Pop();
				temp.Push(crate);
			}

			for (int i = 0; i < inst[0]; i++)
			{
				var crate = temp.Pop();
				stacks[inst[2] - 1].Push(crate);
			}
		}

		// Output answer
		Console.Write("Part2: ");
		foreach (var stack in stacks)
		{
			Console.Write(stack.Peek());
		}

		Console.Write("\r\n");
	}
}