using System;
using static System.Console;
using static System.Math;

class main
{
	static void Main()
	{
		WriteLine("\n--------------------------------------------------------------");
                WriteLine("\nProblem 9A:\n");
		
		// Integrate [0, PI*2] Sin(x)² = PI
		Func<vector, double> sin2 = (x) => Sin(x[0]) * Sin(x[0]);
		// Limits
		vector a = new vector(0.0);
		vector b = new vector(PI*2);
		// Number of points
		int N = 10000;
		// Call montecarlo.plain()
		double expected = PI;
		(double integral, double error) = montecarlo.plain(sin2, a, b, N);
		// Write results
		WriteLine($"Calculating monte-carlo integral [0, 2*PI] of Sin(x)²");
		WriteLine($"Starting point:				{a[0]:f6}");	
                WriteLine($"End point:				{b[0]:f6}");
		WriteLine($"Expected result:	         	{expected:f6}");
                WriteLine($"Found result:	              		{integral:f6}");
                WriteLine($"Estimated error:	 	 	{error:f6}");
		WriteLine($"Deviation from expected:		{Abs(expected-integral):f6}");
		WriteLine($"Number of sample points used:		{N}");

		
		// Integrate [0, 3.0] 3D rectangular volume function
		Func<vector, double> rectvol = (x) => x[0]*x[0] + x[1]*x[1] + x[2]*x[2];
		// Limits
		a = new vector(0.0, 0.0, 0.0);
		b = new vector(3.0, 3.0, 3.0);
		// Number of points
		N = 10000;
		// Call montecarlo.plain()
		expected = 243;
		(integral, error) = montecarlo.plain(rectvol, a, b, N);
		// Write results
		WriteLine($"\n\nCalculating monte-carlo integral [0, 3]xyz of x²+y²+z²");
		WriteLine($"Starting point:				({a[0]}, {a[1]}, {a[2]})");	
                WriteLine($"End point:				({b[0]}, {b[1]}, {b[2]})");
		WriteLine($"Expected result:	         	{expected:f6}");
                WriteLine($"Found result:	              		{integral:f6}");
                WriteLine($"Estimated error:	 	 	{error:f6}");
		WriteLine($"Deviation from expected:		{Abs(expected-integral):f6}");
		WriteLine($"Number of sample points used:		{N}");


		// Integrate [0,PI][0,PI][0,PI] (1-cos(x)*cos(y)*cos(z))^-1 /(PI*PI*PI) approx 1.39320...
		Func<vector, double> f = (x) => 1/(1-Cos(x[0])*Cos(x[1])*Cos(x[2])) * 1/(PI*PI*PI);
		// Limits
		a = new vector(0.0, 0.0, 0.0);
		b = new vector(PI, PI, PI);
		// Number of points
		N = 10000;
		// Call montecarlo.plain();
		expected = 1.3932039296856768591842462603255;
		(integral, error) = montecarlo.plain(f, a, b, N);
		// Write results
		WriteLine($"\n\nCalculating monte-carlo integral [0,PI]xyz");
		WriteLine($"(1-cos(x)*cos(y)*cos(z))^(-1) / PI^3");
		WriteLine($"Starting point:				({a[0]}, {a[1]}, {a[2]})");	
                WriteLine($"End point:				({b[0]}, {b[1]}, {b[2]})");
		WriteLine($"Expected result:	         	{expected:f6}");
                WriteLine($"Found result:	              		{integral:f6}");
                WriteLine($"Estimated error:	 	 	{error:f6}");
		WriteLine($"Deviation from expected:		{Abs(expected-integral):f6}");
		WriteLine($"Number of sample points used:		{N}");

		WriteLine("\n--------------------------------------------------------------\n");
	}
}
