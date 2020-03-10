namespace SimulationExercise
{
	public struct Vector
	{
		public Vector(int x, int y)
		{
			X = x;
			Y = y;
		}

		public int X { get; private set; }

		public int Y { get; private set; }

		public void Translate(Vector v)
		{
			X += v.X;
			Y += v.Y;
		}

		public override string ToString()
		{
			return $"[{X}, {Y}]";
		}
	}
}