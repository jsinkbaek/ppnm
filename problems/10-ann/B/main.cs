using System;
using static System.Console;
using static System.Math;

class main
{
	static void Main()
	{
		WriteLine("\n--------------------------------------------------------------");
                WriteLine("\nProblem 10B:\n");
                WriteLine("See PlotB.svg for results\n");

		// Generate evenly spaced test data points for Sin(x) in interval [0, 2*PI]
		int m=30;
		double a=0;
		double b=2*PI;
		vector xt = new vector(m);
		vector yt = new vector(m);
		for (int i=0; i<m; i++)
		{
			xt[i] = a+(b-a)*i/(m-1);
			yt[i] = Sin(xt[i]);
		}

		// ANN setup with default gaussian wavelet activation function
		int neurons = 3;
		ann ann4 = new ann(neurons);
		// Train ANN to do linear interpolation
		double acc=1e-5;
		ann4.train_intp(xt, yt, acc:acc);
		// Write
		WriteLine($"Training complete on set of {m} evenly spaced values of Sin(x)");
		WriteLine("Parameters and training feedback can be found in out.err.txt");
		
		// Compare regular, derivative and integral with expected results
		m = 300;
		xt = new vector(m); 
		a = 0; b = 2*PI;
		var writer = new System.IO.StreamWriter("out.reg.txt");
		var writed = new System.IO.StreamWriter("out.der.txt");
		var writei = new System.IO.StreamWriter("out.int.txt");
		string deriv = "derivative";
		string integ = "integral";
		for (int i=0; i<m; i++)
		{
			xt[i] = a + (b-a)*i/(m-1);
			writer.WriteLine($"{xt[i]} {ann4.feedforward(xt[i])} {Sin(xt[i])}");
			writed.WriteLine($"{xt[i]} {ann4.feedforward(xt[i], deriv)} {Cos(xt[i])}");
			writei.WriteLine($"{xt[i]} {ann4.feedforward(xt[i], integ)} {-Cos(xt[i])+Cos(0)}");
		}
		writer.Close(); writed.Close(); writei.Close();

		WriteLine("\n--------------------------------------------------------------\n");

	}

}
