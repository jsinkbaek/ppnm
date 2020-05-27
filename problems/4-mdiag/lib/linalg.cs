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

		public matrix V{get{return v;}}
		public matrix D{get{return new matrix(d);}}
		public vector Eigvals{get{return d;}}

		public jcbi_cycl(matrix A)
		{// constructor of jacobi diag object through sweep
			sweep = 0;
			int n = A.size1;
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
					V[i,j]=0.0; V[j,i]=0.0;
				}
			}
			
			
			do {rot_sweep(A);} while (cond);
		}


		private void rot_sweep(matrix A)
		{
			cond = false; sweep++;
			int n = A.size1;

			for (p=0; p<n; p++)
			{
				for (q=p+1; q<n; q++)
				{
					// pp starts as A[p,p], qq as A[q,q], pq=A[p,q]
					double pp=d[p], qq=d[q], pq=A[p,q];
					double phi = Atan2(2*pq, qq-pp)/2;
					double c=Cos(phi), s=Sin(phi);

					// New values
					double ppn = c*c*pp - 2*s*c*pq + s*s*qq;
					double qqn = s*s*pp + 2*s*c*pq + c*c*qq;

					if (ppn != pp || qqn != qq)
					{
						cond = true;
						d[p] = ppn; 
						d[q] = qqn;
						A[p, q] = 0.0;
						
						// Update rest of matrix elements in upper
						// off-diagonal
						for (int i=0; i<p; i++)
						{
							double ip=A[i,p], iq=A[i,q];
							A[i, p] = c*ip - s*iq;
							A[i, q] = c*iq + s*ip;
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
			}// loop over p
		}// rotation sweep
	}

}
