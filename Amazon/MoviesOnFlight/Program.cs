using System;
using System.Collections.Generic;
using System.Linq;

/*
You are on a flight and wanna watch two movies during this flight.
You are given int[] movie_duration which includes all the movie durations.
You are also given the duration of the flight which is d in minutes.
Now, you need to pick two movies and the total duration of the two movies is less than or equal to (d - 30min).
Find the pair of movies with the longest total duration. If multiple found, return the pair with the longest movie.

e.g.
Input
movie_duration: [90, 85, 75, 60, 120, 150, 125]
d: 250

Output from aonecode.com
[90, 125]
90min + 125min = 215 is the maximum number within 220 (250min - 30min)
*/

namespace MoviesOnFlight
{
	class Program
	{
		static void Main(string[] args)
		{
			var movie_duration = new int[] { 90, 85, 75, 60, 120, 150, 125 };
			int d = 250;
			var moviePair = SelectMovies(movie_duration, d);

			foreach (var item in moviePair)
			{
				Console.WriteLine($"[{item[0]}, {item[1]}]  ==>  {item[0]}min + {item[1]}min = {item[0] + item[1]}");
			}

			Console.ReadLine();
		}

		private static List<int[]> SelectMovies(int[] movieTimes, int flightTime)
		{
			int targetDuration = flightTime - 30;
			var movieSelections = new List<int[]>();
			int bestDuration = 0;

			for (int j = 0; j < movieTimes.Length; j++)
			{
				for (int k = 0; k < movieTimes.Length; k++)
				{
					if (j != k)
					{
						int mJ = movieTimes[j];
						int mK = movieTimes[k];
						int thisDuration = mJ + mK;

						if (thisDuration <= targetDuration)
						{
							var mPair = new int[] { mJ, mK };
							var mReversePair = new int[] { mK, mJ };

							bool isUniquePair = true;

							foreach (var item in movieSelections)
							{
								if (item.SequenceEqual(mReversePair))
								{
									isUniquePair = false;
									break;
								}
							}

							if (isUniquePair)
							{
								if (thisDuration > bestDuration)
								{
									movieSelections = new List<int[]>() { mPair };
									bestDuration = thisDuration;
								}
								else if (thisDuration == bestDuration)
								{
									movieSelections.Add(mPair);
								}
							}
						}
					}
				}
			}

			return movieSelections;
		}
	}
}
