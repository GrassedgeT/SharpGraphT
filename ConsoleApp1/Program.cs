public class Class1<E>
{
    private Func<string>? mystr;
    
    public Class1(Func<string> str)
    {
        mystr = str;
    }

    public void show()
    {
        Console.WriteLine(mystr());
    }
}

public class Class2
{
    public string strFunc()
    {
        return "hello world";
    }

    public string str
    {
        get
        {
            return "hello world";
        }
    }
}

public class Class3
{
    public static void Main()
    {
        Class1<string> c1 = new Class1<string>(() => new Class2().strFunc()); //报错
        c1.show();

        Class1<string> c2 = new Class1<string>(() => new Class2().str); //不报错
        c2.show();

        Class1<string> c3 = new Class1<string>(new Class2().strFunc); //不报错
        c3.show();

        Class1<string> c4 = new Class1<string>(new Class2().str); //报错
        c2.show();
    }
}
