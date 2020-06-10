using System;
using static System.Console;
using static System.Math;
using System.Collections.Generic;

class main
{
	static void Main()
	{
		WriteLine("\n--------------------------------------------------------------");
                WriteLine("\nProblem 9B:\n");
		WriteLine("See PlotB.svg for results");
		// Check that estimated error follows O(1/sqrt(N))

		// Integrate [0, 3.0] 3D rectangular volume many times with different N
		Func<vector, double> rectvol = (x) => x[0]*x[0] + x[1]*x[1] + x[2]*x[2];
		// Limits
		vector a = new vector(0.0, 0.0, 0.0);
		vector b = new vector(3.0, 3.0, 3.0);
		// Expected value
		double expected = 243;
		// N limits
		int Nmin = 5000; int Nmax = 300000;
		// O(1/sqrt(N)) scaling
		(double scale_intg, double scale_error) = montecarlo.plain(rectvol, a, b, Nmin);
		double scale = scale_error * Sqrt(Nmin);
		// Loop amount for avg value and mean
		int nloops = 9; List<double> integrals = new List<double>();
		// Commence loops
		var writer = new System.IO.StreamWriter("out.data.txt");
		for (int N=Nmin; N<Nmax; N+=5000)
		{
			double integral_median = 0;
			double integral_avg = 0; double error_avg = 0;
			integrals.Clear();

			// Get avg
			for (int i=0; i<nloops; i++)
			{
				(double integral, double error) = montecarlo.plain(rectvol, a, b, N);
				integral_avg += integral;
				integrals.Add(integral);
				error_avg += error;
			}
			// Average
			integral_avg /= nloops;
			error_avg /= nloops;
			// Median
			integrals.Sort();
			if (integrals.Count % 2 == 0)
			{// Count is even, average middle elements
				integral_median = integrals[integrals.Count/2-1];
				integral_median += integrals[integrals.Count/2+1];
				integral_median /= 2;
			}
			else
			{
				integral_median = integrals[integrals.Count/2];
			}

			// Write result
			writer.WriteLine($"{N} {error_avg} {Abs(expected-integral_avg)} {scale/Sqrt(N)} " + 
					$"{Abs(expected - integral_median)}");
		}
		writer.Close();


		WriteLine("\n--------------------------------------------------------------\n");
	}
}
