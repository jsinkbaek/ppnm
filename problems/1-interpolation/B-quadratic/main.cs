using static System.Math;
using static System.Console;

class main
{
	static int Main()
	{
		vector[] testdata = dataMaker.readFileToVector("testdata.txt");
		vector x = testdata[0];
		vector y = testdata[1];
		
		double xa, xb, dz, z, deriv, intg, yz;
		xa = x[0];
		xb = x[x.size-1];
		dz = 0.01;
		

		for (z=xa; z<xb; z+=dz)
		{
			intg = interp.quad.integral(x, y, z);
			yz = interp.quad.spline_eval(x, y, z);
			deriv = interp.quad.derivative(x, y, z);
			Write("{0:f16} {1:f16} {2:f16} {3:f16}\n", z, yz, intg, deriv);
			//Write("{0}\n", deriv);
		}
		return 0;

	}
}
