using System;
using static System.Math;
using static System.Console;
using System.Collections;
using System.Collections.Generic;

public class ode
{
	public static vector[] rk12_step(
			Func<double, vector, vector> f,	/* right-hand side of dy/dt=f(t,y) */
			double t,			/* current value of variable */
			vector yt,			/* current y(t) of sought function */
			double h			/* stepsize */
			)
	{
		// Midpoint method
		vector k0 = f(t, yt);
		vector k12 = f(t + 0.5*h, yt + 0.5*h*k0);
		vector yh = yt + h*k12;	
		// Error estimate as difference between order 1 and 2
		vector err = h*k12 - h*k0;
		return new vector[] {yh, err};
	}


	public static (List<double>, List<vector>) rk12(
			Func<double, vector, vector> f, 
			double a, 			/* startingpoint */
			vector ya, 			/* initial values y(a) */
			double b,			/* end-point of integration */
			double h=0.1,			/* initial stepsize */
			double acc=1e-3,		/* absolute accuracy goal */
			double eps=1e-3,		/* relative accuracy goal */
			List<double> xlist = null,	/* lists to fill results into */
			List<vector> ylist = null,
			int limit = 999,		/* Absolute limit on steps */
			double max_factor = 2,		/* limit on step factor increase */
			bool slack = false		/* decides if accuracy should reduce with integration */
			)
	{
		Error.WriteLine("ODE RK12 integration");
		if (slack) {Error.WriteLine("Slacking on tolerances as integration proceeds");}
		return driver(f, a, ya, b, h, acc, eps, xlist, ylist, limit, max_factor, slack, rk12_step);
	}
	

	public static vector[] rk45_step(
			Func<double, vector, vector> f,
			double t,
			vector yt,
			double h
			)
	{ /* Implementation of Runge Kutta Fehlberg */
		// Butcher's table
		vector K1 = h * f(t, yt);
		vector K2 = h * f(t+1.0/4*h, yt+1.0/4*K1);
		vector K3 = h * f(t+3.0/8*h, yt+3.0/32*K1+9.0/32*K2);
		vector K4 = h * f(t+12.0/13*h, yt+1932.0/2197*K1-7200.0/2197*K2+7296.0/2197*K3);
		vector K5 = h * f(t+1.0*h, yt+439.0/216*K1-8.0*K2+3680.0/513*K3-845.0/4104*K4);
		vector K6 = h * f(t+0.5*h, yt-8.0/27*K1+2.0*K2-3544.0/2565*K3+1859.0/4104*K4-11.0/40*K5);

		vector[] K = new vector[] {K1, K2, K3, K4, K5, K6};
		vector b5 = new vector(new double[] {16.0/135, 0, 6656.0/12825, 28561.0/56430, -9.0/50, 2.0/55});
		vector b4 = new vector(new double[] {25.0/216, 0, 1408.0/2565, 2197.0/4104, -1.0/5, 0});

		vector yh = yt;
		vector err = new vector(yt.size);

		for (int i=0; i<b5.size; i++)
		{
			yh += K[i] * b5[i];
			err += K[i] * (b5[i] - b4[i]);
		}
		return new vector[] {yh, err};
	}

	
	public static (List<double>, List<vector>) rk45(
			Func<double, vector, vector> f,
			double a,
			vector ya,
			double b,
			double h=0.1,
			double acc=1e-6,
			double eps=1e-6,
			List<double> xlist = null,
			List<vector> ylist = null,
			int limit = 999,
			double max_factor = 2,
			bool slack = false
			)
	{
		Error.WriteLine("ODE RK45 integration");
		if (slack) {Error.WriteLine("Slacking on tolerances as integration proceeds");}
		return driver(f, a, ya, b, h, acc, eps, xlist, ylist, limit, max_factor, slack, rk45_step);
	}


	public static (List<double>, List<vector>) driver(
			Func<double, vector, vector> f,	/* equation */
			double a,			/* starting-point */
			vector ya,			/* initial values y(a) */
			double b,			/* end-point of integration */
			double h,			/* Initial stepsize */
			double acc,			/* Absolute accuracy goal */
			double eps,			/* Relative accuracy goal */
			List<double> xlist,		/* Lists to fill results into */
			List<vector> ylist,		
			int limit,			/* Absolute step limit */
			double max_factor,		/* limit on step factor increase */
			bool slack,			/* decides if accuracy should reduce with integration */
			Func< Func<double, vector, vector>, double, vector, double,
				vector[]> stepper	/* Stepping function */
			)
	{
		int nsteps = 0;
		if (xlist != null)
		{// Clears or creates lists to be filled with results, and adds starting point
			xlist.Clear(); xlist.Add(a);
		}
		else 
		{// null-coealescing assignment operator ??= used because comment whined
			xlist ??= new List<double>();
		//	List<double> xlist = new List<double>(); 
			xlist.Add(a);
		}
		if (ylist != null)
		{
			ylist.Clear(); ylist.Add(ya);
		}
		else 
		{
			ylist ??= new List<vector>();
		//	List<vector> ylist = new List<vector>(); 
			ylist.Add(ya);
		}

		double x = a;
		vector yx = ya;
		while (x<b)
		{
			nsteps++;
			if (x+h > b) {h = b-x;}

			// Attempt step
			vector[] attempt = stepper(f, x, yx, h);
			vector yh = attempt[0];
			vector err = attempt[1];

			// Calculate step tolerance
			vector tau;
			if (slack) {tau = (eps*yh.abs() + acc) * Sqrt(h/(b-x));}
			else {tau = (eps*yh.abs() + acc) * Sqrt(h/(b-a));}
			// Slack uses x instead of a. Reduces accuray requirements as integration progresses.

			// Determine if error is within tolerance, calculate tolerance ratio
			bool accept = true;
			vector tol_ratio = new vector(err.size);
			for (int i=0; i<tau.size; i++)
			{
				tol_ratio[i] = Abs(tau[i])/Abs(err[i]);
				if (tol_ratio[i] < 1) {accept = false;}
			}
			
			// Update if step was accepted
			if (accept) 
			{
				x += h;
				yx = yh;
				xlist.Add(x);
				ylist.Add(yx);
			}
			else {Error.WriteLine("Bad step at x={0}. Step rejected.", x);}

			// Adjust step size based on empirical formula
			double tolmin = tol_ratio[0];
			for (int i=1; i<tol_ratio.size; i++) {tolmin = Min(tolmin, tol_ratio[i]);}
			
			double adj_factor = Pow(tolmin, 0.25)*0.95;
			if (adj_factor > max_factor) {adj_factor = max_factor;}
			
			h = h*adj_factor;

			if (nsteps >= limit)
			{
				Error.WriteLine("Step limit exceeded, returning current result");
				break;
			}
		} // end while loop
		Error.WriteLine("ODE solved in {0} steps", nsteps);
		return (xlist, ylist);
	} // ode driver

}
