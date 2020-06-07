using System;
using static System.Math;
using static System.Console;
using System.Collections.Generic;

class main
{
	static List<double> Energy;	// Define here to be reachable by deviation_func
	static List<double> Cross;
	static List<double> Err;

	static void Main()
		{
			WriteLine("\n--------------------------------------------------------------");
			WriteLine("\nProblem 8B:\n");
			WriteLine("minimization.qnewton exit details found in B/out.err.txt");
			WriteLine("See PlotB.svg for plot of result\n");
			// Read file higgs.dat and parse data into separate lists
			Energy = new List<double>();
			Cross = new List<double>();
			Err = new List<double>();

			var fileread = new System.IO.StreamReader("higgs.dat");
			string line;
			do
			{
				line = fileread.ReadLine();
				if (line==null) break;
				string [] data = line.Split();
				Energy.Add(double.Parse(data[0]));
				Cross.Add(double.Parse(data[1]));
				Err.Add(double.Parse(data[2]));
			}
			while (true);
			fileread.Close();
			
			// Starting guess for mass, width, and A in Breit-Wigner formula
			vector x0 = new vector(130, 1, 1);
			double acc = 1e-4;
			// Call minimizer
			(vector x, int nsteps) = minimization.qnewton(deviation_func, x0,
				       					acc:acc);
			double m=x[0];
		        double w=Abs(x[1]); // sign has no influence on function, so we choose positive
			double A=x[2];

			WriteLine("Fitting Higgs data to Breit-Wigner formula");
			WriteLine("by minimizing Chi² deviation function between formula and data.");
			WriteLine($"Found mass:		{m}");
			WriteLine($"Found width:		{w}");
			WriteLine($"Found A:		{A}");
			WriteLine($"Reduced chi² val:	{deviation_func(x)/Energy.Count}");
			WriteLine($"Minimization steps:	{nsteps}");
			WriteLine($"Minimization acc:	{acc}");

			WriteLine("\n--------------------------------------------------------------\n");

			// Write fit data for plot
			var fitwriter = new System.IO.StreamWriter("out.fit.txt");
			double dE = 0.25;
			for (double e=Energy[0]; e<=Energy[Energy.Count-1]; e+=dE)
			{
				double fval = 1/((e-m)*(e-m) + w*w/4);
				fitwriter.WriteLine($"{e}, {A*fval}");
			}
			fitwriter.Close();		
		}// end Main


	public static double deviation_func(
			vector x	// vector with parameters of assumed distribution
			)
	{// Chi² - Sum of square difference between measurement and assumed distribution
	 // Calculates difference between measurement and Breit-Wigner formula
		double m = x[0];	// mass
		double w = x[1];	// width
		double A = x[2];	// proportionality constant

		double sum = 0;
		double fval;		// Value of Breit-Wigner formula
		int n = Energy.Count;

		for (int i=0; i<n; i++)
		{
			double e = Energy[i]; double c = Cross[i]; double err = Err[i];
			fval = 1/((e-m)*(e-m) + w*w/4);
			sum += (c-A*fval) * (c-A*fval)/(err*err);
		}
		return sum;
	} // end deviation_func
} // end class main
