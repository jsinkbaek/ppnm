using static System.Console;
using static System.Math;
using static linalg;
using static mathelp;

class main
{
	static int Main()
	{
		B();

		return 0;
	}

	static void B()
	{
		WriteLine("Problem B");
		var rand = new System.Random();
		int m = 2 + rand.Next(6);
		int n = m;

		WriteLine("Random square matrix A");
		matrix A = make_random_matrix(n, m);
		A.print("A = ");

		qr_decomp_GS decomposition = new qr_decomp_GS(A);

		matrix B = decomposition.inverse();
		WriteLine("B inverse of Q*R.");
		B.print("B = ");

		matrix I = A*B;
		I.print("A * B = ");
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
