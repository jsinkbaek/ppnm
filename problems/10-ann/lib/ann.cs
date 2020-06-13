using static System.Math;
using static System.Console;
using System;
using System.Collections.Generic;

public class ann
{// Artificial neural network class
	int n;				// number of hidden neurons
	Func<double, double> f;		// activation function
	Func<double, double> fd;	// activation function derivative
	Func<double, double> fi;	// activation function integral
	List<vector> pars;		// parameters of the network (ai, bi, wi)
	double startpoint;		// starting point of interval (to calculate integral)
	public List<vector> Params{get{return pars;}}

	Func<double, string, double> gws = delegate(double x, string version) {return gauss_wavelet(x, version);};

	public ann(
			int nNeurons, 				// number of hidden neurons
			Func<double, string, double> acf=null	// activation function (with derivative and integral)
			)
	{
		acf ??= gws; // make acf gaussian wavelet if acf=null
		
		f = delegate(double x) {return acf(x, "regular");};
		fd = delegate(double x) {return acf(x, "derivative");};
		fi = delegate(double x) {return acf(x, "integral");};
		n = nNeurons;
		Error.WriteLine($"ANN initialized with {n} neurons");

		pars = new List<vector>();
		for (int i = 0; i<n; i++)
		{// Create initial parameter values
			vector p = new vector(3);
			p[0] = 1; p[1] = 1; p[2] = 1;
			pars.Add(p);
		}
	}

	public double feedforward(double x, string version="regular", double x0=double.NaN)
		{
		if (double.IsNaN(x0)) x0 = startpoint;	// if x0 not provided, set it to startpoint
		double sum = 0;				// sum of neuron outputs using activation sum
		for (int i=0; i<n; i++)
		{
			double a = pars[i][0];
			double b = pars[i][1];
			double w = pars[i][2];
			if (string.Equals(version, "regular")) sum += w * f((x-a)/b);
			else if (string.Equals(version, "derivative")) sum += w/b * fd((x-a)/b);
			else if (string.Equals(version, "integral")) 
			{
				sum += b*w * fi((x-a)/b);
				sum -= b*w * fi((x0-a)/b);
			}
			else 
			{
				Error.WriteLine($"feedforward unknown version= {version}");
				sum += w* f((x-a)/b);
			}
		}
		return sum;
	}// end feedforward
	public double feedforward(double x, vector pvec)
	{// Overload to use with minimization training
		double sum=0;
		for (int i=0; i<n; i++)
		{
			double a = pvec[3*i+0];
			double b = pvec[3*i+1];
			double w = pvec[3*i+2];
			sum += w * f((x-a)/b);
		}
		return sum;
	}
	
	public void train_intp(vector x, vector y, double acc=1e-3, bool restart=false, int maxsteps=3000)
	{// Trains the ann to interpolate the given table {x,y}
		Error.WriteLine("\nTraining started for ANN to do interpolation");
		vector pvec = new vector(3*n);
		startpoint = x[0];
		int ncalls = 0;
		int nsteps = 0;
		for (int i=0; i<n; i++)
		{// space out the a values prior to training
			pars[i][0] = x[0] + (x[x.size-1] - x[0]) * i /(n-1);
			if (restart)
			{
				pars[i][1] = 1;
				pars[i][2] = 1;
			}
		}
		
		// Copy pars values into pvec
		save_tovector(ref pvec);
		// Function delegate to feed minimizer
		Func<vector, double> dev = delegate(vector p)
		{ncalls++; return deviation(x, y, p);};
		// Minimizer call
		(pvec, nsteps) = minimization.qnewton(dev, pvec, acc:acc, limit:maxsteps);
		// Save new values to pars
		save_topars(pvec);
		// Write complete
		Error.WriteLine("Training complete");
		Error.WriteLine($"ncalls = {ncalls}");
		Error.WriteLine($"nsteps = {nsteps}");
		Error.WriteLine("Parameters for the network are:");
		for (int i=0; i<n; i++)
		{
			Error.WriteLine($"i={i}, a={pars[i][0]}, b={pars[i][1]}, w={pars[i][2]}");
		}
	}
		
	private static double gauss_wavelet(double x, string version="regular")
	{// Evaluates gaussian wavelet function
		if (string.Equals(version, "regular")) return x*Exp(-x*x);
		else if (string.Equals(version, "derivative")) return (1-2*x*x)*Exp(-x*x);
		else if (string.Equals(version, "integral")) return -0.5 * Exp(-x*x);
		else {Error.WriteLine("gauss_wavelet unknown version"); return x*Exp(-x*x);}
	}

	private double deviation(vector x, vector y, vector pvec)
	{// Evaluates ann in x and calculates result deviation from y
		double sum=0;
		for (int i=0; i<x.size; i++)
		{
			double ff = feedforward(x[i], pvec);
			sum += (ff - y[i]) * (ff - y[i]);
		}
		return sum;
	}

	private void save_topars(vector p)
	{
		for (int i=0; i<n; i++)
		{
			pars[i][0] = p[3*i+0];
			pars[i][1] = p[3*i+1];
			pars[i][2] = p[3*i+2];	
		}
	}

	private void save_tovector(ref vector p)
	{
		for (int i=0; i<n; i++)
		{
			p[3*i+0] = pars[i][0];
			p[3*i+1] = pars[i][1];
			p[3*i+2] = pars[i][2];	
		}
	}

}
