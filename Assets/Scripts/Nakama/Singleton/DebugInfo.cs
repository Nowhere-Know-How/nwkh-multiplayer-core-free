public class DebugInfo 
{
    static string header = "<Header>";
    static string message = "<Message>";

    private DebugInfo()
    {

    }

    public static string Message {
        get { return message; }
        set { message = value; }
    }

    public static string Header
    {
        get { return header; }
        set { header = value; }
    }

    public static void SetToast(string h, string m)
    {
        Header = h;
        Message = m;
    }
}
