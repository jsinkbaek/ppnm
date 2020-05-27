public static class mathelp
{
	public static matrix rand_sym_mat(int n)
	{
		var rand = new System.Random();
		matrix A = new matrix(n, n);
		for (int i=0; i<n; i++)
		{
			A[i,i] = rand.NextDouble();
		}
		for (int i=0; i<n; i++)
		{
			for (int j=i+1; j<n; j++)
			{
				double aij = rand.NextDouble();
				A[i, j] = aij;
				A[j, i] = aij;
			}
		}
		return A;
	}


}
