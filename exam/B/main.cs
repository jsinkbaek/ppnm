using static System.Console;
using static System.Math;
using System;
using static cmath;
using System.Diagnostics;

class main
{
	static void Main()
	{
		WriteLine("\n----------------------------------------------------------------\n");
		WriteLine("\nPart B: Test of complex quasi-newton implementation\n");
		complex i = new complex(0, 1);
		complex croot; int nstepsc; complex res1; complex z0; int fcalls=0;
		double eps = 1e-3; complex dz = 1e-7 * (1+i);
	
		// 1D complex rootfinding of f = sin(z)*exp(z), roots z=pi*n + 0i
		Func<complex, complex> fc2 = delegate(complex z)
			{fcalls++; return sin(z)*exp(z);};
		Func<complex, complex> dfc2 = delegate(complex z)
			{fcalls++; return exp(z)*(sin(z)+cos(z));};
		z0 = 1+0.1*i;
		res1 = 0;

		// Finite difference derivative
		fcalls = 0;
		(croot, nstepsc) = rootf.qnewton(fc2, z0, eps:eps, dz:dz);

		// Write
		WriteLine($"Doing complex 1D qnewton rootf of f = sin(z)*exp(z) close to {z0}");
                WriteLine($"Using finite difference approximation as derivative");
		WriteLine($"Accuracy goal eps:                  {eps}");
                WriteLine($"Expected root:                      {res1}");
                WriteLine($"Found root:                         {croot}");
                WriteLine($"Deviation:                          {(croot-res1).Re:f3}+{(croot-res1).Im:f2}i");
                WriteLine($"Value of function at root:          {fc2(croot)}");
                WriteLine($"Number of function calls:           {fcalls}");
                WriteLine($"Number of rootfinder steps:         {nstepsc}\n");


		// Analytical derivative
		fcalls = 0;
		(croot, nstepsc) = rootf.newton(fc2, z0, eps:eps, dz:dz, df:dfc2);

		// Write
                WriteLine($"Doing complex 1D newton rootf of f = sin(z)*exp(z) close to {z0}");
                WriteLine($"Using analytical derivative exp(z)*(sin(z)+cos(z))");
                WriteLine($"Accuracy goal eps:                  {eps}");
                WriteLine($"Expected root:	               	    {res1}");
                WriteLine($"Found root:                         {croot}");
                WriteLine($"Deviation:                          {(croot-res1).Re:f3}+{(croot-res1).Im:f2}i");
                WriteLine($"Value of function at root:          {fc2(croot)}");
                WriteLine($"Number of function calls:           {fcalls}");
                WriteLine($"Number of rootfinder steps:         {nstepsc}\n");

		// Complex rootfinding of f = z*, finite difference step
		WriteLine("*************\n");
		fcalls = 0;
		Func<complex, complex> fc = delegate(complex z)
		{
			fcalls++;
			return new complex(z.Re, -z.Im);
		};
		z0 = new complex(1,1);

		// Complex rootfinder call
		(croot, nstepsc) = rootf.qnewton(fc, z0, eps:eps, dz:dz);
		res1 = 0+0*i;

		// Write
		WriteLine($"Doing complex 1D qnewton rootf of f = z* close to {z0}");
		WriteLine($"Using finite-difference approximation as derivative");
		WriteLine($"Accuracy goal eps:                  {eps}");
                WriteLine($"Analytical roots:                   {res1}");
                WriteLine($"Found root:                         {croot}");
                WriteLine($"Deviation:                          {(croot-res1).Re:f3}+{(croot-res1).Im:f2}i");
                WriteLine($"Value of function at root:          {fc(croot)}");
		WriteLine($"Number of function calls:	    {fcalls}");
		WriteLine($"Number of rootfinder steps:	    {nstepsc}\n");

		// 2D real rootfinding of f = z*, finite difference.
		Func<vector, vector> fr = delegate(vector xy)
		{
			fcalls++; double x=xy[0]; double y=xy[1];
			return new vector(x, -y);
		};
		vector xy0 = new vector(z0.Re, z0.Im);
		fcalls = 0;
		double dxy = 1e-7;
		(vector rroot, int nstepsr) = rootf.newton(fr, xy0, eps:eps, dx:dxy);
		complex rrootc = new complex(rroot[0], rroot[1]);
		vector frval = fr(rroot);
		complex frvalc = new complex(frval[0], frval[1]);

		// Write
                WriteLine($"Doing real 2D rootfinding of f = conj(z) close to {z0}");
                WriteLine($"Using Jacobi matrix with finite difference approximation");
                WriteLine($"Accuracy goal eps:                  {eps}");
                WriteLine($"Analytical roots:                   {res1}");
                WriteLine($"Found root:                         {rrootc}");
                WriteLine($"Deviation:                          {(rrootc-res1).Re:f3}+{(rrootc-res1).Im:f2}i");
                WriteLine($"Value of function at root:          {frvalc}");
                WriteLine($"Number of function calls:           {fcalls}");
                WriteLine($"Number of rootfinder steps:         {nstepsr}\n");
		
		WriteLine("\n----------------------------------------------------------------\n");
	}

}
