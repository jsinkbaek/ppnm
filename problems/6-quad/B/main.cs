extern alias quadmlib;
using System;
using static System.Console;
using static System.Math;

class main
{
	static void Main()
	{
		WriteLine("\n-------------------------------------");
		WriteLine("-------------------------------------");
		

		// Problem B1-2
		WriteLine("\nProblem B:\n");
		
		WriteLine("\nTest Clenshaw-Curtis variable transformation");
		// Integral 1
		// function and interval
		int fcalls = 0;
		Func<double, double> f1 = (x) => {fcalls++; return 1/Sqrt(x);};
		double a = 0, b = 1;
		// integrator call o8a
		(double itg1, double err1, int nc1) = quad.o8a(f1, a, b);
		// integrator call o8a with CC transformation
		(double itg1cc, double err1cc, int nc1cc) = quad.o8a(f1, a, b, SubstitutionType:"clenshaw-curtis");
		// integrator call o8av matlib
		fcalls = 0;
		double itg1_o8av = quadmlib.quad.o8av(f1, a, b);
		int nc1_o8av = ((fcalls-8)*2)/8 + 1;	// as function calls = 8+(n-1)*8/2 (with n integrator calls)

		// Write results
		Write($"\nIntegrating 1/Sqrt(x). acc:1e-6. eps:1e-6\n");
		Write("Without any transformation:\n");
		Write($"Result:                            {itg1}\n");
		Write($"Analytical result:                 {2}\n");
		Write($"Deviation:                         {itg1 - 2}\n");
		Write($"Estimated error:     		   {err1}\n");
		Write($"Integrator calls:      		   {nc1}\n");
		
		Write("\nWith the Clenshaw Curtis transformation:\n");
		Write($"Result:                            {itg1cc}\n");
		Write($"Analytical result:                 {2}\n");
		Write($"Deviation:                         {itg1cc - 2}\n");
		Write($"Estimated error:       		   {err1cc}\n");
		Write($"Integrator calls:	           {nc1cc}\n");
		
		Write("\nIn comparison, the matlib o8av rutine:\n");
		Write($"Result:				   {itg1_o8av}\n");
		Write($"Deviation:                         {itg1_o8av - 2}\n");
		Write($"Integrator calls:	           {nc1_o8av}\n");
		
		WriteLine("\n--------------------------------------------------------------");
		
		
		// Integral 2
		// function and interval
		Func<double, double> f2 = (x) => {fcalls++; return Log(x)/Sqrt(x);};
		a = 0; b = 1;
		// integrator call o8a
		(double itg2, double err2, int nc2) = quad.o8a(f2, a, b, acc:1e-5);
		// integrator call o8a with CC transformation
		(double itg2cc, double err2cc, int nc2cc) = quad.o8a(f2, a, b, SubstitutionType:"clenshaw-curtis",
				acc:1e-5);
		// integrator call o8av matlib
		fcalls = 0;
		double itg2_o8av = quadmlib.quad.o8av(f2, a, b, acc:1e-5);
		int nc2_o8av = ((fcalls-8)*2)/8 + 1;
		
		// Write results
		Write($"\nIntegrating Log(x)/Sqrt(x). acc:1e-5. eps:1e-6\n");
		Write("Without any transformation:\n");
		Write($"Result:                            {itg2}\n");
		Write($"Analytical result:                 {-4}\n");
		Write($"Deviation:                         {itg2 + 4}\n");
		Write($"Estimated error:	           {err2}\n");
		Write($"Integrator calls:      		   {nc2}\n");
		
		Write("\nWith the Clenshaw Curtis transformation:\n");
		Write($"Result:                            {itg2cc}\n");
		Write($"Analytical result:                 {-4}\n");
		Write($"Deviation:                         {itg2cc + 4}\n");
		Write($"Estimated error:	           {err2cc}\n");
		Write($"Integrator calls:	           {nc2cc}\n");
		
		Write("\nIn comparison, the matlib o8av rutine:\n");
		Write($"Result:				   {itg2_o8av}\n");
		Write($"Deviation:                         {itg2_o8av + 4}\n");
		Write($"Integrator calls:	           {nc2_o8av}\n");
		
		WriteLine("\n--------------------------------------------------------------");
		

		// Integral 3
		// function and interval
		Func<double, double> f3 = (x) => {fcalls++; return 4*Sqrt(1-x*x);};
		a = 0; b = 1; double acc=1e-14; double eps=1e-14;
		// integrator call o8a
		(double itg3, double err3, int nc3) = quad.o8a(f3, a, b, acc:acc, eps:eps);
		// integrator call o8a with CC transform
		(double itg3cc, double err3cc, int nc3cc) = quad.o8a(f3, a, b, SubstitutionType:"clenshaw-curtis",
				acc:acc, eps:eps);
		// integrator call o8av matlib
		fcalls = 0;
		double itg3_o8av = quadmlib.quad.o8av(f3, a, b, acc:acc, eps:eps);
		int nc3_o8av = ((fcalls-8)*2)/8 + 1;

		// Write results
		Write($"\nIntegrating 4*Sqrt(1-x*x). acc:1e-14. eps:1e-14\n");
		Write("Without any transformation:\n");
		Write($"Result:                            {itg3:f16}\n");
		Write($"Analytical result:                 {PI:f16}\n");
		Write($"Deviation:                         {(itg3 - PI)}\n");
		Write($"Estimated error:     		   {err3}\n");
		Write($"Integrator calls:      		   {nc3}\n");
		
		Write("\nWith the Clenshaw Curtis transformation:\n");
		Write($"Result:                            {itg3cc:f16}\n");
		Write($"Analytical result:                 {PI:f16}\n");
		Write($"Deviation:                         {(itg3cc - PI)}\n");
		Write($"Estimated error:       		   {err3cc}\n");
		Write($"Integrator calls:	           {nc3cc}\n");
		
		Write("\nIn comparison, the matlib o8av rutine:\n");
		Write($"Result:				   {itg3_o8av:f16}\n");
		Write($"Deviation:                         {(itg3_o8av - PI)}\n");
		Write($"Integrator calls:	           {nc3_o8av}\n");
	}
}
