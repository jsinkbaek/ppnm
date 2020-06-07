using static System.Console;
using static System.Math;
using System;

public class minimization
{
	public static (vector, int) qnewton(
			Func<vector, double> f,	// Function to evaluate
			vector x,		// starting point
			double acc=1e-3,	// accuracy goal, |gradient|<acc on exit
			double alpha=1e-4,	// alpha param for Armijo condition
			double dx=1e-7,		// dx used in gradient calculation
			double minlam=1e-7,	// minimum lambda value before reset
			int limit=999,		// limit on recursion steps
			double eps=1.0/4194304
			)
	{// Quasi-newton minimization method for multivariable function
		int n = x.size;
		// Approximate inverse Hessian matrix B with identity matrix
		matrix B = new matrix(n, n);
		B.set_identity();
		
		// Gradient of f(x)
		vector gradx = gradient(f, x, dx:dx);
		vector Dx;
		// Precalc fx
		double fx = f(x);
		
		int nsteps = 0;
		do
		{	
			nsteps++;
			// Calculate Newton step
			Dx = -B*gradx;
			// Lambda factor
			double lam = 1.0;
			
			// Armijo condition step check (Bracktracking line-search)
			while(f(x+lam*Dx) > fx+alpha*lam*Dx.dot(gradx))
			{
				// Check if B needs to be reset and begrudgingly accept
				if (lam < minlam) {B.set_identity(); break;}
				// else update lambda
				lam /= 2;
			}

			// Calc new point z and gradz
			vector z = x + lam*Dx;
			vector gradz = gradient(f, z, dx:dx);
			// Calc u and <u, y>
			vector y = gradz - gradx;
			vector s = lam*Dx;
			vector u = s - B*y;
			double uTy = u.dot(y);
			// Do SR1 update of B if uTy numerically safe
			if (Abs(uTy) > 1e-6) 
				B.update(u, u, 1/uTy);
			// Update x, gradx, fx
			x = z;
			gradx = gradz;
			fx = f(x);

		}
		while (gradx.norm2() > acc & nsteps < limit & Dx.norm2()>eps*x.norm2());
		Error.WriteLine($"\nminimization.qnewton returning (x, nsteps)");
		Error.WriteLine($"gradx.norm2()		{gradx.norm2()}");
		Error.WriteLine($"acc			{acc}");
		Error.WriteLine($"nsteps / limit		{nsteps} / {limit}");
		Error.WriteLine($"Dx.norm2()		{Dx.norm2()}");
		Error.WriteLine($"eps*x.norm2()		{eps*x.norm2()}\n");
		return (x, nsteps);
	} // end qnewton


	static vector gradient(Func<vector, double> f, vector x, double dx=1e-7)
	{
		int n = x.size;
		vector grad = new vector(n);
		double fx = f(x);

		for (int i=0; i<n; i++)
		{
			x[i] += dx;
			grad[i] = (f(x) - fx)/dx;
			x[i] -= dx;
		}
		return grad;
	} // end gradient
} // end class
