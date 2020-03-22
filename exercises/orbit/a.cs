using System;
using static System.Math;
using static System.Console;
using System.Collections;
using System.Collections.Generic;

public class acl
{//Exercise A
	public static int A()
	{
		// Boundary conditions
		double xa=0, xb=3, y0=0.5;
		vector ya = new vector(y0);
		// Logistic function definition
		Func<double, vector, vector> logistic = delegate(double t, vector y)
		{
			return new vector(y[0]*(1-y[0]));
		};

		// Solved coordinates create
		List<double> xs = new List<double>();
		List<vector> ys = new List<vector>();

		// ODE solver rk23 call
		ode.rk23(logistic, xa, ya, xb, xlist:xs, ylist:ys);
		
		for (int i=0; i<xs.Count; i++)
			WriteLine($"{xs[i]} {ys[i][0]}");

	return 0;
	}


}
