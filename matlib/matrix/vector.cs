// (C) 2020 Dmitri Fedorov; License: GNU GPL v3+; no warranty.
using System;
using static System.Math;
public partial class vector{

private double[] data;
public int size{ get{return data.Length;} }

public double this[int i]{
	get{return data[i];}
	set{data[i]=value;}
}

public vector(int n){data=new double[n];}
public vector(double[] a){data=a;}
public vector(double a)
	{ data = new double[]{a}; }
public vector(double a, double b)
	{ data = new double[]{a,b}; }
public vector(double a, double b, double c)
	{ data = new double[]{a,b,c}; }
public vector(double a, double b, double c, double d)
	{ data = new double[]{a,b,c,d}; }

public static implicit operator vector (double[] a){ return new vector(a); }
public static implicit operator double[] (vector v){ return v.data; }

public void print(string s="",string format="{0,10:g3} "){
	System.Console.Write(s);
	for(int i=0;i<size;i++) System.Console.Write(format,this[i]);
	System.Console.Write("\n");
}

public static vector operator+(double a, vector v){
	vector r=new vector(v.size);
	for(int i=0;i<r.size;i++)r[i]=a+v[i];
	return r; }

public static vector operator+(vector v, double a)
	{return a+v;}

public static vector operator+(vector v, vector u){
	vector r=new vector(v.size);
	for(int i=0;i<r.size;i++)r[i]=v[i]+u[i];
	return r; }

public static vector operator-(vector v, vector u){
	vector r=new vector(v.size);
	for(int i=0;i<r.size;i++)r[i]=v[i]-u[i];
	return r; }

public static vector operator*(vector v, double a){
	vector r=new vector(v.size);
	for(int i=0;i<v.size;i++)r[i]=a*v[i];
	return r; }

public static vector operator*(double a, vector v){
	return v*a; }

public static vector operator/(vector v, double a){
	vector r=new vector(v.size);
	for(int i=0;i<v.size;i++)r[i]=v[i]/a;
	return r; }

public double dot(vector o){
	double sum=0;
	for(int i=0;i<size;i++)sum+=this[i]*o[i];
	return sum;
	}

public double norm(){
	double meanabs=0;
	for(int i=0;i<size;i++)meanabs+=this[i];
	meanabs/=size;
	double sum=0;
	for(int i=0;i<size;i++)sum+=(this[i]/meanabs)*(this[i]/meanabs);
	return meanabs*sum;
	}

public double norm2(bool check=false)
{// euclidean norm2 by definition, with over/underflow potential
	double res_temp=0;
	double res=0;
	bool floaterr = false;
	if (check)
	{// check if float rounding (if check=true)
		for (int i=0; i<this.size; i++)
		{
			res_temp = res + this[i]*this[i];
			if (res_temp != res)
			{
				res = res_temp;
			}
			else
			{
				floaterr = true;
				System.Console.Error.WriteLine("vector.norm2() double arithmetic rounding");
				break;
			}
		}
		if (floaterr)
		{
			System.Console.Error.WriteLine("returning vector.norm() instead. Check result");
			return this.norm();
		}
	}
	else
	{
		for (int i=0; i<this.size; i++)
		{
			res += this[i]*this[i];
		}
	}

	if (double.IsInfinity(res))
	{// Check if over/underflowed regardless of bool check
		System.Console.Error.WriteLine("vector.norm2() double over/underflowed");
		System.Console.Error.WriteLine("returning vector.norm() instead. Check result");
		return this.norm();
	}
	
	return Sqrt(res);
}

public vector copy(){
	vector b=new vector(this.size);
	for(int i=0;i<this.size;i++)b[i]=this[i];
	return b;
}

public static bool approx(double x, double y, double acc=1e-9, double eps=1e-9){
	if(Abs(x-y)<acc)return true;
	if(Abs(x-y)/Max(Abs(x),Abs(y))<eps)return true;
	return false;
	}

public static bool approx(vector a,vector b,double acc=1e-9,double eps=1e-9){
	if(a.size!=b.size)return false;
	for(int i=0;i<a.size;i++)
		if(!approx(a[i],b[i],acc,eps))return false;
	return true;
}
public bool approx(vector o){
	for(int i=0;i<size;i++)
		if(!approx(this[i],o[i]))return false;
	return true;
	}

public vector abs() {
	vector b = new vector(size);
	for(int i = 0; i < size; i++) {
		b[i] = Abs(data[i]);
	}
	return b;
}

}//vector
