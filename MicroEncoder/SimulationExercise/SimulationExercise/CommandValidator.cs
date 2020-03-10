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
				sb.AppendLine(new string('=', 50));
				sb.AppendLine($"Command Test Simulation #{i + 1}");
				sb.AppendLine(new string('=', 50));
				sb.AppendLine($"Probe start position: " + sc.ProbeStartPosition.ToString());

				Vector probeCurrentPosition = sc.ProbeStartPosition;

				foreach (MachineCommand cmd in sc.MachineCommands)
				{
					// todo
				}

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
					return new Vector(0, -1);
				case MachineCommand.Down:
					return new Vector(0, 1);
				case MachineCommand.MeasurementOn:
				case MachineCommand.MeasurementOff:
				default:
					return new Vector(0, 0);
			}
		}
	}
}