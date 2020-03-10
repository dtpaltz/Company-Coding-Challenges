using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SimulationExercise
{
	class Program
	{
		static void Main(string[] args)
		{
			//SimConfiguration config = new SimConfiguration
			//{
			//	MachineSize = PromptForValidInput("Machine Size", TryPosition),
			//	ProbeStartPosition = PromptForValidInput("Probe Starting Position", TryPosition),
			//	ObjectPositions = PromptForValidInput("Object Positions", TryPositions),
			//	MachineCommands = PromptForValidInput("Machine Commands", TryMachineCommands)
			//};

			SimConfiguration config = new SimConfiguration
			{
				MachineSize = TryPosition("7 7").Value,
				ProbePosition = new Stack<Vector>(new[] { new TryPosition("2 4").Value }),
				ObjectPositions = TryPositions("0 2, 1 2, 2 2, 3 2, 4 1").Value,
				MachineCommands = TryMachineCommands("DRODFRODDFR").Value
			};

			var cv = new CommandValidator(config);
			foreach (string testResults in cv.TestCommands())
			{
				Console.WriteLine(testResults);
			}
		}

		private static T PromptForValidInput<T>(string prompt, Func<string, Optional<T>> tryParse)
		{
			while (true)
			{
				Console.Write($"{prompt}: ");
				var input = Console.ReadLine();
				var result = tryParse(input);

				if (result.HasValue)
					return result.Value;

				Console.WriteLine("Invalid input.");
			}
		}

		private static Optional<List<Vector>> TryPositions(string input)
		{
			var positions = input.Split(',')
								 .Where(i => !string.IsNullOrWhiteSpace(i))
								 .Select(TryPosition)
								 .ToList();

			if (positions.Any(i => i.IsEmpty))
				return Optional.Empty;

			return positions.Select(i => i.Value).ToList();
		}

		private static Optional<List<MachineCommand>> TryMachineCommands(string inputs)
		{
			var commands = inputs.Select(TryMachineCommand).ToList();
			if (commands.Any(i => i.IsEmpty))
				return Optional.Empty;

			return commands.Select(i => i.Value).ToList();
		}

		private static Optional<MachineCommand> TryMachineCommand(char input)
		{
			return char.ToLower(input) switch
			{
				'l' => MachineCommand.Left,
				'r' => MachineCommand.Right,
				'u' => MachineCommand.Up,
				'd' => MachineCommand.Down,
				'o' => MachineCommand.MeasurementOn,
				'f' => MachineCommand.MeasurementOff,
				_ => Optional.Empty
			};
		}

		private static Optional<Vector> TryPosition(string input)
		{
			var pattern = new Regex(@"^\s*([0-9]+)\s+([0-9]+)\s*$");
			var result = pattern.Match(input);

			if (!result.Success)
				return Optional.Empty;

			if (int.TryParse(result.Groups[1].Value, out var x) && int.TryParse(result.Groups[2].Value, out var y))
				return new Vector(x, y);

			return Optional.Empty;
		}
	}
}
