using static System.Console;
using static System.Math;
using static linalg;
using static mathelp;
using System;
using System.Diagnostics;
using static fit;

class main
{
	static int Main()
	{
		B1();
		B2();
		B3();
		B4();
		B5();

		return 0;
	}

	static void B1()
	{
		WriteLine("\n Problem B1: Test Jacobi cycle time \n");
		int n = 50;
		WriteLine("Symmetric random test matrix A of size {0}", n);
		Stopwatch sw = new Stopwatch();
		
		double time_avg = 0;
		int k = 15;
		vector times = new vector(k);
		for (int i=0; i<k; i++)
		{
			sw.Reset();
			matrix A = rand_sym_mat(n);
			matrix B = A.copy();
			sw.Start();
			jcbi_cycl test = new jcbi_cycl(B);
			sw.Stop();
			times[i] = sw.Elapsed.TotalMilliseconds;
			time_avg += times[i];
		}
		time_avg = time_avg/times.size;
		WriteLine("Average Jacobi diagonalisation time");
		WriteLine("time_avg = {0} ms\n", time_avg);
		
		WriteLine("Perform calculation for different matrix sizes");
		var timewriter = new System.IO.StreamWriter("out.time.txt");
		k = 5; // number of tries for each avg
		int l = 30; // upper range of matrix sizes;
		vector tavgs = new vector(l-5);
		vector xs = new vector(l-5);
		vector terr = new vector(l-5);
		for (int i=5; i<l; i++)
		{
			xs[i-5] =  i*1.0;
			tavgs[i-5] = 0;
			for (int j=0; j<k; j++)
			{
				matrix A = rand_sym_mat(i);
				sw.Reset();
				matrix B = A.copy();
				sw.Start();
				jcbi_cycl test = new jcbi_cycl(B);
				sw.Stop();
				tavgs[i-5] += sw.Elapsed.TotalMilliseconds;
			}
			tavgs[i-5] = tavgs[i-5] / k;
			terr[i-5] = tavgs[i-5] * 0.025;
			timewriter.WriteLine($"{i} {tavgs[i-5]}");
		}
		timewriter.Close();
		Func<double, double>[] tfun = new Func<double,double>[2];
		tfun[0] = (x) => 1; tfun[1] = (x) => x*x*x;
		//tfun[0] = (x) => 1; tfun[1] = (x) => x; tfun[2] = (x) => x*x; tfun[3] = (x) => x*x*x;
		lsfit tfit = new lsfit(xs, tavgs, terr, tfun);
		WriteLine("exec time fit params {0} {1}", tfit.C[0], tfit.C[1]);
		vector fiteval = tfit.evaluate(xs);
		var tfunw = new System.IO.StreamWriter("out.tfun.txt");
		for (int i=0; i<xs.size; i++)
		{
			tfunw.WriteLine($"{xs[i]} {fiteval[i]}");
		}
		tfunw.Close();

		WriteLine("See PlotB1.svg for execution time calculation and a+b*x^3 fit");

	}

	static void B2()
	{
		WriteLine("\n Problem B2\n");
		int n = 8;
		matrix A = rand_sym_mat(n);
		matrix B = A.copy();
		WriteLine("Random symmetric matrix A with size {0}", n);
		WriteLine("Perform eigenvalue by eigenvalue calculation for first 4 values");
		int evalnum = 4;
		bool val_by_val = true;
		jcbi_cycl test_vbv = new jcbi_cycl(B, val_by_val, evalnum);
		Write("Found eigenvalues = ");
		for (int i=0; i<evalnum; i++)
		{
			Write(" {0:g3} ", test_vbv.Eigvals[i]);
		}
		Write("\n");

		((test_vbv.V).T * A * test_vbv.V - test_vbv.D).print("V.T*A*V - D = ", "{0,10:f6}");
		WriteLine("\n Total rotations used = {0}", test_vbv.Rotations);
		B.print("\n Matrix copy used, after value by value operations = ", "{0,10:f6}");

		matrix C = A.copy();
		WriteLine("\nSimilar calculation with cyclic sweeps");
		jcbi_cycl test_cycl = new jcbi_cycl(C);
		test_cycl.Eigvals.print("Found eigenvalues = ");
		(test_cycl.V.T * A * test_cycl.V - test_cycl.D).print("V.T*A*V - D = ", "{0,10:f6}");
		WriteLine("\n Total rotations used = {0}", test_cycl.Rotations);
		C.print("\n Matrix copy used, after cyclic sweeps = ", "{0,10:f6}");
		
	}

	static void B3()
	{
		WriteLine("\n Problem B3\n");
		WriteLine("See PlotB3r.svg and PlotB3t.svg for results");
		int l = 150; // max matrix size
		int k = 5; // number of tries for each avg

		Stopwatch sw = new Stopwatch();
		var timewriter = new System.IO.StreamWriter("out.b3time.txt");
		var rotwriter = new System.IO.StreamWriter("out.b3rot.txt");
		
		vector tvbv = new vector(l);
		vector tcyc = new vector(l);
		vector rvbv = new vector(l);
		vector rcyc = new vector(l);
		for (int i=0; i<l; i++)
		{
			tvbv[i] = 0;
			tcyc[i] = 0;
			rvbv[i] = 0;
			rcyc[i] = 0;
			for (int j=0; j<k; j++)
			{
				matrix A = rand_sym_mat(i+1);
				
				sw.Reset();
				matrix B = A.copy();
				bool vbv = true;
				int evalnum = 1;
				sw.Start();
				jcbi_cycl test_vbv = new jcbi_cycl(B, vbv, evalnum);
				sw.Stop();
				tvbv[i] += sw.Elapsed.TotalMilliseconds;
				rvbv[i] += test_vbv.Rotations;

				matrix C = A.copy();
				sw.Reset();
				sw.Start();
				jcbi_cycl test_cyc = new jcbi_cycl(C);
				sw.Stop();
				tcyc[i] += sw.Elapsed.TotalMilliseconds;
				rcyc[i] += test_cyc.Rotations;
			}
			tvbv[i] = tvbv[i] / k;
			tcyc[i] = tcyc[i] / k;
			rvbv[i] = rvbv[i] / k;
			rcyc[i] = rcyc[i] / k;

			timewriter.WriteLine($"{i} {tvbv[i]} {tcyc[i]}");
			rotwriter.WriteLine($"{i} {rvbv[i]} {rcyc[i]}");
		}
		timewriter.Close();
		rotwriter.Close();
	}
	
	static void B4()
	{
		WriteLine("\n Problem B4\n");
		WriteLine("See PlotB4r.svg and PlotB4t.svg for results");
		int l = 50; // max matrix size
		int k = 5; // number of tries for each avg

		Stopwatch sw = new Stopwatch();
		var timewriter = new System.IO.StreamWriter("out.b4time.txt");
		var rotwriter = new System.IO.StreamWriter("out.b4rot.txt");
		
		vector tvbv = new vector(l);
		vector tcyc = new vector(l);
		vector rvbv = new vector(l);
		vector rcyc = new vector(l);
		for (int i=0; i<l; i++)
		{
			tvbv[i] = 0;
			tcyc[i] = 0;
			rvbv[i] = 0;
			rcyc[i] = 0;
			for (int j=0; j<k; j++)
			{
				matrix A = rand_sym_mat(i+1);
				
				sw.Reset();
				matrix B = A.copy();
				bool vbv = true;
				int evalnum = l;
				sw.Start();
				jcbi_cycl test_vbv = new jcbi_cycl(B, vbv, evalnum);
				sw.Stop();
				tvbv[i] += sw.Elapsed.TotalMilliseconds;
				rvbv[i] += test_vbv.Rotations;

				matrix C = A.copy();
				sw.Reset();
				sw.Start();
				jcbi_cycl test_cyc = new jcbi_cycl(C);
				sw.Stop();
				tcyc[i] += sw.Elapsed.TotalMilliseconds;
				rcyc[i] += test_cyc.Rotations;
			}
			tvbv[i] = tvbv[i] / k;
			tcyc[i] = tcyc[i] / k;
			rvbv[i] = rvbv[i] / k;
			rcyc[i] = rcyc[i] / k;

			timewriter.WriteLine($"{i} {tvbv[i]} {tcyc[i]}");
			rotwriter.WriteLine($"{i} {rvbv[i]} {rcyc[i]}");
		}
		timewriter.Close();
		rotwriter.Close();
	}

	static void B5()
	{
		WriteLine("\n Problem B5\n");

		int n = 5;
		matrix A = rand_sym_mat(n);
		matrix B = A.copy();
		matrix C = A.copy();

		WriteLine($"Random symmetric matrix A with size {n}");
		WriteLine("We find all eigenvalues with cyclic routine");

		jcbi_cycl test_cycl = new jcbi_cycl(B);
		test_cycl.Eigvals.print("Eigenvalues found by cyclic routine, low to high");

		WriteLine("\nNow we use the value-by-value routine to calculate eigenvalues from last to first");
		WriteLine("We do this by changing the calculation of " +
			  "rotation angle theta to 0.5*Atan2(-Apq, App-Aqq)\n");
		
		bool invert = true;
		int evalnum = n;
		bool vbv = true;
		jcbi_cycl test_vbv = new jcbi_cycl(C, vbv, evalnum, invert);

		test_vbv.Eigvals.print("Eigenvalues value-by-value, in order of calculation");	
	}



}
