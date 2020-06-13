using static System.Math;
using static System.Console;
using System;
public class bcl
{// Exercise B
	static double e = E;
	public static void B()
	{
		double start=0, posinf=double.PositiveInfinity;
		Func<double, double> pow3ex = (x) => Pow(x, 3)/(Exp(x)-1);
		Func<double, double> gauss1 = makegauss(1);
		Func<double, double> gausse = makegauss(e);
		Func<double, double> ex3e42 = makex3ea2(4);
		
		double rp = quad.o8av(pow3ex, start, posinf);
		double rg1 = quad.o8av(gauss1, start, posinf);
		double rge = quad.o8av(gausse, start, posinf);
		double rex = quad.o8av(ex3e42, start, posinf);

		WriteLine($"quad(Pow(x,3)/(Exp(x)-1)) from 0 to inf is {rp}");
		WriteLine($"should be Pow(PI,4)/15={Pow(PI,4)/15}\n");
		WriteLine($"quad(Pow(e,-1*Pow(x,2)) from 0 to inf is {rg1}");
		WriteLine($"should be 0.5*Sqrt(PI/1)={0.5*Sqrt(PI)}\n");
		WriteLine($"quad(Pow(e,-e*Pow(x,2)) from 0 to inf is {rge}");
		WriteLine($"should be 0.5*Sqrt(PI/e)={0.5*Sqrt(PI/e)}\n");
		WriteLine($"quad(Pow(x,3)*Pow(e,-4*Pow(x,2))) from 0 to inf is {rex}");
		WriteLine($"should be 1/(2*Pow(4,2))={1/(2*Pow(4,2))}\n");

	}

	static Func<double, double> makegauss(double a)
	{
		Func<double, double> f = (x) => Pow(e, -a*Pow(x, 2));
		return f;
	}
	
	static Func<double, double> makex3ea2(double a)
	{
		Func<double, double> f = (x) => Pow(x,3)*Pow(e, -a*Pow(x,2));
		return f;
	}

}
