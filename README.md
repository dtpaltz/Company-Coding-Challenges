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

# Sample Interaction & Output

Machine Size: 7 7
Probe Starting Position: 2 4
Object Positions: 0 2, 1 2, 2 2, 3 2, 4 1
Machine Commands: DRODFRODDFR

>>>>>>>> Command Test Simulation #1
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
