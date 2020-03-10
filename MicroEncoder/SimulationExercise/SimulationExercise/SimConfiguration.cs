using System;
using System.Collections.Generic;
using System.Text;

namespace SimulationExercise
{
	public class SimConfiguration
	{
		public Vector MachineSize { get; set; }

		public Vector ProbeStartPosition { get; set; }

		public List<Vector> ObjectPositions { get; set; }

		public List<MachineCommand> MachineCommands { get; set; }
	}
}
