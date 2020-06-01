using System;
using static System.Console;
using static System.Math;
using static System.Double;

public class quad
{
	public static (double, double) o4a(
	//public static double o4a(
			Func<double, double> f,	// function to integrate
			double a,		// start-point
			double b,		// end-point
			double acc=1e-6,	// global accuracy goal
			double eps=1e-6,	// relative accuracy goal
			int nrecs=0,		// current amount of recursions
			int limit=30		// upper limit on number of recursions
			)
	{// open 4 point adaptive trapeziodal quadrature
		vector w = new vector(new double[] {2.0/6, 1.0/6, 1.0/6, 2.0/6}); // weight for higher order
		vector v = new vector(new double[] {1.0/4, 1.0/4, 1.0/4, 1.0/4}); // weight for lower order
		vector xi = new vector(new double[] {1.0/6, 2.0/6, 4.0/6, 5.0/6}); // abscissas scaled between [0,1]
		vector fi = new vector(new double[] {NaN, NaN, NaN, NaN}); // the two middle ones will be midpoints
		int[] calc = new int[] {0, 3};	// index of points to recalculate every time
		int[] mids = new int[] {1, 2};	// index of points to reuse
		
		return oga(f, a, b, w, v, xi, fi, calc, mids, acc, eps, nrecs, limit);
	}


	public static (double, double) oga(
	//public static double oga(
			Func<double, double> f, 	// function to integrate
			double a,			// start-point
			double b,			// end-point
			vector w,			// weight values high order
			vector v,			// weight values low order
			vector xi,			// Abscissas scaled to interval [0, 1]
			vector fi,			// points
			int[] calc,			// index of points to calculate every time
			int[] mids,			// index of midpoints that only should be calculated once
			double acc=1e-6,		// accuracy goal
			double eps=1e-6,		// relative accuracy goal
			int nrecs=0,			// current amount of recursions
			int limit=30			// upper limit on number of recursions
			)
	{// open general point adaptive integrator, with reuse of points
		double h = b - a;
		double sqrt2 = Sqrt(2);
		
		for (int i=0; i<calc.Length; i++)
		{// recalculate outer points
			fi[calc[i]] = f(a + h*xi[calc[i]]);
		}
		if (IsNaN(fi[mids[0]]))
		{// check if midpoints need to be calculcated, and do if
			for (int i=0; i<mids.Length; i++)
			{
				fi[mids[i]] = f(a + h*xi[mids[i]]);
			}
		}
		double integral = 0;
		double approx = 0;
		for (int i=0; i<fi.size; i++)
		{// calculate higher and lower order integral
			integral += w[i] * fi[i];
			approx += v[i] * fi[i];
		}
		integral = integral * h;
		approx = approx * h;
		double error = Abs(integral-approx);
		double tolerance = acc + eps*Abs(integral);
		if (error < tolerance)
		{
			//return integral;
			return (integral, error);
		}
		else if (++nrecs > limit)
		{
			Error.WriteLine($"oga: nrec>{limit}, a={a}, b={b}");
			//return integral;
			return (integral, error);
		}
		else
		{// split interval in middel and evaluate each half individually, reusing points as midpoints
			vector fi1 = new vector(fi.size); vector fi2 = new vector(fi.size);
			for (int i=0; i<mids.Length; i++)
			{// overwrite midpoints to use already calculated points
				fi1[mids[i]] = fi[i];
				fi2[mids[i]] = fi[i+fi.size/2];	
			}
			//return oga(f, a, (a+b)/2, w, v, xi, fi1, calc, mids, acc/sqrt2, eps, nrecs, limit)
			//	+ oga(f, (a+b)/2, b, w, v, xi, fi2, calc, mids, acc/sqrt2, eps, nrecs, limit);
			(double itg1, double err1) = oga(f, a, (a+b)/2, w, v, xi, fi1, calc, mids, 
								     acc/sqrt2, eps, nrecs, limit);
			(double itg2, double err2) = oga(f, (a+b)/2, b, w, v, xi, fi2, calc, mids, 
								     acc/sqrt2, eps, nrecs, limit);

			// Add results
			integral = itg1 + itg2;
			error = Sqrt(Pow(err1, 2) + Pow(err2, 2));
			return (integral, error);
		}


	}// open general point adaptive integrator

}
