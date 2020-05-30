using System;
using static System.Console;
using static System.Math;
using System.Collections;
using System.Collections.Generic;

class main
{
	static void Main()
	{
		WriteLine("Problem 5B - SIR epidemic model");

		// Recovery time Tr seems to be about 14 days
		double Tr = 14;
		// Population of DK about 5.8 mill
		double N = 5.8e6;
		// Infection factor, we try with 2.5 infections per infected person first
		double ifact0 = 2.5;
		// Typical amount of days per contact per infected
		double Tc0 = Tr/ifact0;
		// Set up differential equations
		Func<double, vector, vector> SIReq0 = makeSIReq(N, Tc0, Tr);

		WriteLine("\nFirst model uses unhindered infection, 2.5 per infected, for two months");
		WriteLine("See plots in PlotB0.svg");
		
		// Starting point, assume everyone susceptible, assume about 500 infected
		double a = 0;
		vector ya = new vector(N, 500, 0);

		// Endpoint after 600 days
		double b = 600;

		// Call ODE integrator to solve system
		(List<double> xs0, List<vector> ys0) = ode.rk45(SIReq0, a, ya, b);

		// Write to file
		var sirw0 = new System.IO.StreamWriter("out.B0.txt");
		for (int i=0; i<xs0.Count; i++)
		{
			sirw0.WriteLine($"{xs0[i]:f8} {ys0[i][0]:f8} {ys0[i][1]:f8} {ys0[i][2]:f8}");
		}
		sirw0.Close();


		// New model - 60 days of unhindered spread, followed by 540 days of harshly limited spread
		WriteLine("\nSecond model uses unhindered infection for 60 days.");
		WriteLine("This is followed by 540 days of harshly limited infection, 0.6 per infected.");
		WriteLine("See plots in PlotB1.svg");
		// Endpoints
		b = 60;
		double c = 600;
		// Calculate first part
		(List<double> xs10, List<vector> ys10) = ode.rk45(SIReq0, a, ya, b);
		// New infection factor and Tc
		double ifact1 = 0.6;
		double Tc1 = Tr/ifact1;
		// Find starting conditions from end of first part
		double S1 = ys10[ys10.Count-1][0]; 
		double I1 = ys10[ys10.Count-1][1]; 
		double R1 = ys10[ys10.Count-1][2];
		vector yb = new vector(S1, I1, R1);
		// Set up differential equation
		Func<double, vector, vector> SIReq1 = makeSIReq(N, Tc1, Tr);
		// Call ODE integrator
		(List<double> xs11, List<vector> ys11) = ode.rk45(SIReq1, b, yb, c);
		// Write results
		var sirw1 = new System.IO.StreamWriter("out.B1.txt");
		var sirw2 = new System.IO.StreamWriter("out.B2.txt");
		for (int i=0; i<xs10.Count; i++)
		{
			sirw1.WriteLine($"{xs10[i]:f8} {ys10[i][0]:f8} {ys10[i][1]:f8} {ys10[i][2]:f8}");
			sirw2.WriteLine($"{xs10[i]:f8} {ys10[i][0]:f8} {ys10[i][1]:f8} {ys10[i][2]:f8}");
		}
		for (int i=0; i<xs11.Count; i++)
		{
			sirw1.WriteLine($"{xs11[i]:f8} {ys11[i][0]:f8} {ys11[i][1]:f8} {ys11[i][2]:f8}");
		}
		sirw1.Close();


		// Last model - 60 days unhindered, 60 days harshly limited, 480 days semi-limited
		WriteLine("\nThird model uses unhindered infection for 60 days, 2.5 infections per infected.");
		WriteLine("This is followed by 60 days of harshly limited infection, 0.6 per infected.");
		WriteLine("This is followed by 480 days of semi-limited infection, 1.3 per infected.");
		WriteLine("See plots in PlotB2.svg");
		// Endpoint
		c = 120;
		double d = 600;
		// Calculate second part
		(List<double> xs20, List<vector> ys20) = ode.rk45(SIReq1, b, yb, c);
		// New infection factor and Tc
		double ifact2 = 1.3;
		double Tc2 = Tr/ifact2;
		// Find starting conditions from end of model 2
		double S2 = ys20[ys20.Count-1][0];
		double I2 = ys20[ys20.Count-1][1];
		double R2 = ys20[ys20.Count-1][2];
		vector yc = new vector(S2, I2, R2);
		// Set up differential equations
		Func<double, vector, vector> SIReq2 = makeSIReq(N, Tc2, Tr);
		// Call ODE integrator
		(List<double> xs2, List<vector> ys2) = ode.rk45(SIReq2, c, yc, d);
		// Write results
		for (int i=0; i<ys20.Count; i++)
		{
			sirw2.WriteLine($"{xs20[i]:f8} {ys20[i][0]:f8} {ys20[i][1]:f8} {ys20[i][2]:f8}");
		}
		for (int i=0; i<xs2.Count; i++)
		{
			sirw2.WriteLine($"{xs2[i]:f8} {ys2[i][0]:f8} {ys2[i][1]:f8} {ys2[i][2]:f8}");
		}
		sirw2.Close();
	}


	public static Func<double, vector, vector> makeSIReq(double N, double Tc, double Tr)
	{	
		Func<double, vector, vector> eq = delegate(double x, vector y)
		{
			return new vector(-y[1]*y[0]/(N*Tc), 
					  y[1]*y[0]/(N*Tc) - y[1]/Tr, 
					  y[1]/Tr);
		};
		return eq;
	}
}
