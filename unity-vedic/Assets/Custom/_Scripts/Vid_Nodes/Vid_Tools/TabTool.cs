using System.Text;

public class TabTool  {
    static int count = 0;
    public static string TabCount() {
        StringBuilder sb = new StringBuilder("");
        for (int i = 0; i < count; i++) {
            sb.Append("\t");
        }
        return sb.ToString();
    }
    public static void incromentCount() {
        count++;
    }
    public static void deccromentCount() {
        if (count > 0) {
            count--;
        }
    }
}