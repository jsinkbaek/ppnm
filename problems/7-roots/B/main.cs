using System;
using static System.Console;
using static System.Math;
using System.Collections;
using System.Collections.Generic;

class main
{
	static void Main()
	{
		WriteLine("\n----------------------------------------------------------------");

		WriteLine("\nProblem 7B\n");

		// starting and end points
		double ra = 0.001; double rb = 20;
		vector fa = new vector(ra-ra*ra, 1-2*ra); // starting f and f' conditions
		vector fb = new vector(0, Double.NaN);	// expected result at f(b), f'(b) is unknown
		
		// ODE conditions
		double h=1e-3; double oeps=1e-6; double oacc=1e-6;

		// Auxiliary function to rootf on
		Func<vector, vector> M = delegate(vector epsilon)
		{// energy epsilon
			Func<double, vector, vector> seq = makeSEQ(epsilon[0]);
			(List<double> xvals, List<vector> yvals) = ode.rk45(seq, ra, fa, rb, h:h);
			vector Fb = yvals[yvals.Count-1];
			return new vector(Fb[0]-fb[0]);
		};

		// Shooting method by root finding of M(epsilon)
		vector x0 = new vector(-0.47);
		double racc = 1e-3;
		vector groot = rootf.newton(M, x0, eps:racc);
		double energy = groot[0];

		// Write results
		WriteLine($"Shooting method of hydrogen SEQ ODE by rootfinding of auxiliary func M");
                WriteLine($"Initial ODE step:                   	{h}");
		WriteLine($"ODE accuracy goal:			{oacc}");
		WriteLine($"ODE relative accuracy goal:		{oeps}");
		WriteLine($"ODE starting x:				{ra}");
		WriteLine($"ODE end x:				{rb}");
		WriteLine($"rootf started at epsilon:		{x0[0]}");
		WriteLine($"rootf accuracy goal:			{racc}");
                WriteLine($"Found root of M(epsilon):           	{energy}");
                WriteLine($"Value of M at root:		        {M(groot)[0]}");
		WriteLine($"Analytical root:			{-0.5}");
		WriteLine($"Deviation from analytical result:	{-0.5-energy}");

		// Solve differential equation with found solution and export
		Func<double, vector, vector> seq_sol = makeSEQ(energy);
		(List<double> rvals, List<vector> fvals) = ode.rk45(seq_sol, ra, fa, rb, h:h);
		
		var writer = new System.IO.StreamWriter("out.func.txt");
		for (int i=0; i<rvals.Count; i++)
		{
			if (rvals[i]>8) break;
			writer.WriteLine($"{rvals[i]} {fvals[i][0]} {fvals[i][1]}");
		}

		// Write analytical solution
		writer.Write("\n\n");
		for (double r=0; r<=8; r+=0.05)
		{
			writer.WriteLine($"{r} {r*Exp(-r)}");
		}
		writer.Close();

		WriteLine($"\n\nSee PlotB.svg for plot of solution and analytical function");
		WriteLine("\n----------------------------------------------------------------\n");
	
	}


	public static Func<double, vector, vector> makeSEQ(double epsilon)
	{// make hydrogen Schrödinger equation with given epsilon
		Func<double, vector, vector> eq = delegate(double x, vector y)
		{
			return new vector (y[1], -2*(epsilon*y[0] + y[0]/x));
			
		};
		return eq;
	}
}
