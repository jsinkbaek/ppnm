using static System.Console;
using static System.Math;

public class linalg
{
	public class qr_decomp_GS
	{
		matrix q;
		matrix r;

		public matrix Q{get{return q;}}
		public matrix R{get{return q;}}
		
		public qr_decomp_GS(matrix A)
		{
			int n = A.size1;
			int m = A.size2;

			q = A.copy();
			r = new matrix(m, m);
			vector qi = new vector(n);
			vector qj = new vector(m);

			for (int i=0; i<m; i++)
			{
				qi = q.col_toVector(i);	
				r[i, i] = qi.norm();

				for (int k=0; k<n; k++)
				{
					q[k, i] = q[k, i]/r[i, i];
				}

				for (int j=i+1; j<m; j++)
				{
					qi = q.col_toVector(i);
					qj = q.col_toVector(j);
					r[i, j] = qi.dot(qj);

					for (int k=0; k<n; k++)
					{
						q[k, j] = q[k, j] - q[k, i]*r[i, j];
					}
				}
			}
		}
	
		public vector solve(vector b)
		{// in place replacement of c with x
			vector x = q.Transpose()*b;
			backsubstitution(r, x);
			return x;
		}

		public void backsubstitution(matrix U, vector y)
		{
			y[y.size-1] = y[y.size-1]/U[y.size-1, y.size-1];

			for (int i = y.size-2; i >= 0; i--)
			{
				double sum = 0;
				for (int k = i+1; k < x.size; k++)
				{
					sum += U[i, k] * y[k];
				}
				
				y[i] = (y[i] - sum) / U[i, i];
			}

		}

	}


}
