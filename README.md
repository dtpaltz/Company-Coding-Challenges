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

4) The next position of the probe is calculate by converting the move direction to a translation vector, then added to the current probe position. Note: the probes position is stored as a Stack to allow for easy back-tracking.

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

# Test Coverage

Unit tests include assertion of operation:
Move probe left
Move probe up
Move probe right
Move probe down
Simulation reports collision with objects
Probe can be positioned anywhere on the machine table
Simulation reports expected measurements taken
Simulation reports expected probe end position
Simulation reports warning when measurement mode is OFF during probe collision with an object
Simulation reports warning when no measurements were taken during the simulation
Simulation reports error when the the probe is commanded out of bounds of the machine table

# Comments

For this exercise I considered altering the input prompt to instead take the input as args in the Main method, but that seemed less user-friendly. I also considered creating some UI using WPF on top of the simulation, but thought wiring-up a UI and still performing validation would take me down a rabbit hole for a small project and waste too much time. I decided to utilze the console8 prompt for input and validation, but encapsulated the user input into a SimConfiguration object that is passed to a CommandValidationEngine which puts the commands through a simutated execution. The design of the CommandValidationEngine is such that this program can be easily extended to pass a collection of SimConfiguration instances to it and run them all through the simulation sequentially at once.
