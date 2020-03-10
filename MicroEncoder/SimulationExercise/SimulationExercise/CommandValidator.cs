using System.Collections.Generic;
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
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < m_Configurations.Count; i++)
			{
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
					
					// if nextProbePosition is off the table = ERROR
					
					// if nextProbePosition is an object position...
						// if measure mode is ON, take measurement
						// else WARNING measrement mode is not ON
					// else push probe position and continue
				}
				
				string measurementsReport = "WARNING - no measurements were collected during simulation";
				
				if (measurements.Count() > 0)
					measurementsReport = "Measurments output: " + String.Join(", ", measurements);
				
				sb.AppendLine(measurementsReport);
				sb.AppendLine("Probe end position: " + sc.ProbePosition.Peek().ToString());

				yield return sb.ToString();
				sb.Clear();
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
