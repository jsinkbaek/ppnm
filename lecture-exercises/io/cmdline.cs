using System;
class cmdline
{
	// all parameters after commandline will be converted to string
	// and parsed to Main
	static int Main(string[] args)
	{
		foreach(var s in args)
		{
			double x = double.Parse(s);
			Console.WriteLine("{0} {1} {2}", x, Math.Sin(x), Math.Cos(x));
		}
	
	return 0;
	}
}
