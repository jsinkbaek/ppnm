public static class interp
{

	public static int binsearch(vector x, double z)
	{/* Locates interval start for z by bisection */
		int i=0, j=x.size-1;
	
		while (j-i>1)
		{
			int mid=(i+j)/2;
			if (z>x[mid]) i=mid;
			else j=mid;
		}
	
		return i;
	}


	public static class linear
	{

		public static vector spline_run(vector x, vector y, vector z)
		{/* Run spline interpolation on a set of new points z */
			vector yz = new vector(z.size);
			for (int i = 0; i < z.size-1; i++)
			{
				yz[i] = spline(x, y, z[i]);
			}
			return yz;
		}

		
		public static double spline(vector x, vector y, double z)
		{/* Perform linear spline interpolation of points at point z */
			double dy, dx, p, xi, yi, yz;
		
			int idx = binsearch(x, z);
			xi = x[idx]; yi = y[idx];
			dx = x[idx+1] - xi; dy = y[idx+1] - yi;
			p = dy/dx;

			yz = yi + p*(z - xi);
			return yz;	
		}


		public static double integral(vector x, vector y, double z)
		{/* Integrate a linear spline analytically from x[0] to z*/
			double intg, dx, p, xi, yi, pz, dz;
			intg = 0;
			
			int idx = binsearch(x, z);

			for (int i = 0; i < idx; i++)
			{
				xi=x[i]; yi=y[i];

				dx = x[i+1]-xi;
				p = (y[i+1]-yi)/dx;
				intg += yi*dx + 0.5*p*dx*dx;
			}

			pz = (y[idx+1]-y[idx]) / (x[idx+1]-x[idx]);
			dz = z - x[idx];
			intg += y[idx]*dz + 0.5*pz*dz*dz;

			return intg;
		}
	
	}


	public static class quad
	{
		public static (vector, vector) spline(vector x, vector y)
		{
			int n = x.size;
			vector b = new vector(n-1);
			vector c = new vector(n-1);
			vector p = new vector(n-1);
			vector dx = new vector(n-1);
			vector dy = new vector(n-1);

			for (int i=0; i<n-1; i++)
			{
				dx[i] = x[i+1]-x[i];
				dy[i] = y[i+1]-y[i];
				p[i] = dy[i] / dx[i];
			}

			// Recursion forwards, first pass of c
			c[0] = 0;
			for (int i=0; i<c.size-1; i++)
			{
				c[i+1] = (p[i+1] - p[i] - c[i]*dx[i])  / dx[i+1];
			}

			// Recursion backwards, second pass to average c
			c[c.size-1] /= 2;
			for (int i=c.size-2; i>=0; i--)
			{
				c[i] = (p[i+1] - p[i] - c[i+1]*dx[i+1]) / dx[i];
			}

			// Calculate b values
			for (int i=0; i<n-1; i++)
			{
				b[i] = p[i] - c[i]*dx[i];
			}

			return (b, c);
		}

		public static double spline_eval(vector x, vector y, double z)
		{
			double yz, dx;

			(vector b, vector c) = quad.spline(x, y);

			int idx = binsearch(x, z);
			dx = z - x[idx];

			yz = y[idx] + b[idx]*dx + c[idx]*dx*dx;

			return yz;
		}

		public static vector spline_run(vector x, vector y, vector z)
		{
			int idx; double dx;
			vector yz = new vector(z.size);

			(vector b, vector c) = quad.spline(x, y);

			for (int i = 0; i < z.size-1; i++)
			{
				idx = binsearch(x, z[i]);
				dx = x[idx+1] - x[idx];
				yz[i] = y[idx] + b[idx]*dx + c[idx]*dx*dx;
			}

			return yz;
		}

		public static double integral(vector x, vector y, double z)
		{
			int idx = binsearch(x, z);
			double dx;
			double intg = 0;

			(vector b, vector c) = quad.spline(x, y);
			
			for (int i=0; i < idx; i++)
			{
				dx = x[i+1]-x[i];
				intg += y[i]*dx + 0.5*b[i]*dx*dx + 1.0/3*c[i]*dx*dx*dx;
			}

			dx = z - x[idx];
			intg += y[idx]*dx + 0.5*b[idx]*dx*dx + 1.0/3*c[idx]*dx*dx*dx;

			return intg;
		}

		public static double derivative(vector x, vector y, double z)
		{
			int idx = binsearch(x, z);
			(vector b, vector c) = quad.spline(x, y);
			double dx = z - x[idx];
			double deriv = b[idx] + 2*c[idx]*dx;

			return deriv;
		}

	}
}
