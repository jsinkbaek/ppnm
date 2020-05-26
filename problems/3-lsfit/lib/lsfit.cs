using static linalg;
using System;
using static System.Math;

public class fit
{
	public class lsfit
	{
		vector c;
		vector cerr;
		matrix covar;
		matrix q;
		matrix r;
		Func<double, double>[] fun;

		public vector C{get{return c;}}
		public matrix Q{get{return q;}}
		public matrix R{get{return R;}}
		public vector Cerr{get{return cerr;}}
		public matrix Covar{get{return covar;}}

		public lsfit(vector x, vector y, vector yerr, Func<double, double>[] fs)
		{
			int dsize = x.size;
			int nf = fs.Length;
			matrix A = new matrix(dsize, nf);
			vector b = new vector(dsize);
			fun = fs;

			for (int i=0; i<dsize; i++)
			{
				b[i] = y[i] / yerr[i];
				for (int k=0; k<nf; k++)
				{
					A[i, k] = fs[k](x[i]) / yerr[i];
				}
			}

			qr_decomp_GS decomp = new qr_decomp_GS(A);
			q = decomp.Q;
			r = decomp.R;

			c = decomp.solve(b);

			qr_decomp_GS decomp_covar = new qr_decomp_GS(A.T * A);
			covar = decomp_covar.inverse();
			cerr = new vector(covar.size1);
			for (int i=0; i<covar.size1; i++)
			{
				cerr[i] = Sqrt(covar[i, i]);
			}
		}

		public vector evaluate(vector xs)
		{
			vector ys = new vector(xs.size);
			for (int i=0; i<xs.size; i++)
			{
				ys[i] = 0;
				for (int k=0; k<c.size; k++)
				{
					ys[i] += c[k]*fun[k](xs[i]);
				}
			}
			return ys;

		}

		public vector upper(vector xs)
		{
			vector ys = new vector(xs.size);
			for (int i =0; i<xs.size; i++)
			{
				ys[i] = 0;
				for (int k=0; k<c.size; k++)
				{
					ys[i] += (c[k]+cerr[k])*fun[k](xs[i]);
				}

			}
			return ys;
		}

		public vector lower(vector xs)
		{
			vector ys = new vector(xs.size);
			for (int i =0; i<xs.size; i++)
			{
				ys[i] = 0;
				for (int k=0; k<c.size; k++)
				{
					ys[i] += (c[k]-cerr[k])*fun[k](xs[i]);
				}

			}
			return ys;
		}

	}


}
