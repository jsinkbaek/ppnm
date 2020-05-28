using static System.Console;
using static System.Math;
using static linalg;
using static mathelp;
using System;

class main
{
	static int Main()
	{
		WriteLine("\n Problem A1: Test Jacobi functionality \n");
		
		var rand = new System.Random();
		int n = rand.Next(1, 15);
		matrix A = rand_sym_mat(n);
		WriteLine("Symmetric random test matrix A");
		A.print("A = ");
		matrix B = A.copy();
		
		jcbi_cycl test = new jcbi_cycl(B);
		WriteLine("\nJacobi diagonalisation A=V*D*V.T");
		matrix V = test.V;
		matrix D = test.D;
		V.print("\nV = ");
		D.print("\nD = ");

		((V.T * A)*V - D).print("\n(V.T*A)*V - D =", "{0,10:f6}");
		

		WriteLine("\n\n Problem A2: Quantum Particle in box \n");
		
		n = 99;
		double s=1.0/(n+1);
		matrix H = new matrix(n, n);
		for (int i=0; i<n-1; i++)
			{
				H[i, i] = -2;
				H[i, i+1] = 1;
				H[i+1, i] = 1;
			}
		H[n-1, n-1] = -2;
		H = H * (-1 / s / s);

		matrix H1 = H.copy();
		jcbi_cycl h_jac = new jcbi_cycl(H1);
		matrix Vh = h_jac.V;
		matrix Dh = h_jac.D;
		vector eigvals = h_jac.Eigvals;

		H.print("\n H = ");
		WriteLine("\n Jacobi diagonalisation H=V*D*V.T");
		Vh.print("\n V = ");
		Dh.print("\n D = ");
		WriteLine("\n");

		// Calculate analytic energies and compare
		WriteLine("Compare first {0} eigvals with analytic energies", n/3);
		for (int i=0; i<n/3; i++)
		{
			double exact = PI*PI*(i+1)*(i+1);
			double calculated = eigvals[i];
			WriteLine("{0} {1:f6} {2:f6}", i, calculated, exact);
		}

		var datw = new System.IO.StreamWriter("out.fun.txt");
		// Write analytic function to file
		//double a = 0.149;
		//double a=0.141;
		double a = Sqrt(2*s);
		int q = 1;
		Func<double, double> psiev = (x) => a*Sin(q*x*PI);
		Func<double, double> psiod = (x) => a*Sin(q*x*PI-PI);
		for (int k=0; k<4; k++)
		{
			vector psinum = Vh.col_toVector(k);
			Func<double, double> psi = pick_analytic_func(a, n, q, psinum);
			datw.WriteLine($"{0} {0}");
			for (double x=0.005; x<1; x+=0.005)
			{
				datw.WriteLine($"{x} {psi(x)/a}");
/*				if (k % 2 == 0)
				{
					datw.WriteLine($"{x} {psi(x)}");
				}
				else
				{
					datw.WriteLine($"{x} {psiod(x)}");
				}
*/			}
			datw.WriteLine($"{1} {0}\n\n");
			q++;
		}	
		
		// Write function plot data to file
		for (int k=0; k<4; k++)
		{
			datw.WriteLine($"{0} {0}");
			for (int i=0; i<n; i++)
			{
				datw.WriteLine($"{(i+1.0)/(n+1)} {Vh[i, k]/a}");
			}
			datw.WriteLine($"{1} {0} \n\n");
		}
		datw.Close();
		return 0;
	}

	static Func<double, double> pick_analytic_func(double a, int n, int q, vector psinum)
	{
		Func<double, double> psi1 = (x) => a*Sin(q*x*PI);
		Func<double, double> psi2 = (x) => a*Sin(q*x*PI-PI);

		double sum1=0;
		double sum2=0;

		for (int i=0; i<n; i++)
		{
			double s = (i+1.0)/(n+1);
			sum1 += Abs(psi1(s)-psinum[i]);
			sum2 += Abs(psi2(s)-psinum[i]);
		}

		if (sum1 < sum2){return psi1;}
		else if (sum2 < sum1){return psi2;}
		else 
		{
			WriteLine("Error determining phase for analytic result, sum1={0}, sum2={1}", sum1, sum2);
			return psi1;
		}
	}

}
