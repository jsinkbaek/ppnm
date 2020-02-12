using static System.Console;
class main{
	static int Main(){
		maxmin();
		epscalc();
		harmsum();
	return 0;
	}

	static int maxmin(){
		// Max int
		int intmax = int.MaxValue;
		int i=0;
		while(i+1>i){
			i++;}
		int res = i - intmax;
		Write("int.MaxValue = {0}\n",int.MaxValue);
		Write("My max int i = {0}\n",i);
		Write("i-intmax = {0}\n",res);
		
		// Min int
		int intmin = int.MinValue;
		i=0;
		while(i-1<i){
			i--;}
		res = i-intmin;
		Write("int.MinValue = {0}\n",int.MinValue);
		Write("My min int i = {0}\n", i);
		Write("i-intmin = {0}\n", res);
	return 0;
	}

	static int epscalc(){
		double x=1;
		while(1+x!=1){
			x/=2;}
		x *= 2;

		float y=1F;
		while (1F+y!=1F){
			y/=2F;}
		y *= 2F;

		double dfeps = System.Math.Pow(2,-52);
		float feps = (float)System.Math.Pow(2F,-23F);
		

		Write("Double precision epsilon = {0}\n",x);
		Write("System.Math.Pow(2,-52) = {0}\n",dfeps);
		Write("Single precision epsilon = {0}\n",y);
		Write("System.Math.Pow(2,-23) = {0}\n",feps);
	return 0;
	}

	static int harmsum(){
		int max = 0;
		float float_sum_up = 1f;
		float float_sum_down = 1f/max;
		int i = 0;
		int k=0;
		for (k=8;k>2;k--){
			max = int.MaxValue/k;
			float_sum_up = 1f;
			for (i=2;i<max;i++)
				float_sum_up += 1f/i;
		
			float_sum_down = 1f/max;
			for (i=max-1;i>1;i--)
				float_sum_down += 1f/i;
		
			Write("max=int.MaxValue/{0},  ", k);
			Write("float_sum_up={0},  ", float_sum_up);
			Write("float_sum_down={0}\n", float_sum_down);
		}

	return 0;
	}
}
