public struct complex{
	private double re,im;
	double Re {get{return re;}set{re=value;}}
	double Im {get{return im;}set{im=value;}}
	public complex(double x, double y){re=x;im=y;}
	public complex(double x){re=x;im=0;}
	
	public static implicit operator complex(double x){return new complex(x);}

	public static complex operator+(complex a,complex b){
		double x=a.Re+b.Re;
		double y=a.Im+b.Im;
		return new complex(x,y);
	}
	
	public static complex operator-(complex a, complex b){
		double x=a.Re-b.Re;
		double y=a.Im-b.Im;
		return new complex(x,y);
	}
	
	public void print(string s=""){
		System.Console.Write("{0} {1} {2}\n",s,this.Re,this.Im);
	}


	public override string ToString(){
		return string.Format("{0} {1}",this.Re,this.Im);
	}
}
