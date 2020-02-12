public class vector{
	// the data as an array of doubles
	double[] data;

	// the size of the data array
	public int size{
		get{return data.Length;}
	}

	// "this" is a normal operator referring to the argument we are working
	//  on. Used to set individual index values or see them.
	public double this[int i]{
		get{return data[i];}
		set{data[i]=value;}
	}
	// Constructor to create a new vector
	public vector(int n){data=new double[n];}
	
	// for debugging (s="" is default parameter)
	public void print(string s=""){
		System.Console.Write(s);
		for(int i=0;i<size;i++)
			System.Console.Write("{0:f3} ",this[i]);
		System.Console.Write("\n");
	}

	// + operation
	public static vector operator+(vector u, vector v){
		vector r=new vector(u.size);
		for(int i=0;i<u.size;i++)r[i]=u[i]+v[i];
		return r;
	}
	
	// one * operation (double*vector and vector*double)
	public static vector operator*(double a, vector v){
		vector r=new vector(v.size);
		for(int i=0;i<v.size;i++)r[i]=a*v[i];
		return r;
	}
	public static vector operator*(vector v, double a){
		return a*v;
	}
}
