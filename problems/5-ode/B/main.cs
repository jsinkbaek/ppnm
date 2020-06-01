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


		// Third model - 60 days unhindered, 60 days harshly limited, 480 days semi-limited
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


		// Fourth model - 60 days unhindered, 60 days harshly limited increase stepwise to unhindered
		WriteLine("\nFourth model uses unhindered 60d, 2.5 infections pr infected.");
		WriteLine("Followed by smooth transition to 60d harshly limited, 0.6 pr infected.");
		WriteLine("Followed by smoot transition to 60d 1 per infected, and then periodical 0.1 increase every 30d until 2.4.");
		WriteLine("See plot PlotB3.svg for it.");
		var sirw3 = new System.IO.StreamWriter("out.B3.txt");
		// Startpoints
		double a0=0, a01=40, a02=50, a03=55, a1=60, a04=65, a05=70, a06=80, a2=120, a3=150, a4=180, a5=210, 
		       a6=240, a7=270, a8=300, a9=330, a10=360;
		double a11=390, a12=420, a13=450, a14=480, a15=510, a16=540, a17=570, a18=600;
		vector av = new vector(new double[] {a0,a01,a02,a03,a1,a04,a05,a06,a2,a3,a4,a5,a6,a7,a8,a9,a10,a11,
				a12,a13,a14,a15,a16,a17,a18});
		// Tc's
		double tc0=Tr/2.5, tc01=Tr/2.0, tc02=Tr/1.5, tc03=Tr/1.2, tc1=Tr/1.0, tc04=Tr/0.8, tc05=Tr/0.65, 
		       tc06=Tr/0.6, tc2=Tr/1.0, tc3=Tr/1.0, tc4=Tr/1.1, tc5=Tr/1.2, tc6=Tr/1.3;
		double tc7=Tr/1.4, tc8=Tr/1.5, tc9=Tr/1.6, tc10=Tr/1.7, tc11=Tr/1.8, tc12=Tr/1.9, tc13=Tr/2.0;
		double tc14=Tr/2.1, tc15=Tr/2.2, tc16=Tr/2.3, tc17=Tr/2.4;
		vector tcs = new vector(new double[] {tc0,tc01,tc02,tc03,tc1,tc04,tc05,tc06,tc2,tc3,tc4,tc5,tc6,tc7,
				tc8,tc9,tc10,tc11,tc12,tc13,tc14,tc15,tc16,tc17});
		// Create lists to store data
		List<double> xs4 = new List<double>();
		List<vector> ys4 = new List<vector>();
		List<double> x4 = new List<double>(); // temp list
		List<vector> y4 = new List<vector>(); // temp list
		vector ya4 = new vector(3);
		for (int i=0; i<tcs.size; i++)
			{
				Func<double, vector, vector> eq = makeSIReq(N, tcs[i], Tr);
				if (i==0)
				{
					ya4[0] = N;
					ya4[1] = 500;
					ya4[2] = 0;
				}
				else 
				{
					ya4[0] = y4[y4.Count-1][0];
					ya4[1] = y4[y4.Count-1][1];
					ya4[2] = y4[y4.Count-1][2];
				}
				(x4, y4) = ode.rk45(eq, av[i], ya4, av[i+1], xlist:x4, ylist:y4);
				for (int j=0; j<x4.Count; j++)
				{
					sirw3.WriteLine($"{x4[j]:f8} {y4[j][0]:f8} {y4[j][1]:f8} {y4[j][2]:f8}");
				}
			}
		sirw3.Close();

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
