using System;
class cmdline
{
	// all parameters after commandline will be converted to string
	// and parsed to Main
	static int Main(string[] args)
	{
		string s;

		do{	
			s = Console.ReadLine();
			
			if(s==null) break;
			
			string [] words = s.Split(' ', ',', '\t');
			
			foreach(var word in words)
			{
				double x = double.Parse(word);
				Console.WriteLine("{0} {1} {2}", x, Math.Sin(x), Math.Cos(x));
			}
			
		}
		while(true);
	
	return 0;
	}
}
