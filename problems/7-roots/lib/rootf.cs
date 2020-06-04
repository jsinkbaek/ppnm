using System;
using static System.Math;
using static System.Console;
using static linalg;

public class rootf
{
	public static vector newton(
			Func<vector, vector> f,	// func takes vector(x1,...,xn) returns vector(f1(..),...,fn(..))
			vector x,		// starting values of vector(x1,...,xn)
			double eps=1e-3,	// accuracy goal ||f(x)||<eps
			double dx=1e-7		// finite difference used in numerical eval of Jacobian
			)
	{
		int n = x.size;
		vector fx = f(x);
		double lam;
		do
		{
			// create jacobian
			matrix J = jacobian(f, x, fx, dx);

			// find jacobian inverse by QR-decomposition
			qr_decomp_GS qrd = new qr_decomp_GS(J);
			matrix J_inv = qrd.inverse();

			// solve J*Dx = - fx for newton stepsize Dx
			vector Dx = -J_inv*fx;

			// lambda factor
			lam = 1.0;

			// backtracking linesearch algorithm (first failing is good, second is step out try again)
			while(f(x+lam*Dx).norm2() > fx.norm2()*(1-lam/2) & lam > 1.0/64) lam /= 2;
			// norm2 is an euclidean vector norm I implemented
			// Update values
			x = x + lam*Dx;
			fx = f(x);
		}
		while (fx.norm2() > eps);
		Error.WriteLine($"rootf.newton returning x, a condition is satisfied");
		Error.WriteLine($"f(x).norm2()		{fx.norm2()}");
		Error.WriteLine($"eps			{eps}");
		Error.WriteLine($"lam			{lam}");
		Error.WriteLine($"dx			{dx}\n");
		return x;
	} // newton


	public static matrix jacobian(
			Func<vector, vector> f, // func f
			vector x, 		// starting values x
			vector fx,		// f evaluated at starting values x
			double dx		// step size dx
			)
	{
		int n = x.size;
		matrix J = new matrix(n, n);
		vector df;
		for (int j=0; j<n; j++)
		{
			// change only one index to make a small step in that direction (finite difference)
			x[j] += dx;
			df = f(x) - fx;
			for (int i=0; i<n; i++)
			{
				J[i, j] = df[i]/dx;
			}
			// change value of x[j] back to before
			x[j] -= dx;
		}
		return J;
	} // jacobian
}
