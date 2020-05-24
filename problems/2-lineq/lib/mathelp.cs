public static class mathelp
{
	public static matrix make_random_matrix(int n, int m)
	{
		var rand = new System.Random();
		matrix A = new matrix(n, m);
		for (int i=0; i<n; i++)
		{
			for (int j=0; j<m; j++){A[i, j] = 10*rand.NextDouble();}
		}
		return A;
	}

	public static vector make_random_vector(int n)
	{
		var rand = new System.Random();
		vector v = new vector(n);
		for (int i=0; i<n; i++){v[i] = 5*rand.NextDouble();}
		return v;
	}

	public static matrix make_test_matrix(int n, int m)
	{
		matrix A = new matrix(n, m);
		for (int i=0; i<n; i++)
		{
			for (int j=0; j<m; j++){A[i, j] = i+j*j+1;}
		}
		return A;
	}

	public static vector make_test_vector(int n)
	{
		vector v = new vector(n);
		for (int i=0; i<n; i++){v[i] = i+1;}
		return v;
	}
}
