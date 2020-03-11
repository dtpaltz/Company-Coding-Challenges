# SimulationExercise

Console application to be used for validating machine probe movement commands in a simulated environment to varify the end result is expected.

# Development Approach

Utilizing the partial solution provided the major changes to the initial code provided:

1) Encapsulated the user input configuration values into a seperate SimConfiguration object created to be passed to the CommandValidationEngine I created to simulate the commands.

2) Moved the user input parser methods to a static class to make them accessible to the SimulationExerciseTest project.

3) Added a Translate(Vector v) method to the Vector class

4) Overrode the ToString() method in the Vector class

5) Changed the X & Y properties in the Vector class to be privately settable

# Assumptions Made

1) The user will input configuration values in the proper format (ex. vectors are parsed as 'X1 Y1, X2 Y2, XN YN')

2) The user will input non-negative configuration values

3) Input configuration vector values will be NOT be outside the user-defined bounds of the machine table size

# Simulation Process

1) When CommandValidationEngine.TestCommands() is called, the simulation engine iterates through all the command configurations it has received.

2) With each configuration the simulation will iterate through the commands for that configuration

3) If the command is a MeasurementModeOn/MeasurementModeOff direcrtive, then the mode is set and the iteration jumps to the next command

4) The next position of the probe is calculate by converting the move direction to a translation vector, then added to the current probe position. Note: the probes position is stored as a Stack to allow for back-tracking.

5) If the probes next position is outside the bounds of the table, then that is an error condition and the simulation ends.

6) If the probes next position hosts an object, then take a measurement if the measurement mode is ON and then return to the probes previous position. Otherwise, push the probes current position to be that next position.

7) END command iteration.

8) Report the results of the simulation.

# Sample Interaction & Output

Machine Size: 7 7
Probe Starting Position: 2 4
Object Positions: 0 2, 1 2, 2 2, 3 2, 4 1
Machine Commands: DRODFRODDFR
==================================================
Command Test Simulation #1
==================================================
Probe start position: [2, 4]
Move probe down to [2, 3]
Move probe right to [3, 3]
Collide probe on [3, 2]
Move probe right to [4, 3]
Move probe down to [4, 2]
Collide probe on [4, 1]
Move probe right to [5, 2]
--- FINISHED ---

Measurements output: [3, 2], [4, 1]
Probe end position: [5, 2]
