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
		vector ys = new vector(yt);
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
		
		vector c = fit1.C;
		WriteLine("\n Data:");
		xs.print("time in days = ");
		ys.print("y = ");
		WriteLine("\n Attempting to make fit y=a*Exp(-lambda*t) by using ln(y)=ln(a)-lambda*t");
		c.print("c =   ln(a)   lambda   = ");
		WriteLine("a = {0:f6}", Exp(c[0]));
		WriteLine("half life = {0:f6}\n", Log(2)*1/c[1]);
		
		Func<double, double> fit1fun = x => c[0]*lnexp[0](x) + c[1]*lnexp[1](x);

		var fitwriter = new System.IO.StreamWriter("out.fit.txt");
		for (double x=0; x<20; x+=0.1)
		{
			fitwriter.Write("{0:f16} {1:f16}\n", x, Exp(fit1fun(x)));
		}
		fitwriter.Close();

		var datwriter = new System.IO.StreamWriter("out.data.txt");
		for (int i=0; i<xs.size; i++)
		{
			datwriter.Write("{0:f16} {1:f16} {2:f16}\n", xs[i], yt[i], yerr[i]);
		}
		datwriter.Close();
		
		return 0;
	}

}

