using static fit;
using static System.Console;
using static System.Math;
using System;

class main
{
	static int Main()
	{
		Func<double, double>[] lnexp = new Func<double, double>[2];
		lnexp[0] = (x) => 1;
		lnexp[1] = (x) => -x;
		double[] t = {1, 2, 3, 4, 6, 9, 10, 13};
		double[] yt = {117, 100, 88, 72, 53, 29.5, 25.2, 15.2};
		vector xs = new vector(t);
		vector lny = new vector(yt.Length);
		vector yerr = new vector(yt.Length);
		vector lnerr = new vector(yt.Length);
		
		for (int i=0; i<yt.Length; i++)
		{
			lny[i] = Log(yt[i]);
			yerr[i] = yt[i] / 20;
			lnerr[i]= yerr[i] / yt[i];
		}

		lsfit fit1 = new lsfit(xs, lny, lnerr, lnexp);
		WriteLine("Trying to fit y=a*Exp(-lambda*x) on log form ln(y) = ln(a)-lambda*x \n");
		vector c = fit1.C;
		vector cerr = fit1.Cerr;
		matrix covar = fit1.Covar;

		c.print("c =  ln(a)   lambda   =  ");
		covar.print("covariance matrix = ");
		double a = Exp(c[0]);
		double aerr = Exp(c[0]) * cerr[0];
		double ml = 1/c[1];
		double hlerr = cerr[1] * Log(2) / ((-c[1])*(-c[1]));
		double hl = Log(2) * ml;
		WriteLine("a = {0:f6} +- {1:f6}", a, aerr);
		WriteLine("lambda = {0:f6} +- {1:f6}\n", c[1], cerr[1]);
		WriteLine("Lambda converted to half life, error found by simplified propagation formula");
		WriteLine("half life = {0:f6} +- {1:f6}", hl, hlerr);
		
		Func<double, double> expfun = x => a * Exp(-c[1]*x);
		Func<double, double> expu = x => (a+aerr) * Exp(-(c[1]-cerr[1])*x);
		Func<double, double> expl = x => (a-aerr) * Exp(-(c[1]+cerr[1])*x);

		vector x_eval = new vector(200);
		for (int i=0; i<x_eval.size; i++)
		{
			x_eval[i] = (double)i / 10;
		}

		var fitwriter = new System.IO.StreamWriter("out.fit.txt");
		var fitupwriter = new System.IO.StreamWriter("out.fitu.txt");
		var fitlowriter = new System.IO.StreamWriter("out.fitl.txt");
		for (int i=0; i<x_eval.size; i++)
		{
			fitwriter.Write("{0:f16} {1:f16}\n", x_eval[i], expfun(x_eval[i]));
			fitupwriter.Write("{0:f16} {1:f16}\n", x_eval[i], expu(x_eval[i]));
			fitlowriter.Write("{0:f16} {1:f16}\n", x_eval[i], expl(x_eval[i]));
		}
		fitwriter.Close();
		fitupwriter.Close();
		fitlowriter.Close();
		
		var datwriter = new System.IO.StreamWriter("out.data.txt");
		for (int i=0; i<xs.size; i++)
		{
			datwriter.Write("{0:f16} {1:f16} {2:f16}\n", xs[i], yt[i], yerr[i]);
		}
		datwriter.Close();
		
		return 0;
	}

}

