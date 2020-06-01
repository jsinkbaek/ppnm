using System;
using static System.Console;
using static System.Math;

class main
{
	static void Main()
	{
		WriteLine("Problem A:");
		// First integration
		WriteLine("\nFirst integrate Sqrt(x) on interval [0, 1] with o4a");
		// function and interval
		Func<double, double> sqrt = (x) => Sqrt(x);
		double a = 0, b = 1;
		// integrator call o4a
		(double itg1, double err1, int nc1) = quad.o4a(sqrt, a, b);

		WriteLine("Using relative and absolute tolerance 1e-6");
		WriteLine($"quad.o4a estimates integral = {itg1}");
		WriteLine($"analytical value of integral is = {2.0/3}");
		WriteLine($"error on estimate = {err1}");
		WriteLine($"deviation from analytic = {2.0/3 - itg1}");
		WriteLine($"amount of calls = {nc1}");
		// integrator call o8a
		WriteLine("\nSame integral using o8a");
		(itg1, err1, nc1) = quad.o8a(sqrt, a, b);

		WriteLine("Using relative and absolute tolerance 1e-6");
		WriteLine($"quad.o4a estimates integral = {itg1}");
		WriteLine($"analytical value of integral is = {2.0/3}");
		WriteLine($"error on estimate = {err1}");
		WriteLine($"deviation from analytic = {2.0/3 - itg1}");
		WriteLine($"amount of calls = {nc1}");

		// Second integration
		WriteLine("\n-----------------------------------------------------");
		WriteLine("\nSecond integrate 4*Sqrt(1-x*x) on interval [0, 1] with o4a");
		// Function
		Func<double, double> f2 = (x) => 4*Sqrt(1-x*x);
		// quad.o4a call
		(double itg2, double err2, int nc2) = quad.o4a(f2, a, b);
		
		WriteLine($"quad.o4a estimates integral = {itg2}");
		WriteLine($"analytical value of integral is {PI}");
		WriteLine($"error on estimate = {err2}");
		WriteLine($"deviation from analytic = {PI - itg2}");
		WriteLine($"amount of calls = {nc2}");
		// integrator call o8a
		WriteLine("\nsame integral using o8a");
		(itg2, err2, nc2) = quad.o8a(f2, a, b);
		
		WriteLine($"quad.o4a estimates integral = {itg2}");
		WriteLine($"analytical value of integral is {PI}");
		WriteLine($"error on estimate = {err2}");
		WriteLine($"deviation from analytic = {PI - itg2}");
		WriteLine($"amount of calls = {nc2}");


	}
}
