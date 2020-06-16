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
		WriteLine("\nExam project 15 - complex rootfinding - Jeppe ST\n");
		WriteLine("\n----------------------------------------------------------------\n");
		WriteLine("\nPart A: Test of complex newton implementation\n");
		complex i = new complex(0, 1);
		
		// Complex rootfinding of f = z^3 + 5, finite difference step
		int fcalls = 0;
		Func<complex, complex> fc = delegate(complex z)
		{
			fcalls++;
			return z*z*z +5;
		};
		complex z0 = new complex(0.5, 0.1);

		// Complex rootfinder setup
		double eps = 1e-3; complex dz = 1e-7 * (1+i);
		complex five = new complex(5, 0);
		complex minusfive = new complex(-5, 0);
		complex minusone = new complex(-1, 0);
		complex res1 = -five.pow(1.0/3); 
		complex res2 = minusfive.pow(1.0/3);
		complex res3 = minusone.pow(2.0/3)*five.pow(1.0/3);
		
		// Complex rootfinding, analytical derivative
		fcalls = 0;
		Func<complex, complex> df = delegate(complex z)
			{fcalls++; return 3*z*z;};
		(complex croot2, int nstepsc2) = rootf.newton(fc, z0, eps:eps, dz:dz, df:df);

		// Write
		WriteLine($"Doing complex 1D newton rootf of f = z^3 + 5 close to {z0}");
                WriteLine($"Using analytical derivative 3*z²");
                WriteLine($"Accuracy goal eps:                  {eps}");
                WriteLine($"Analytical roots:                   {res1} , {res2} , {res3} ");
                WriteLine($"Found root:                         {croot2}");
                WriteLine($"Deviation:                          {(croot2-res2).Re:f3}+{(croot2-res2).Im:f2}i");
                WriteLine($"Value of function at root:          {fc(croot2)}");
                WriteLine($"Number of function calls:           {fcalls}");
                WriteLine($"Number of rootfinder steps:         {nstepsc2}\n");

		// 2D real rootfinding of f = z^3 + 5
		fcalls = 0;
		Func<vector, vector> fr = delegate(vector xy)
		{
			fcalls++;
			double x = xy[0]; double y=xy[1];
			complex fz = x*x*x - y*y*x - 2*x*y*y + 2*i*x*x*y + i*x*x*y - i*y*y*y + 5;
			return new vector(fz.Re, fz.Im);
		};
		vector xy0 = new vector(0.5, 0.1);

		// 2D real rootfinder call
		eps = 1e-3; double dxy = 1e-7;
		(vector rroot, int nstepsr) = rootf.newton(fr, xy0, eps:eps, dx:dxy);
		complex rrootc = new complex(rroot[0], rroot[1]);
		vector frval = fr(rroot);
		complex frvalc = new complex(frval[0], frval[1]);

		// Write
		WriteLine($"Doing real 2D rootfinding of f = z^3 + 5 close to {z0}");
                WriteLine($"Using Jacobi matrix with finite difference approximation");
		WriteLine($"Accuracy goal eps:                  {eps}");
                WriteLine($"Analytical roots:                   {res1} , {res2} , {res3} ");
                WriteLine($"Found root:                         {rrootc}");
                WriteLine($"Deviation:                          {(rrootc-res2).Re:f3}+{(rrootc-res2).Im:f2}i");
                WriteLine($"Value of function at root:          {frvalc}");
		WriteLine($"Number of function calls:	    {fcalls}");
		WriteLine($"Number of rootfinder steps:	    {nstepsr}\n");

		
		// 1D complex rootfinding of f = sin(z)*exp(z), roots z=pi*n + 0i
		Func<complex, complex> fc2 = delegate(complex z)
			{fcalls++; return sin(z)*exp(z);};
		Func<complex, complex> dfc2 = delegate(complex z)
			{fcalls++; return exp(z)*(sin(z)+cos(z));};
		z0 = 1+0.1*i;
		res1 = 0;

		WriteLine("*************\n");

		// Analytical derivative
		fcalls = 0;
		(complex croot, int nstepsc) = rootf.newton(fc2, z0, eps:eps, dz:dz, df:dfc2);

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

		// 2D real rootfinding of f = sin(z)*exp(z), roots z=pi*n + 0i
		Func<vector, vector> fr2 = delegate(vector xy)
		{
			fcalls++; double x = xy[0]; double y = xy[1];
			complex fz = sin(x + i*y)*exp(x + i*y);
			return new vector(fz.Re, fz.Im);
		};
		xy0 = new vector(z0.Re, z0.Im);
		fcalls = 0;
		(rroot, nstepsr) = rootf.newton(fr2, xy0, eps:eps, dx:dxy);
		rrootc = new complex(rroot[0], rroot[1]);
		frval = fr2(rroot);
		frvalc = new complex(frval[0], frval[1]);

		// Write
                WriteLine($"Doing real 2D rootfinding of f = sin(z)*exp(z) close to {z0}");
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
