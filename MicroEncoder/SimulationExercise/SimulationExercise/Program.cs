﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SimulationExercise
{
   class Program
   {
      static void Main(string[] args)
      {
         var size = PromptForValidInput("Machine Size", TryPosition);
         var startPosition = PromptForValidInput("Probe Starting Position", TryPosition);
         var objectPositions = PromptForValidInput("Object Positions", TryPositions);
         var machineCommands = PromptForValidInput("Machine Commands", TryMachineCommands);

         Console.WriteLine("Calculation not implemented.");
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
            _ => Optional.Empty,
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
