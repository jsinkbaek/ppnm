using static System.Math;
static class Approx{
	static bool approx(double a, double b, double tau=1e-9, double epsilon=1e-9){
		double absdif = Abs(a-b);
		double absum = Abs(a) + Abs(b);
		bool result = false;
		if (absdif < tau){result = true;}
		else if (absdif/absum < epsilon){result = true;}
		else{result = false;}
	return result;
	}
}
