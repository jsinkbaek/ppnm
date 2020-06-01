using System;
using static System.Console;
using static System.Math;
using static System.Double;
using System.Reflection;

public class quad
{
	
	public static (double, double, int) o4a(
			Func<double, double> f,	// function to integrate
			double a,		// start-point
			double b,		// end-point
			double acc=1e-6,	// global accuracy goal
			double eps=1e-6,	// relative accuracy goal
			int nrecs=0,		// current amount of recursions
			int limit=30,		// upper limit on number of recursions
			string SubstitutionType="regular"	// indicates if known substitution should be performed
			)
	{// open 4 point adaptive trapeziodal quadrature
		vector w = new vector(new double[] {2.0/6, 1.0/6, 1.0/6, 2.0/6}); // weight for higher order
		vector v = new vector(new double[] {1.0/4, 1.0/4, 1.0/4, 1.0/4}); // weight for lower order
		vector xi = new vector(new double[] {1.0/6, 2.0/6, 4.0/6, 5.0/6}); // abscissas scaled between [0,1]
		vector fi = new vector(new double[] {NaN, NaN, NaN, NaN}); // the two middle ones will be midpoints
		int[] calc = new int[] {0, 3};	// index of points to recalculate every time
		int[] mids = new int[] {1, 2};	// index of points to reuse
		
		(f, a, b) = substitution_control(SubstitutionType, f, a, b);
		
		return oga(f, a, b, w, v, xi, fi, calc, mids, acc, eps, nrecs, limit);
	}


	public static (double, double, int) o8a(
			Func<double, double> f,
			double a,
			double b,
			double acc=1e-6,
			double eps=1e-6,
			int nrecs=0,
			int limit=30,
			string SubstitutionType="regular"
			)
	{// open 8-point adaptive integrator with reuse of points
		vector w = new vector(new double[] {4738.0/19845, -59.0/567, 5869.0/13230, -74.0/945,
						    -74.0/945, 5869.0/13230, -59.0/567, 4738.0/19845});
		vector v = new vector(new double[] {208.0/945, -7.0/135, 209.0/630, 0,
						    0, 209.0/630, -7.0/135, 208.0/945});
		vector xi = new vector(new double[] {1.0/12, 2.0/12, 4.0/12, 5.0/12,
						     7.0/12, 8.0/12, 10.0/12, 11.0/12});
		vector fi = new vector(new double[] {NaN, NaN, NaN, NaN, NaN, NaN, NaN, NaN});
		int[] calc = new int[] {0, 3, 4, 7};
		int[] mids = new int[] {1, 2, 5, 6};

		(f, a, b) = substitution_control(SubstitutionType, f, a, b);

		return oga(f, a, b, w, v, xi, fi, calc, mids, acc, eps, nrecs, limit);
	}


	static (double, double, int) oga(
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
			double acc,			// accuracy goal
			double eps,			// relative accuracy goal
			int nrecs,			// current amount of recursions
			int limit,			// upper limit on number of recursions
			int ncalls=0			// total amount of method calls
			)
	{// open general point adaptive integrator, with reuse of points
		double h = b - a;
		double sqrt2 = Sqrt(2);
		ncalls++;
		
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
			return (integral, error, ncalls);
		}
		else if (++nrecs > limit)
		{
			Error.WriteLine($"oga: nrec>{limit}, a={a}, b={b}, f(a)={f(a)}, f(b)={f(b)}");
			//return integral;
			return (integral, error, ncalls);
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
			(double itg1, double err1, int nc1) = oga(f, a, (a+b)/2, w, v, xi, fi1, calc, mids, 
								     acc/sqrt2, eps, nrecs, limit, ncalls);
			(double itg2, double err2, int nc2) = oga(f, (a+b)/2, b, w, v, xi, fi2, calc, mids, 
								     acc/sqrt2, eps, nrecs, limit, ncalls);

			// Add results
			integral = itg1 + itg2;
			error = Sqrt(Pow(err1, 2) + Pow(err2, 2));
			ncalls = nc1 + nc2;
			return (integral, error, ncalls);
		}

	}// open general point adaptive integrator


	static (Func<double, double>, double, double) substitution_control(
			string SubstitutionType,
			Func<double, double> f, 
			double a, double b
			)
	{// Controls which substitution to do
		string stype = SubstitutionType;
		
		if (Equals(stype, "regular"))
		{
			return (f, a, b);
		}
		else if (Equals(stype, "clenshaw-curtis"))
		{
			return clenshaw_curtis(f, a, b);
		}
		else
		{
			Error.WriteLine("Unknown SubstitutionType, assuming \"regular\"");
			return (f, a, b);
		}		
	}

	
	/*static (Func<double, double>, double, double) clenshaw_curtis(Func<double, double> f, double a, double b)
	{// Perform Clenshaw-Curtis substitution
		// Rescale to be from -1 to 1
		// u(a) = -1, u(b) = 1, u(x) = alpha*x + b
		Func<double, double> u = (x) => x*(b-a)/2 - (b+a)/2;
		// Rescale f
		Func<double, double> fu = (x) => f(u(x)) * (b-a)/2;

		// Make Clenshaw-Curtis transformation:
		Func<double, double> fcc = (theta) => fu(Cos(theta))*Sin(theta);	
	
		return (fcc, 0.0, PI);
	}*/

	static (Func<double, double>, double, double) clenshaw_curtis(Func<double, double> f, double a, double b)
	{// Perform Clenshaw-Curtis substitution
		// u(a)=-1, u(b)=1
		Func<double, double> fn = (x) => f((a+b)/2+(b-a)*Cos(x)/2) * Sin(x)*(b-a)/2;
		return (fn, 0, PI);
	}

}
