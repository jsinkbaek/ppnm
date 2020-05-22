using static System.Math;
using static System.Console;

class main
{
	static int Main()
	{
		vector[] testdata = dataMaker.readFileToVector("testdata.txt");
		vector x = testdata[0];
		vector y = testdata[1];
		
		double xa, xb, dz, z, intg, yz;
		xa = x[0];
		xb = x[x.size-1];
		dz = 0.01;

		for (z=xa; z<xb; z+=dz)
		{
			intg = interp.linear.integral(x, y, z);
			yz = interp.linear.spline(x, y, z);

			Write("{0:f16} {1:f16} {2:f16}\n", z, yz, intg);
		
		}
		return 0;

	}
}
