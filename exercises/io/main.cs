using static System.Math;
using static System.Console;
class main
{
	static int Main()
	{
		cos_io();
		cos_file();
	return 0;
	}


	static int cos_io()
	{	
		// Standard input stream
		System.IO.TextReader stdin = In;
	
		WriteLine("x	cos(x)\n");

		string s = string.Empty;
		double x = 0;
		double cosx = 0;

		while (true)
		{
			s = stdin.ReadLine();
			
			if (s==null){break;}
			
			x = double.Parse(s);
			cosx = Cos(x);
			WriteLine("{0}	{1}\n", x, cosx);
		}
	return 0;
	}


	static int cos_file()
	{
		// file stream
		var infile = new System.IO.StreamReader("inputfile");
		
		var outfile = new System.IO.StreamWriter("outfile",append:false);
		outfile.WriteLine("x	cos(x)\n");

		string s = string.Empty;
		double x = 0;
		double cosx = 0;

		while (true)
		{
			s = infile.ReadLine();
			
			if (s==null)
			{
				outfile.Close();
				break;
			}
			
			x = double.Parse(s);
			cosx = Cos(x);
			outfile.WriteLine("{0}	{1}\n", x, cosx);
		}
	return 0;
	}
}
