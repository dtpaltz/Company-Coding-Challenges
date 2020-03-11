using NUnit.Framework;
using SimulationExercise;
using System.Collections.Generic;

namespace SimulationExerciseTest
{
	public class Tests
	{
		[Test]
		public void ReportProbeMovesLeft_WhenDirected_DuringSimulation()
		{
			SimConfiguration config = new SimConfiguration
			{
				MachineSize = InputParseHelpers.TryPosition("7 7").Value,
				ProbePosition = new Stack<Vector>(new[] { InputParseHelpers.TryPosition("2 4").Value }),
				ObjectPositions = InputParseHelpers.TryPositions("0 0").Value,
				MachineCommands = InputParseHelpers.TryMachineCommands("L").Value
			};

			var cv = new CommandValidationEngine(config);
			foreach (string testResults in cv.TestCommands())
			{
				Assert.IsTrue(testResults.Contains("Move probe left to [1, 4]"));
			}
		}

		[Test]
		public void ReportProbeMovesUp_WhenDirected_DuringSimulation()
		{
			SimConfiguration config = new SimConfiguration
			{
				MachineSize = InputParseHelpers.TryPosition("7 7").Value,
				ProbePosition = new Stack<Vector>(new[] { InputParseHelpers.TryPosition("2 4").Value }),
				ObjectPositions = InputParseHelpers.TryPositions("0 0").Value,
				MachineCommands = InputParseHelpers.TryMachineCommands("U").Value
			};

			var cv = new CommandValidationEngine(config);
			foreach (string testResults in cv.TestCommands())
			{
				Assert.IsTrue(testResults.Contains("Move probe up to [2, 5]"));
			}
		}

		[Test]
		public void ReportProbeMovesRight_WhenDirected_DuringSimulation()
		{
			SimConfiguration config = new SimConfiguration
			{
				MachineSize = InputParseHelpers.TryPosition("7 7").Value,
				ProbePosition = new Stack<Vector>(new[] { InputParseHelpers.TryPosition("2 4").Value }),
				ObjectPositions = InputParseHelpers.TryPositions("0 0").Value,
				MachineCommands = InputParseHelpers.TryMachineCommands("R").Value
			};

			var cv = new CommandValidationEngine(config);
			foreach (string testResults in cv.TestCommands())
			{
				Assert.IsTrue(testResults.Contains("Move probe right to [3, 4]"));
			}
		}

		[Test]
		public void ReportProbeMovesDown_WhenDirected_DuringSimulation()
		{
			SimConfiguration config = new SimConfiguration
			{
				MachineSize = InputParseHelpers.TryPosition("7 7").Value,
				ProbePosition = new Stack<Vector>(new[] { InputParseHelpers.TryPosition("2 4").Value }),
				ObjectPositions = InputParseHelpers.TryPositions("0 0").Value,
				MachineCommands = InputParseHelpers.TryMachineCommands("D").Value
			};

			var cv = new CommandValidationEngine(config);
			foreach (string testResults in cv.TestCommands())
			{
				Assert.IsTrue(testResults.Contains("Move probe down to [2, 3]"));
			}
		}

		[Test]
		public void ReportCollision_WhenProbeCollidesWithObject_DuringSimulation()
		{
			SimConfiguration config = new SimConfiguration
			{
				MachineSize = InputParseHelpers.TryPosition("7 7").Value,
				ProbePosition = new Stack<Vector>(new[] { InputParseHelpers.TryPosition("2 4").Value }),
				ObjectPositions = InputParseHelpers.TryPositions("0 2, 1 2, 2 2, 3 2, 4 1").Value,
				MachineCommands = InputParseHelpers.TryMachineCommands("DROD").Value
			};

			var cv = new CommandValidationEngine(config);
			foreach (string testResults in cv.TestCommands())
			{
				Assert.IsTrue(testResults.Contains("Collide probe on [3, 2]"));
			}
		}

		[Test]
		public void ProbeCanMove_OverEntireBoard_DuringSimulation()
		{
			SimConfiguration config = new SimConfiguration
			{
				MachineSize = InputParseHelpers.TryPosition("1 1").Value,
				ProbePosition = new Stack<Vector>(new[] { InputParseHelpers.TryPosition("0 0").Value }),
				ObjectPositions = InputParseHelpers.TryPositions("").Value,
				MachineCommands = InputParseHelpers.TryMachineCommands("RULD").Value
			};

			var cv = new CommandValidationEngine(config);
			foreach (string testResults in cv.TestCommands())
			{
				Assert.IsFalse(testResults.Contains("ERROR - command directs the probe out of the machine table bounds"));
			}
		}

		[Test]
		public void ReportCorrectMeasurements_WhenInputValid_DuringSimulation()
		{
			SimConfiguration config = new SimConfiguration
			{
				MachineSize = InputParseHelpers.TryPosition("7 7").Value,
				ProbePosition = new Stack<Vector>(new[] { InputParseHelpers.TryPosition("2 4").Value }),
				ObjectPositions = InputParseHelpers.TryPositions("0 2, 1 2, 2 2, 3 2, 4 1").Value,
				MachineCommands = InputParseHelpers.TryMachineCommands("DRODFRODDFR").Value
			};

			var cv = new CommandValidationEngine(config);
			foreach (string testResults in cv.TestCommands())
			{
				Assert.IsTrue(testResults.Contains("Measurements output: [3, 2], [4, 1]"));
			}
		}

		[Test]
		public void ReportCorrectEndProbePosition_WhenInputValid_DuringSimulation()
		{
			SimConfiguration config = new SimConfiguration
			{
				MachineSize = InputParseHelpers.TryPosition("7 7").Value,
				ProbePosition = new Stack<Vector>(new[] { InputParseHelpers.TryPosition("2 4").Value }),
				ObjectPositions = InputParseHelpers.TryPositions("0 2, 1 2, 2 2, 3 2, 4 1").Value,
				MachineCommands = InputParseHelpers.TryMachineCommands("DRODFRODDFR").Value
			};

			var cv = new CommandValidationEngine(config);
			foreach (string testResults in cv.TestCommands())
			{
				Assert.IsTrue(testResults.Contains("Probe end position: [5, 2]"));
			}
		}

		[Test]
		public void ReportWarning_WhenMeasurementModeOff_DuringObjectCollision()
		{
			SimConfiguration config = new SimConfiguration
			{
				MachineSize = InputParseHelpers.TryPosition("7 7").Value,
				ProbePosition = new Stack<Vector>(new[] { InputParseHelpers.TryPosition("2 4").Value }),
				ObjectPositions = InputParseHelpers.TryPositions("0 2, 1 2, 2 2, 3 2, 4 1").Value,
				MachineCommands = InputParseHelpers.TryMachineCommands("DRD").Value
			};

			var cv = new CommandValidationEngine(config);
			foreach (string testResults in cv.TestCommands())
			{
				Assert.IsTrue(testResults.Contains("WARNING - measurement mode turned off during object collision"));
			}
		}

		[Test]
		public void ReportWarning_WhenNoMeasurementsTaken_DuringSimulation()
		{
			SimConfiguration config = new SimConfiguration
			{
				MachineSize = InputParseHelpers.TryPosition("7 7").Value,
				ProbePosition = new Stack<Vector>(new[] { InputParseHelpers.TryPosition("2 4").Value }),
				ObjectPositions = InputParseHelpers.TryPositions("0 2, 1 2, 2 2, 3 2, 4 1").Value,
				MachineCommands = InputParseHelpers.TryMachineCommands("DRDFRDDFR").Value
			};

			var cv = new CommandValidationEngine(config);
			foreach (string testResults in cv.TestCommands())
			{
				Assert.IsTrue(testResults.Contains("WARNING - no measurements were collected during simulation"));
			}
		}

		[Test]
		public void ReportError_WhenProbeOutOfBounds_DuringSimulation()
		{
			SimConfiguration config = new SimConfiguration
			{
				MachineSize = InputParseHelpers.TryPosition("7 5").Value,
				ProbePosition = new Stack<Vector>(new[] { InputParseHelpers.TryPosition("2 4").Value }),
				ObjectPositions = InputParseHelpers.TryPositions("0 0").Value,
				MachineCommands = InputParseHelpers.TryMachineCommands("UUUUUUU").Value
			};

			var cv = new CommandValidationEngine(config);
			foreach (string testResults in cv.TestCommands())
			{
				Assert.IsTrue(testResults.Contains("ERROR - command directs the probe out of the machine table bounds"));
			}
		}
	}
}