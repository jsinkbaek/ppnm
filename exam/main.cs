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
		complex i = new complex(0, 1);
		// Complex rootfinding of f = z^3 + 5
		int fcalls = 0;
		Func<complex, complex> fc = delegate(complex z)
		{
			fcalls++;
			return z*z*z +5;
		};
		complex z0 = new complex(0.5, 0.1);

		// Complex rootfinder call
		double eps = 1e-3; complex dz = 1e-7 * (1+i);
		(complex croot, int nstepsc) = rootf.newton(fc, z0, eps:eps, dz:dz);
		complex five = new complex(5, 0);
		complex minusfive = new complex(-5, 0);
		complex minusone = new complex(-1, 0);
		complex res1 = -five.pow(1.0/3); 
		complex res2 = minusfive.pow(1.0/3);
		complex res3 = minusone.pow(2.0/3)*five.pow(1.0/3);
		
		// Write
		WriteLine($"Doing complex 1D rootfinding of f = z^3 + 5 close to {z0}");
		WriteLine($"Accuracy goal eps:                  {eps}");
                WriteLine($"Analytical roots:                   {res1} , {res2} , {res3} ");
                WriteLine($"Found root:                         {croot}");
                WriteLine($"Deviation:                          {(croot-res2).Re:f3}+{(croot-res2).Im:f2}i");
                WriteLine($"Value of function at root:          {fc(croot)}");
		WriteLine($"Number of function calls:	    {fcalls}");
		WriteLine($"Number of rootfinder steps:	    {nstepsc}\n");

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
                WriteLine($"Accuracy goal eps:                  {eps}");
                WriteLine($"Analytical roots:                   {res1} , {res2} , {res3} ");
                WriteLine($"Found root:                         {rrootc}");
                WriteLine($"Deviation:                          {(rrootc-res2).Re:f3}+{(rrootc-res2).Im:f2}i");
                WriteLine($"Value of function at root:          {frvalc}");
		WriteLine($"Number of function calls:	    {fcalls}");
		WriteLine($"Number of rootfinder steps:	    {nstepsr}\n");


		
		
		// Test rootfinders for increasing power p
		var writerz = new System.IO.StreamWriter("out.z.txt");
		var writerxy = new System.IO.StreamWriter("out.xy.txt");
		Stopwatch sw = new Stopwatch();
		for (double p = 1; p < 1000; p+=1)
		{
			// Make base functions, and add call counters to them
			Func<complex, complex> zp_base = makezp(p);
			Func<complex, complex> zp = delegate(complex z)
			{
				fcalls++;
				return zp_base(z);
			};
			Func<vector, vector> xyp_base = makexyp(p);
			Func<vector, vector> xyp = delegate(vector xy)
			{
				fcalls++;
				return xyp_base(xy);
			};
			// Reset stopwatch
			sw.Reset();
			// Starting points and expected root
			complex res = minusfive.pow(1.0/p);
			complex startc = res*1.1 + 0.2;
			vector startv = new vector(startc.Re, startc.Im);
			double dxy1 = 1e-7; complex dz1 = 1e-7 * (1+i);
			// Complex 1D rootf call
			fcalls = 0;
			sw.Start();
			(complex zroot, int nsteps1) = rootf.newton(zp, startc, eps:eps, dz:dz1);
			sw.Stop();
			double time1 = sw.Elapsed.TotalMilliseconds;
			int fcalls1 = fcalls;
			// 2D real rootf call
			fcalls = 0;
			sw.Reset(); sw.Start();
			(vector xyroot, int nsteps2) = rootf.newton(xyp, startv, eps:eps, dx:dxy1);
			sw.Stop();
			double time2 = sw.Elapsed.TotalMilliseconds;
			complex xyrootc = new complex(xyroot[0], xyroot[1]);
			int fcalls2 = fcalls;
			// Write
			writerz.WriteLine($"{p} {fcalls1} {nsteps1} {time1} {abs(zroot-res)}");
			writerxy.WriteLine($"{p} {fcalls2} {nsteps2}  {time2} {abs(xyrootc-res)}");

		}
		writerz.Close();
		writerxy.Close();
		WriteLine("\nIn Plot.calls.svg, rootfinding efficiency for functions f(z) = z^p + 5 is compared.");
		WriteLine("Here, starting conditions for z is generated as z0=1.1*(-5).pow(p)+0.2.");
		WriteLine("Then, it is expected that the rootfinder finds the root (-5).pow(p).");
		WriteLine("Shown is both: The amount of function (f(z)) calls necessary to find the root,");
		WriteLine("and the amount of loop iterations required.");
		WriteLine("Function calls and loop iterations scales linearly with function power p in both cases.");
		WriteLine("But the 1D complex rootfinder makes fewer function calls than the multi-variable one.");
		
		WriteLine("\nIn Plot.time.svg, execution time is compared for the two. Here, both also scale");
		WriteLine("linearly, but the 1D complex rootfinder is significantly faster. Most likely");
		WriteLine("because it is more streamlined given its loss of generality.");		

		WriteLine("\n----------------------------------------------------------------\n");
	}


	static Func<complex, complex> makezp(double p)
	{
		Func<complex, complex> zp = (z) => z.pow(p) + 5;
		return zp;
	}
	static Func<vector, vector> makexyp(double p)
	{
		Func<vector, vector> xyp = delegate(vector xy)
		{
			complex z = new complex(xy[0], xy[1]);
			complex zp = z.pow(p) + 5;
			return new vector(zp.Re, zp.Im);
		};
		return xyp;
	}
}
