using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimulationExercise
{
	public class CommandValidator
	{
		private List<SimConfiguration> m_Configurations;

		public CommandValidator(SimConfiguration config) : this(new List<SimConfiguration>() { config })
		{
		}

		public CommandValidator(List<SimConfiguration> configs)
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
					nextProbePosition.Translate(GetCommandMoveVector(cmd));

					if (nextProbePosition.X < 0 || nextProbePosition.X > sc.MachineSize.X || nextProbePosition.Y < 0 || nextProbePosition.Y > sc.MachineSize.Y)
					{
						sb.AppendLine("ERROR - invalid command directs the probe out of the machine tabele bounds");
						break;
					}

					var isObjectPosition = sc.ObjectPositions.Where(p => p.X == nextProbePosition.Y && p.Y == nextProbePosition.Y).ToList().Count > 0;

					if (isObjectPosition)
					{
						if (sc.MeasurementModeOn)
							measurements.Add(nextProbePosition.ToString());
						else
							sb.AppendLine("WARNING - measurement mode turned off during object collision");
					}
					else
					{
						sc.ProbePosition.Push(nextProbePosition);
					}
				}

				string measurementsReport = "WARNING - no measurements were collected during simulation";

				if (measurements.Count > 0)
					measurementsReport = "Measurments output: " + string.Join(", ", measurements);

				sb.AppendLine(measurementsReport);
				sb.AppendLine("Probe end position: " + sc.ProbePosition.Peek().ToString());

				yield return sb.ToString();
			}
		}

		private Vector GetCommandMoveVector(MachineCommand cmd)
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
