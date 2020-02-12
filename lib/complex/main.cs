using System;
using static System.Math;
using static cmath;
class main{
static int Main(){
	var a=new complex(0,1);
	var b=new complex(1,1);
	a.print("a =");
	Console.Write("{0}\n",exp(1.2));
	Console.Write("b  ={0}\n",b);
	Console.Write("exp(b)={0}\n",exp(b));
	Console.Write("sin(b)={0}\n",sin(b));
	Console.Write("cos(b)={0}\n",cos(b));
	Console.Write("log(exp(b))={0}\n",log(exp(b)));
	Console.Write("exp(log(b))={0}\n",exp(log(b)));
	Console.Write("a/b={0}\n",a/b);
	Console.Write("(a/b)*b={0}\n",(a/b)*b);
	Console.Write($"{a}.pow(2)={a.pow(2)}\n");
	Console.Write($"{a}.pow({a})={a.pow(a)}, e^(-pi/2)={exp(-PI/2)}\n");
return 0;
}
}
