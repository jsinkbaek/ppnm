using static System.Console;
class main{
	static int Main(){
		vector3d v1 = new vector3d(1, 2, 3);
		vector3d v2 = new vector3d(2, 2, 1);
		Write("v1 = "); v1.ToString();
		Write("v2 = "); v2.ToString();
		Write("v1.magnitude() = {0}\n",v1.magnitude());
		Write("v1*2 = ");(v1*2).ToString();
		Write("2*v1 = ");(2*v1).ToString();
		Write("v1+v2 = ");(v1+v2).ToString();
		Write("v1-v2 = ");(v1-v2).ToString();
		Write("v1.dot_product(v2) = {0}\n", v1.dot_product(v2));
		Write("v1.vector_product(v2) = ");v1.vector_product(v2).ToString();
	return 0;
	}

}
