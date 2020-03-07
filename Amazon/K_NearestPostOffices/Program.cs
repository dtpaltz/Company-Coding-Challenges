using System;
using System.Collections.Generic;
using System.Linq;

/*
K Nearest Post Offices
Find the k post offices located closest to you, given your location and a list of locations of all post offices available.
Locations are given in 2D coordinates in [X, Y], where X and Y are integers.
Euclidean distance is applied to find the distance between you and a post office.
Assume your location is [m, n] and the location of a post office is [p, q], the Euclidean distance between the
office and you is SquareRoot((m - p) * (m - p) + (n - q) * (n - q)).
K is a positive integer much smaller than the given number of post offices. from aonecode.com

e.g.
Input
you: [0, 0]
post_offices: [[-16, 5], [-1, 2], [4, 3], [10, -2], [0, 3], [-5, -9]]
k = 3

Output from aonecode.com
[[-1, 2], [0, 3], [4, 3]]
*/


namespace K_NearestPostOffices
{
	class Program
	{
		static void Main(string[] args)
		{
			var center = new int[] { 0, 0 };
			var pos = new int[][] { new int[] { -16, 5 }, new int[] { -1, 2 }, new int[] { 4, 3 }, new int[] { 10, -2 }, new int[] { 0, 3 }, new int[] { -5, -9 } };
			int k = 3;

			var results = SelectCloasestPostOffices(center, pos, k);

			foreach (var item in results)
			{
				Console.WriteLine($"[{item[0]}, {item[1]}]");
			}

			Console.ReadLine();
		}

		private static List<int[]> SelectCloasestPostOffices(int[] center, int[][] pos, int k)
		{
			var results = new List<KeyValuePair<int[], double>>();
			for (int j = 0; j < pos.Length; j++)
			{
				results.Add(new KeyValuePair<int[], double>(pos[j], EuclideanDist(center, pos[j])));
			}

			results.Sort((x, y) => x.Value.CompareTo(y.Value));
			return results.GetRange(0, k).Select(x => x.Key).ToList();
		}

		private static double EuclideanDist(int[] p1, int[] p2)
		{
			int m = p1[0];
			int n = p1[1];
			int p = p2[0];
			int q = p2[1];

			return Math.Sqrt((m - p) * (m - p) + (n - q) * (n - q));
		}
	}
}
