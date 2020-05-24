using static System.Console;
using static System.Math;
using static linalg;
using static mathelp;

class main
{
	static int Main()
	{
		A1();
		A2();

		return 0;
	}

	static void A1()
	{
		WriteLine("Problem A1");
		var rand = new System.Random();
		int m = 2 + rand.Next(6);
		int n = m + rand.Next(6);

		WriteLine("Random tall matrix A with random dimensions");
		matrix A = make_random_matrix(n, m);
		A.print("A = ");

		qr_decomp_GS decomposition = new qr_decomp_GS(A);
		matrix Q = decomposition.Q;
		matrix R = decomposition.R;

		Q.print("Q = ");
		R.print("R = ");
		
		(Q.transpose() * Q).print("Q^T Q = ");
		(Q*R - A).print("Q*R-A");
		WriteLine("R Size1: {0}, Size2: {1}", R.size1, R.size2);	
	}

	static void A2()
	{
		WriteLine("Problem A2");
		var rand = new System.Random();
		int n = 2 + rand.Next(20);
		matrix A = make_random_matrix(n, n);
		vector b = make_random_vector(n);
		
		WriteLine("Random square matrix A:");
		A.print("A = ");

		WriteLine("Random vector b:");
		b.print("b = ");

		qr_decomp_GS decomposition = new qr_decomp_GS(A);
		vector x = decomposition.solve(b);

		x.print("Solution x = ");
		(A*x-b).print("A*x-b = ");
	}
}
