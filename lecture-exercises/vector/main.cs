class main{
	static int Main(){
		int n=3;
		vector v=new vector(n);
		vector u=new vector(n);
		for(int i=0;i<n;i++){
			v[i]=i;
			u[i]=-2*i;
		}

		v.print("v=");
		v[0]=5;
		v.print("v=");
		vector w=u+v;
		w.print("w=");
	return 0;
	}
}
