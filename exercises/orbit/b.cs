using System;
using static System.Math;
using static System.Console;
using System.Collections;
using System.Collections.Generic;

public class bcl
{//Exercise B
	public static int B1()
	{
		// Parameters and boundary conditions
		double phia=0, phib=2*PI, eps=0, u00=1, u10=0;
		vector ua = new vector(u00, u10);

		// Generate diff equation function
		Func<double, vector, vector> eqdif = m_eqdif(eps);

		// Solved coordinates create list
		List<double> phis = new List<double>();
		List<vector> us = new List<vector>();
	
		// ODE rk23 solver call
		ode.rk23(eqdif, phia, ua, phib, xlist:phis, ylist:us);	

		// Output file
		using (var outfile = new System.IO.StreamWriter("b1out", append:false))
		{
			for (int i=0; i<phis.Count; i++)
			{
				outfile.WriteLine($"{phis[i]} {us[i][0]}");
			}
		
			WriteLine($"Output given in file b1out");
		}
		return 0;
	}
	

	public static int B2()
	{
		// Parameters and Boundary conditions
		double phia=0, phib=2*PI, eps=0, u00=1, u10=-0.5;
		vector ua = new vector(u00, u10);

		// Generate diff equation function
		Func<double, vector, vector> eqdif = m_eqdif(eps);
		
		// Solved coordinates create list
		List<double> phis = new List<double>();
		List<vector> us = new List<vector>();

		// ODE rk23 solver call
		ode.rk23(eqdif, phia, ua, phib, xlist:phis, ylist:us);

		// Output file
		using (var outfile = new System.IO.StreamWriter("b2out", append:false))
		{
			for (int i=0; i<phis.Count; i++)
			{
				outfile.WriteLine($"{phis[i]} {us[i][0]}");
			}
		
			WriteLine($"Output given in file b2out");
		}

	
		return 0;
	}


	public static int B3()
	{
		// Parameters and Boundary conditions
		double phia=0, phib=30*PI, eps = 0.01, u00 = 1, u10 = -0.5;
		vector ua = new vector(u00, u10);

		// Generate diff equation function
		Func<double, vector, vector> eqdif = m_eqdif(eps);
		
		// Solved coordinates create list
		List<double> phis = new List<double>();
		List<vector> us = new List<vector>();

		// ODE rk23 solver call
		ode.rk23(eqdif, phia, ua, phib, xlist:phis, ylist:us);

		// Output file
		using (var outfile = new System.IO.StreamWriter("b3out", append:false))
		{
			for (int i=0; i<phis.Count; i++)
			{
				outfile.WriteLine($"{phis[i]} {us[i][0]}");
			}
		
			WriteLine($"Output given in file b3out");
		}

		return 0;
	}


	static Func<double, vector, vector> m_eqdif(double eps)
	{// Method to generate equatorial motion differential equation functions with specific parameters
		Func<double, vector, vector> f = delegate(double phi, vector u)
		{
			return new vector(u[1], 1-u[0] + eps*u[0]*u[0]);
		};
		return f;
	}



}
