using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimulationExercise
{
	public class CommandValidationEngine
	{
		private List<SimConfiguration> m_Configurations;

		public CommandValidationEngine(SimConfiguration config) : this(new List<SimConfiguration>() { config })
		{
		}

		public CommandValidationEngine(List<SimConfiguration> configs)
		{
			m_Configurations = configs;
		}

		public IEnumerable<string> TestCommands()
		{
			for (int i = 0; i < m_Configurations.Count; i++)
			{
				StringBuilder sb = new StringBuilder();
				SimConfiguration sc = m_Configurations[i];
				List<string> measurements = new List<string>();

				sb.AppendLine(new string('=', 50));
				sb.AppendLine($"Command Test Simulation #{i + 1}");
				sb.AppendLine(new string('=', 50));
				sb.AppendLine($"Probe start position: " + sc.ProbePosition.Peek().ToString());

				foreach (MachineCommand cmd in sc.MachineCommands)
				{
					if (cmd == MachineCommand.MeasurementOn || cmd == MachineCommand.MeasurementOff)
					{
						sc.MeasurementModeOn = cmd == MachineCommand.MeasurementOn;
						continue;
					}

					Vector nextProbePosition = sc.ProbePosition.Peek();
					nextProbePosition.Translate(GetCommandTranslationVector(cmd));

					if (nextProbePosition.X < 0 || nextProbePosition.X > sc.MachineSize.X || nextProbePosition.Y < 0 || nextProbePosition.Y > sc.MachineSize.Y)
					{
						sb.AppendLine("ERROR - command directs the probe out of the machine table bounds");
						break;
					}

					var isObjectPosition = sc.ObjectPositions.Where(p => p.X == nextProbePosition.X && p.Y == nextProbePosition.Y).ToList().Count > 0;

					if (isObjectPosition)
					{
						sb.AppendLine($"Collide probe on {nextProbePosition.ToString()}");

						if (sc.MeasurementModeOn)
						{
							measurements.Add(nextProbePosition.ToString());
						}
						else
						{
							sb.AppendLine("WARNING - measurement mode turned off during object collision");
						}
					}
					else
					{
						sb.AppendLine($"Move probe {cmd.ToString().ToLower()} to {nextProbePosition.ToString()}");
						sc.ProbePosition.Push(nextProbePosition);
					}
				}

				sb.AppendLine("--- FINISHED ---").AppendLine();

				string measurementsReport = "WARNING - no measurements were collected during simulation";

				if (measurements.Count > 0)
					measurementsReport = "Measurements output: " + string.Join(", ", measurements);

				sb.AppendLine(measurementsReport);
				sb.AppendLine("Probe end position: " + sc.ProbePosition.Peek().ToString());

				yield return sb.ToString();
			}
		}

		private Vector GetCommandTranslationVector(MachineCommand cmd)
		{
			switch (cmd)
			{
				case MachineCommand.Left:
					return new Vector(-1, 0);
				case MachineCommand.Right:
					return new Vector(1, 0);
				case MachineCommand.Up:
					return new Vector(0, 1);
				case MachineCommand.Down:
					return new Vector(0, -1);
				case MachineCommand.MeasurementOn:
				case MachineCommand.MeasurementOff:
				default:
					return new Vector(0, 0);
			}
		}
	}
}
