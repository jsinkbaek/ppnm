using static System.Console;
using static System.Math;
class main{
	string s="old string\n";
	static int Main(){
		double x=1.23;
		int nmax=100;
		// string s shadows s from outer scope
		string s="hello\n";
		Write(s);
		// inner shadow x, in c# block shadowing is not allowed
		// {double x=0;}
		// however, this is allowed as long as y does not shadow another y:
		{double y=0;}

		// if statement
		if(2>1){
			Write("2>1\n");
		}
		else{
			Write("2!>1\n");
		}
		
		// for loop
		for(int i=0;i<10;i++){
			Write("i={0} ",i);
		}
		Write(" \n");

		// while loop
		int k=0;
		while(k<10){
			Write("while loop: i={0}\n",k);
			k++;
		}

		// do while loop. Will perform action first, and then check
		do {
			Write("at least once\n");
		} while (false);

		// fixed format
		Write("pi={0:f2}\n",PI);
		Write("pi={0:f15}\n",PI);

		// array of doubles (empty array with element numbers 0-9)
		double[] v = new double[10];
		// fill and write
		for(k=0;k<10;k++){
			v[k]=Sin(k);
			Write("v[{0}] = {1}\n", k, v[k]);	
		}

		// append PI to end of array
		System.Array.Resize(ref v, v.Length+1);
		Write("v.Length={0}\n",v.Length);
		v[v.Length-1]=PI;

		for(k=0;k<v.Length;k++){
			Write("v[{0}] = {1}\n", k, v[k]);
		}
	return 0;
	}
}
