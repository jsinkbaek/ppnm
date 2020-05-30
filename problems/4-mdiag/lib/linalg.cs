using static System.Console;
using static System.Math;

public class linalg
{
	public class jcbi_cycl
	{// jacobi diagonalisation A=VDV.T with cyclic sweeps inplace
		matrix v; // eigenvectors in matrix V
		vector d; // eigenvalues on diagonal of matrix D, in vector d
		int p, q;
		int sweep;
		bool cond;
		int rotations = 0;
		int n;

		public matrix V{get{return v;}}
		public matrix D{get{return new matrix(d);}}
		public vector Eigvals{get{return d;}}
		public int Rotations{get{return rotations;}}

		public jcbi_cycl(matrix A, bool val_by_val=false, int evalnum=1, bool invert=false)
		{// constructor of jacobi diag object through sweep
			sweep = 0;
			n = A.size1;
			v = new matrix(n, n);
			d = new vector(n);
			
			// diagonal of A, copy into vector d
			for (int i=0; i<n; i++)
			{
				d[i] = A[i, i];
			}

			// set v diagonals to 1, off-diagonals 0
			for (int i=0; i<n; i++)
			{
				v[i, i] = 1.0;
				for (int j=i+1; j<n; j++)
				{
					v[i,j]=0.0; v[j,i]=0.0;
				}
			}
			
			if (val_by_val)
			{
				for (p=0; p<evalnum; p++)
				{
					do {rot_vbv(A, evalnum, invert);} while (cond);
				}
			}
			else
			{
				do {rot_sweep(A);} while (cond);
			}
		}

		private void rot_vbv(matrix A, int evalnum, bool invert, bool val_by_val=true)
		{// value by value variation of cyclic routine
			cond = false;
			rot(A, invert, val_by_val);
		}


		private void rot_sweep(matrix A)
		{
			cond = false; sweep++;
			for (p=0; p<n; p++) rot(A, false);
		}// rotation sweeping cycles


		private void rot(matrix A, bool invert=false, bool val_by_val=false)
		{
			for (q=p+1; q<n; q++)
			{
				// pp starts as A[p,p], qq as A[q,q], pq=A[p,q]
				double pp=d[p], qq=d[q], pq=A[p,q];
				double phi;

				if (invert) phi = Atan2(-2*pq, -qq+pp)/2;
				else phi = Atan2(2*pq, qq-pp)/2;

				double c=Cos(phi), s=Sin(phi);

				// New values
				double ppn = c*c*pp - 2*s*c*pq + s*s*qq;
				double qqn = s*s*pp + 2*s*c*pq + c*c*qq;

				if (ppn != pp || qqn != qq)
				{
					rotations++;
					cond = true;
					d[p] = ppn; 
					d[q] = qqn;
					A[p, q] = 0.0;
					
					// Update rest of matrix elements in upper
					// off-diagonal
					
					if (!val_by_val)
					{// Don't do if calculating vbv, as all the rows above current one are 0
						for (int i=0; i<p; i++)
						{
							double ip=A[i,p], iq=A[i,q];
							A[i, p] = c*ip - s*iq;
							A[i, q] = c*iq + s*ip;
						}
					}
					for (int i=p+1; i<q; i++)
					{
						double pi=A[p,i], iq=A[i,q];
						A[p, i] = c*pi - s*iq;
						A[i, q] = c*iq + s*pi;
					}
					for (int i=q+1; i<n; i++)
					{
						double pi=A[p,i], qi=A[q,i];
						A[p, i] = c*pi - s*qi;
						A[q, i] = c*qi + s*pi;
					}
					for (int i=0; i<n; i++)
					{// update v
						double vip=v[i,p], viq=v[i,q];
						v[i, p] = c*vip - s*viq;
						v[i, q] = c*viq + s*vip;
					}
				}// rotation change check
			}// loop over q
		}// rot()
	}// class jcbi_cycl



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
