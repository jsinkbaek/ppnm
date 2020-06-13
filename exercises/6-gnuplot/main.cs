using System;
using static System.Console;
using static System.Math;

class main
{
	static void Main()
	{
		WriteLine("Exercise 6A");
		WriteLine("See Plot.erf.svg and Plot.gam.svg");
		var erfw = new System.IO.StreamWriter("out.erf.txt");
		var gamw = new System.IO.StreamWriter("out.gam.txt");
		double eps = 1.0/128;
		for (double x=-3; x<=3; x+=0.05)
		{
			erfw.WriteLine($"{x} {erf(x)}");
		}
		for (double x=-5+eps; x<=5-eps; x+=0.01)
		{
			gamw.WriteLine($"{x} {gamma(x)}");
		}
		erfw.Close(); gamw.Close();
	}

	public static double erf(double x)
	{
		if (x<0) return -erf(-x);
		double[] a = {0.254829592,-0.284496736,1.421413741,-1.453152027,1.061405429};
		double t = 1/(1+0.3275911*x);
		double sum = t*(a[0]+t*(a[1]+t*(a[2]+t*(a[3]+t*a[4]))));
		return 1-sum*Exp(-x*x);
	}
	public static double gamma(double x)
	{
		if(x<0)return PI/Sin(PI*x)/gamma(1-x);
		if(x<9)return gamma(x+1)/x;
		double lngamma=x*Log(x+1/(12*x-1/x/10))-x+Log(2*PI/x)/2;
		return Exp(lngamma);
	}
}

