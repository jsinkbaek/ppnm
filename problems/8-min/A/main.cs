using System;
using static System.Console;
using static System.Math;

class main
{
	static void Main()
	{
		WriteLine("\n--------------------------------------------------------------");
                WriteLine("\nProblem 8A:\n");
		WriteLine("minimization.qnewton exit details found in A/out.err.txt\n");

		// Rosenbrock's valley function
		Func<vector, double> rosenbrock = (v) => 
			(1-v[0])*(1-v[0])+100*(v[1]-v[0]*v[0])*(v[1]-v[0]*v[0]);
		// Convergence criteria and startingpoint
		double acc=1e-4;
		vector v0 = new vector(0, 0);
		vector vm_an = new vector(1.0, 1.0);

		// Minimizer call
		(vector v_min, int nsteps) = minimization.qnewton(rosenbrock, v0, acc:acc);

		WriteLine($"Looking for minimum of Rosenbrock valley function");
		WriteLine($"Starting point:			({v0[0]}, {v0[1]})");	
                WriteLine($"Accuracy goal acc:          	{acc}");
                WriteLine($"Known 1 minimum:         	({vm_an[0]}, {vm_an[1]})");
                WriteLine($"Found minimum:              	({v_min[0]}, {v_min[1]})");
                WriteLine($"Value of function at minimum:  	{rosenbrock(v_min)}");
		WriteLine($"Number of steps:		{nsteps}");

		
		// Himmelblau's function
		Func<vector, double> himmelblau = (v) =>
			(v[0]*v[0]+v[1]-11)*(v[0]*v[0]+v[1]-11) +
			(v[0]+v[1]*v[1]- 7)*(v[0]+v[1]*v[1]- 7);
		// Convergence criteria and startingpoint
		acc = 1e-4;
		v0 = new vector(0, 0);

		// Himmelblau local minima
		vector xmin = new vector(3.0, -2.805118, -3.779310, 3.584428);
		vector ymin = new vector(2.0, 3.13131, -3.28319, -1.84813);

		// Minimizer call
		(v_min, nsteps) = minimization.qnewton(himmelblau, v0, acc:acc);

		WriteLine($"\n\nLooking for a minimum of Himmelblau's function");
		WriteLine($"Starting point:			({v0[0]}, {v0[1]})");	
                WriteLine($"Known 4 minima:			( {xmin[0]:f4},  {ymin[0]:f4})"+
				$" ({xmin[1]:f4}, {ymin[1]:f4})");
		WriteLine($"				({xmin[2]:f4}, {ymin[2]:f4})"+
				$" ({xmin[3]:f4}, {ymin[3]:f4})");
		WriteLine($"Accuracy goal acc:          	{acc}");
                WriteLine($"Found minimum:              	({v_min[0]}, {v_min[1]})");
                WriteLine($"Value of function at minimum:  	{himmelblau(v_min)}");
		WriteLine($"Number of steps:		{nsteps}");
		
		WriteLine("\n--------------------------------------------------------------\n");
	}
}
