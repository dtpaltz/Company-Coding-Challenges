using System;
using System.Collections.Generic;
using System.Text;

namespace SimulationExercise
{
	public class SimConfiguration
	{
		public Vector MachineSize { get; set; }

		public Stack<Vector> ProbePosition { get; set; }
		
		public bool MeasurementModeOn { get; set; } = false;

		public List<Vector> ObjectPositions { get; set; }

		public List<MachineCommand> MachineCommands { get; set; }
	}
}
