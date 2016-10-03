using System.Text;

public class Vid_SelectQuery : Vid_Query
{
    public bool noRepeted;
    public bool isConditional;

    public Vid_SelectQuery()
    {
        isConditional = true;
        noRepeted = false;
    }

    public override void Awake() {
        base.Awake();
        base.output_dataType = VidData_Type.LIST;
        inputs = new Vid_ObjectInputs(3);
        acceptableInputs = new VidData_Type[3];
            acceptableInputs[0] = VidData_Type.DATABASE_TABLE;
            acceptableInputs[1] = VidData_Type.DATABASE_COL;
            acceptableInputs[2] = VidData_Type.DATABASE_CALUSE;
    }

    public override string ToString() {
        StringBuilder sb = new StringBuilder("");
        if (inputs.getInput_atIndex(1) == null) {
            sb.AppendLine(string.Format("{0," + TabTool.numberOfSpaces() + "}","SELECT *"));
        }
        else {
            sb.AppendLine(string.Format("{0," + TabTool.numberOfSpaces() + "}", 
                                            "SELECT " + inputs.getInput_atIndex(1).ToString()));
        }
        if (inputs.getInput_atIndex(0) == null) {
            sb.Append(string.Format("{0," + TabTool.numberOfSpaces() + "}", 
                                        "FROM error:NoT"));
        }
        else {
            sb.Append(string.Format("{0," + TabTool.numberOfSpaces() + "}",
                                        "FROM " + inputs.getInput_atIndex(0).ToString()));
        }
        if (inputs.getInput_atIndex(2) != null) {
            sb.AppendLine();
            sb.Append(string.Format("{0," + TabTool.numberOfSpaces() + "}",
                                       inputs.getInput_atIndex(2).ToString()));
        }
        return sb.ToString();
    }

    /*Builder functions*/
    public override bool addInput(Vid_Object obj, int argumentIndex) {
        // Note: don't change, Table=0,COL=1,Where=2 need to be these value.  
        switch (argumentIndex) {
            case 0:
                if (obj.output_dataType == VidData_Type.DATABASE_TABLE) {
                    bool b = base.addInput(obj, 0);
                    return b;
                }
                else {
                    return false;
                }
            case 1:
                if(obj.output_dataType == VidData_Type.DATABASE_COL) {
                    bool b = base.addInput(obj, 1);
                    return b;
                }
                else {
                    return false;
                }
            case 2:
                if (obj.output_dataType == VidData_Type.DATABASE_CALUSE) {
                    bool b = base.addInput(obj, 2);
                    return b;
                }
                else {
                    return false;
                }
        }
        return false;
    }

}
