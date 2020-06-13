using System;
using static System.Console;
using static cmath;
class main{
static int Main(){
	double a=2;
	complex one = new complex(1,0);
	complex i = new complex(0,1);
	Write("i={0}\n",i);
	Write("a={0}\n",a);
	
	Write("Sqrt({0}) 		= {1}	Expected: 1.41421\n",a,Math.Sqrt(a));
	Write("Sqrt({0})*Sqrt({0})		= {1}			Expected: 2\n",a, Math.Sqrt(a)*Math.Sqrt(a));
	
	complex ei = exp(i);
	complex e = Math.E;
	complex ei_ = e.pow(i);
	Write("exp(i) 			= {0}		Expected: 0.54+0.84i\n",ei);
	//Write("Math.E^Math.Sqrt(-1) = {0}\n",ei_);
	complex eipowpi = ei.pow(Math.PI);
	Write("exp(i).pow(PI) 		= {0}		Expected: -1\n",eipowpi);
	
	complex eipi = exp((i*Math.PI));
	Write("exp(i*Math.PI) 		= {0}		Expected: -1\n",eipi);
	complex eulerid = eipi + one;
	Write("exp(i*Math.PI)+1	= {0}		Expected: 0\n",eulerid);
	
	complex sipi = sin((i*Math.PI));
	Write("sin(i*Math.PI)		= {0}		Expected: 0+11.5i\n",sin((i*Math.PI)));
return 0;
}
}
