

class main{
	static int Main(){
		sprint.print();
		var x=new writer();
		var y=new writer();
		x.print();
		y.print();
		y.s="new string\n";
		y.print();
		var z=new swriter();
		z.s="hello from swriter\n";
		z.print();
		return 0;
	}
}

public static class sprint{
	static string s="hello from print\n";
	public static void print(){
		System.Console.Write(s);
	}
}

public class writer{
	public string s="hello from writer\n";
	public void print(){
		System.Console.Write(s);
	}
}

public struct swriter{
	public string s;
	public void print(){
		System.Console.Write(s);
	}
}
