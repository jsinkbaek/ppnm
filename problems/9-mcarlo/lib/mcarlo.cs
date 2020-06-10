using System;
using static System.Math;

public class montecarlo
{
	private static Random rnd = new Random();
	
	public static (double, double) plain(
			Func<vector, double> f,	// Function to integrate
			vector a,		// Starting point
			vector b,		// End point
			int N=10000		// Number of points
			)
	{
		int dim = a.size;	// Amount of function variables
		
		// Calculate Mone-Carlo integration volume
		double volume = 1;
		for (int i = 0; i<dim; i++) volume *= (b[i] - a[i]);
		
		// Sum and Sum²
		double sum = 0;
		double sum2 = 0;

		// Calculate quad sum for N rand points x. Sum² is for error estimate
		vector x = new vector(dim);
		for (int k=0; k<N; k++)
		{
			// Generate random point
			for (int i=0; i<dim; i++) x[i] = a[i] + rnd.NextDouble()*(b[i]-a[i]);

			// Sum
			double fx = f(x);
			sum += fx;
			sum2 += fx*fx;
		}
		double mean = sum/N;
		double variance = Sqrt(sum2/N - sum/N * sum/N);
		double integral = mean * volume;
		double error = variance * volume / Sqrt(N);
		return (integral, error);
	}

}
