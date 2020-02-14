public struct vector3d{
	
	public double x
	{ get; set; }
	
	public double y
	{ get; set; }

	public double z
	{ get; set; }
	
	//constructor
	public vector3d(double a, double b, double c){x=a;y=b;z=c;}

	//operators
	public static vector3d operator*(vector3d v, double c)
	{
		return new vector3d(c*v.x, c*v.y, c*v.z);
	}
	
	public static vector3d operator*(double c, vector3d v)
	{
		return v*c;
	}
	
	public static vector3d operator+(vector3d v, vector3d u)
	{
		return new vector3d(v.x+u.x, v.y+u.y, v.z+u.z);
	}	
	
	public static vector3d operator-(vector3d v, vector3d u)
	{
		return new vector3d(v.x-u.x, v.y-u.y, v.z-u.z);
	}

	//methods
	public double dot_product(vector3d other)
	{
		return this.x*other.x + this.y*other.y + this.z*other.z;
	}	
	
	public vector3d vector_product(vector3d other)
	{
		return new vector3d(this.y*other.z-this.z*other.y, this.z*other.x-this.x*other.z, 
				    this.x*other.y-this.y*other.z);
	}
	
	public double magnitude()
	{
		return System.Math.Sqrt(this.x*this.x + this.y*this.y + this.z*this.z);
	}

	public override string ToString()
	{
		string str=$"({x}, {y}, {z})";
		System.Console.Write(str+"\n");
		return str;
	}
}
