using System;
using static System.Math;
using static System.Console;

class main
{
	static void Main()
	{
		WriteLine("\n--------------------------------------------------------------");
                WriteLine("\nProblem 10A:\n");
		WriteLine("See PlotA.svg for results\n");

		// Generate evenly spaced test data points for Cos(x) in interval [0, 2PI]
		int m = 30;
		double a = 0;
		double b = 2*PI;
		vector xt = new vector(m);
		vector yt = new vector(m);
		for (int i=0;i<m; i++)
		{
			xt[i] = a + (b-a)*i/(m-1);
			yt[i] = Cos(xt[i]);
		}

		// ANN setup with default gaussian wavelet activation function
		int neurons = 3;
		ann ann4 = new ann(neurons);
		// Train ANN to do linear interpolation
		double acc = 1e-5;
		ann4.train_intp(xt, yt, acc:acc);
		// Write to output file
		WriteLine($"Training completed on set of {m} evenly spaced values of Cos(x)");
		WriteLine($"Parameters and training results can be found in out.err.txt");
		var writeo = new System.IO.StreamWriter("out.train.txt");
		for (int i=0; i<m; i++)
		{
			writeo.WriteLine($"{xt[i]} {ann4.feedforward(xt[i])} {yt[i]}");
		}
		writeo.Close();

		// See how ann fares on a new set
		m = 300;
		xt = new vector(m);
		yt = new vector(m);
		a = 0; b = 2*PI;
		var writeo1 = new System.IO.StreamWriter("out.test.txt");
		for (int i=0; i<m; i++)
		{
			xt[i] = a + (b-a)*i/(m-1);
			yt[i] = Cos(xt[i]);
			writeo1.WriteLine($"{xt[i]} {ann4.feedforward(xt[i])} {yt[i]}");
		}
		writeo1.Close();

		WriteLine("\n--------------------------------------------------------------");
	}

}
