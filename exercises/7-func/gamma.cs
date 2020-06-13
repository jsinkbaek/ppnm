using System;
//using static System.Numerics.Complex;
public class gamma
{
	static int g = 7;
	static double[] p = {0.99999999999980993, 676.5203681218851, -1259.1392167224028,
			     771.32342877765313, -176.61502916214059, 12.507343278686905,
			     -0.13857109526572012, 9.9843695780195716e-6, 1.5056327351493116e-7};
 
	public static complex Gamma(complex z)
	{
    	// Reflection formula
    	if (z.Re < 0.5)
		{
        	return Math.PI / (cmath.sin( Math.PI * z) * Gamma(1 - z));
		}
   	 else
		{
        	z -= 1;
        	complex x = p[0];
        	for (var i = 1; i < g + 2; i++)
			{
            	x += p[i]/(z+i);
			}
        	complex t = z + g + 0.5;
        	return cmath.sqrt(2 * Math.PI) * (cmath.pow(t, z + 0.5)) * cmath.exp(-t) * x;
		}
	}
}
