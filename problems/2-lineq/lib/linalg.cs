using static System.Console;
using static System.Math;

public class linalg
{

	public class qr_decomp_GS
	{
		matrix q;
		matrix r;

		public matrix Q{get{return q;}}
		public matrix R{get{return r;}}
		
		public qr_decomp_GS(matrix A)
		{
			int n = A.size1;
			int m = A.size2;

			q = A.copy();
			r = new matrix(m, m);
			vector qi = new vector(n);
			vector qj = new vector(n);

			for (int i=0; i<m; i++)
			{
				qi = q.col_toVector(i);	
				r[i, i] = Sqrt(qi.dot(qi));

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
			vector x = q.T*b;
			backsubstitution(r, x);
			return x;
		}

		public void backsubstitution(matrix U, vector y)
		{
			y[y.size-1] = y[y.size-1]/U[y.size-1, y.size-1];

			for (int i = y.size-2; i >= 0; i--)
			{
				double sum = 0;
				for (int k = i+1; k < y.size; k++)
				{
					sum += U[i, k] * y[k];
				}
				
				y[i] = (y[i] - sum) / U[i, i];
			}

		}

		public matrix inverse()
		{
			matrix I = new matrix(q.size2, q.size2);
			I.set_identity();
			matrix B = new matrix(q.size1, q.size2);
			for (int i=0; i < q.size2; i++)
			{
				vector ei = I.col_toVector(i);
				vector x = solve(ei);

				for (int j=0; j<B.size1; j++) {B[j, i] = x[j];}
			}	
			return B;
		}

	}


}
