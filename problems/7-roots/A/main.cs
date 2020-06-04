using static System.Console;
using static System.Math;
using System;

class main
{
	static void Main()
	{
		WriteLine("Problem 7A:\n");
		
		// Rootfinding of sin²(x)=0
		Func<vector, vector> sin2 = (x) => new vector(Sin(x[0])*Sin(x[0]));
		vector x0 = new vector(1.5);	// close to root x=PI

		// Rootfinder call
		double eps1 = 1e-3; double dx1=1e-7;
		vector groot1 = rootf.newton(sin2, x0, eps:eps1, dx:dx1);
		double res1 = PI;
		
		WriteLine($"Looking for root of Sin(x)² close to {x0[0]:f3}:");
		WriteLine($"Accuracy goal eps:			{eps1}");
		WriteLine($"Analytical root:			{res1}");
		WriteLine($"Found root:				{groot1[0]}");
		WriteLine($"Deviation:				{groot1[0]-res1}");
		WriteLine($"Value of function at root:		{sin2(groot1)[0]}");

		// Rootfinding of equations 
		// x0 + x1^2 + x2^3 = 0
		// x0 + x1 - 5 = 0
		// x0 * x2 - 2 = 0
		// real solution: x0=-0.6358, x1=5.6358, x2=-3.14565
		Func<vector, vector> p3 = (x) => new vector(x[0] + x[1]*x[1] + x[2]*x[2]*x[2],
							    x[0] + x[1] - 5,
							    x[0] * x[2] - 2);
		double eps2 = 1e-3;
		x0 = new vector(-0.1, 4.0, -1.0);
		vector groot2 = rootf.newton(p3, x0);
		vector res2 = new vector(-0.6358, 5.6358, -3.14565);


		WriteLine($"\nLooking for root of equation system");
		Write($"(x0+x1^2+x2^3, x0+x1-5, x0*x2-2) near (-0.1, 4.0, -1.0):\n");
		WriteLine($"Accuracy goal eps:		     {eps2}");
		res2.print("Analytical root:		");
		groot2.print("Found root:			");
		(groot2-res2).print("Deviation:			");
		p3(groot2).print("Value of function at root:	");
		
		
		// Extremus finding of RosenBrock's valley function by root search of gradient
		// f(x,y) = (1-x)^2 + 100(y-x^2)^2
		// Define derivative:
		Func<vector, vector> fr = (x) => new vector(-2*(1-x[0])+100*2*(x[1] - x[0]*x[0]) * 2*x[0],
							    100*2*(x[1] - x[0]*x[0]));
		// Analytical minimum (root) at (1, 1)
		x0 = new vector(1.4, 0.5);
		double eps3 = 1e-3; double dx3 = 1e-7;

		// Rootfinder call
		vector groot3 = rootf.newton(fr, x0);
		vector res3 = new vector(1.0, 1.0);
		
		WriteLine($"\nLooking for extremus of Rosenbrock's valley function near (1.4, 0.5):");
		WriteLine($"Accuracy goal eps:		     {eps3}");
		res3.print("Analytical root:		");
		groot3.print("Found root:			");
		(groot3-res3).print("Deviation:			");
		fr(groot3).print("Value of function at root:	");
	}
}
