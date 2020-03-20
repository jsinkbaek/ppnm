using static System.Math;
using static System.Console;
using System;
using static gamma;
public class acl
{// Exercise A
public static void A()
{
	// lambda anonymous function
	Func<double, double> f = (x) => Log(x)/Sqrt(x);
	
	double a=0, b=1, result;
	// a and b are limits for the integration
	// acc and eps are absolute and relative accuraccy reqs
	double rf = quad.o8av(f, a, b, acc:1e-6, eps:1e-6);

	Func<double, double> g = (x) => Exp(-x*x);
	double posinf = double.PositiveInfinity;
	double neginf = double.NegativeInfinity;
	double rg = quad.o8av(g, neginf, posinf);

	double e = E;
	Func<double, double> h1 = makelnp(1.0);
	Func<double, double> h2 = makelnp(2.0);
	Func<double, double> h3 = makelnp(3.0);
	Func<double, double> he = makelnp(e);
	
	double rh1 = quad.o8av(h1, a, b);
	double rh2 = quad.o8av(h2, a, b);
	double rh3 = quad.o8av(h3, a, b);
	double rhe = quad.o8av(he, a, b);

	WriteLine($"quad(Log(x)/Sqrt(x)) from 0 to 1 = {rf}");
	WriteLine("should be -4\n");
	
	WriteLine($"quad(exp(-x*x)) from -inf to inf = {rg}");
	WriteLine($"should be Sqrt(PI)={Sqrt(PI)}\n");

	WriteLine($"quad(Pow(Log(1/x), 1)) from 0 to 1 = {rh1}");
	WriteLine($"should be Gamma(2)={Gamma(1.0+1.0).Re}\n");

	WriteLine($"quad(Pow(Log(1/x), 2)) from 0 to 1 = {rh2}");
	WriteLine($"should be Gamma(3)={Gamma(2.0+1.0).Re}\n");

	WriteLine($"quad(Pow(Log(1/x), 3)) from 0 to 1 = {rh3}");
	WriteLine($"should be Gamma(4)={Gamma(3.0+1.0).Re}\n");

	WriteLine($"quad(Pow(Log(1/x), e)) from 0 to 1 = {rhe}");
	WriteLine($"should be Gamma(e+1)={Gamma(e+1.0).Re}\n");
}
static Func<double, double> makelnp(double p)
{// makelnp returns a function ln(1/x)^p with p specified
	Func<double, double> f = (x) => Pow(Log(1.0/x), p);
	return f;
}

}
