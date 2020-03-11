using System;
using System.Collections.Generic;

namespace SimulationExercise
{
	class Program
	{
		static void Main(string[] args)
		{
			SimConfiguration config = new SimConfiguration
			{
				MachineSize = PromptForValidInput("Machine Size", InputParseHelpers.TryPosition),
				ProbePosition = new Stack<Vector>(new[] { PromptForValidInput("Probe Starting Position", InputParseHelpers.TryPosition) }),
				ObjectPositions = PromptForValidInput("Object Positions", InputParseHelpers.TryPositions),
				MachineCommands = PromptForValidInput("Machine Commands", InputParseHelpers.TryMachineCommands)
			};

			//SimConfiguration config = new SimConfiguration
			//{
			//	MachineSize = InputParseHelpers.TryPosition("7 7").Value,
			//	ProbePosition = new Stack<Vector>(new[] { InputParseHelpers.TryPosition("2 4").Value }),
			//	ObjectPositions = InputParseHelpers.TryPositions("0 2, 1 2, 2 2, 3 2, 4 1").Value,
			//	MachineCommands = InputParseHelpers.TryMachineCommands("DRODFRODDFR").Value
			//};

			var cv = new CommandValidationEngine(config);
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
	}
}
