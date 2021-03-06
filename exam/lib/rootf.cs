using System;
using static System.Math;
using static System.Console;
using static cmath;
using static linalg;


public class rootf
{
	static readonly complex i = new complex(0, 1);

	public static (complex, int) newton(
			Func<complex, complex> f,	// func takes z=x+iy returns complex f(z)
			complex z,			// starting values of z
			complex dz,			// complex stepsize for numerical differentation
			Func<complex, complex> df=null,	// derivative of f
			double eps=1e-3			// accuracy goal ||f(z)||<eps
			)
	{// Implements a 1-dimensional newton rootfinder of complex functions with complex variables
		// If df is not given, will instead be a quasi-newton method with finite-difference approx
		int nsteps = 0;
		complex fz = f(z);
		double lam;
		
		do
		{
			nsteps++;
			// df/dz
			complex dfdz;
			if (df==null){dfdz = (f(z+dz) - fz)/dz;}
			else {dfdz = df(z);}
			// Dz = - f(z)/(df/dz)
			complex Dz = -fz / dfdz;
			// Lambda factor
			lam = 1.0;
			// Backtracking line-search algorithm
			while(abs(f(z+lam*Dz)) > abs(fz)*(1-lam/2) & lam > 1.0/64) lam /= 2;
			// Update values
			z = z + lam*Dz;
			fz = f(z);
		}
		while (abs(fz) > eps);
		Error.WriteLine($"rootf.newton returning complex z, a condition is satisfied");
		Error.WriteLine($"abs(f(z))		{abs(fz)}");
		Error.WriteLine($"eps			{eps}");
		Error.WriteLine($"lam			{lam}");
		Error.WriteLine($"dz			{dz}");
		return (z, nsteps);
	}// newton complex
	

	public static (vector, int) newton(
			Func<vector, vector> f,	// func takes vector(x1,...,xn) returns vector(f1(..),..,fn(..))
			vector x,		// starting values of vector(x1,...,xn)
			double eps=1e-3,	// accuracy goal ||f(x)||<eps
			double dx=1e-7		// finite difference used in numerical eval of jacobian
			)
	{// overload that implements a multiple-dimensional newton rootfinder for real functions of real variables
		int n = x.size;
		int nsteps = 0;
                vector fx = f(x);
                double lam;
                do
                {
                        nsteps++;
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
                        // norm2 is an euclidean vector norm by its definition, with over/underflow potential
                        // Update values
                        x = x + lam*Dx;
                        fx = f(x);
                }
                while (fx.norm2() > eps);
                Error.WriteLine($"rootf.newton returning x, a condition is satisfied");
                Error.WriteLine($"f(x).norm2()          {fx.norm2()}");
                Error.WriteLine($"eps                   {eps}");
                Error.WriteLine($"lam                   {lam}");
                Error.WriteLine($"dx                    {dx}\n");
                return (x, nsteps);
	}// qnewton real vector


	static matrix jacobian(
                        Func<vector, vector> f, // func f
                        vector x,               // starting values x
                        vector fx,              // f evaluated at starting values x
                        double dx               // step size dx
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


	public static (complex, int) qnewton(
			Func<complex, complex> f,
			complex z,
			complex dz,
			double eps=1e-3
			)
	{// Calls newton 1D complex rootf, specifying no derivative df (so uses finite differences)
		return newton(f, z, dz:dz, df:null, eps:eps);
	}

}
