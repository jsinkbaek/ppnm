using System;
using static System.Math;
using static System.Console;
using System.Collections;
using System.Collections.Generic;

class main
{
	static void Main()
	{
		WriteLine("Problem 5A. See PlotA.svg for solution.");
		WriteLine("Feedback form ODE integrators will be included in out.err.txt");
		WriteLine("See PlotAdevi.svg for variation of solutions from Sin(x)");

		// Set up differential equation u'' = -u
		Func<double, vector, vector> eq = delegate(double x, vector y)
		{
			return new vector (y[1], -y[0]);
		};

		// Starting point
		double a = 0;
		vector ya = new vector(0, 1);

		// Endpoint
		double b = 3 * PI;

		// Call solver rk12
		(List<double> xs12, List<vector> ys12) = ode.rk12(eq, a, ya, b);

		// Call solver rk12 with slacking tolerances as integration proceeds
		(List<double> xs12_slac, List<vector> ys12_slac) = ode.rk12(eq, a, ya, b, slack:true);

		// Call solver rk45
		(List<double> xs45, List<vector> ys45) = ode.rk45(eq, a, ya, b);
		
		// Call solver rk45 with slacking tolerances as integration proceeds
		(List<double> xs45_slac, List<vector> ys45_slac) = ode.rk45(eq, a, ya, b);
		
		// Write rk12 integration
		var writer12 = new System.IO.StreamWriter("out.rk12.txt");
		for (int i=0; i<xs12.Count; i++)
		{
			writer12.WriteLine($"{xs12[i]:f6} {ys12[i][0]:f6} {ys12[i][1]:f6}");
		}
		writer12.Close();

		// Write rk45 integration
		var writer45 = new System.IO.StreamWriter("out.rk45.txt");
		for (int i=0; i<xs45.Count; i++)
		{
			writer45.WriteLine($"{xs45[i]:f6} {ys45[i][0]:f6} {ys45[i][1]:f6}");
		}
		writer45.Close();

		// Write deviation from Sin(x)
		var deviw = new System.IO.StreamWriter("out.deviation.txt");
		for (int i=0; i<xs12.Count; i++)
		{
			deviw.WriteLine($"{xs12[i]:f6} {Abs(ys12[i][0]-Sin(xs12[i])):f12}");
		}
		deviw.WriteLine("\n");
		for (int i=0; i<xs12_slac.Count; i++)
		{
			deviw.WriteLine($"{xs12_slac[i]:f6} {Abs(ys12_slac[i][0]-Sin(xs12_slac[i])):f12}");
		}
		deviw.WriteLine("\n");
		for (int i=0; i<xs45.Count; i++)
		{
			deviw.WriteLine($"{xs45[i]:f6} {Abs(ys45[i][0]-Sin(xs45[i])):f12}");
		}
		deviw.WriteLine("\n");
		for (int i=0; i<xs45_slac.Count; i++)
		{
			deviw.WriteLine($"{xs45_slac[i]:f6} {Abs(ys45_slac[i][0]-Sin(xs45_slac[i])):f12}");
		}
		deviw.Close();
	}
}
