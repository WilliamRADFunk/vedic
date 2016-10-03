using System.Text;

public class Vid_MySql_Where : Vid_Object
{
    public bool isEXISTS = false;
    public bool inFlag = false;
    public bool likeFlag = false;

    public Vid_MySql_Where() {
        output_dataType = VidData_Type.DATABASE_CALUSE;
    }

    public override void Awake() {
        base.Awake();
        inputs = new Vid_ObjectInputs(2);
        acceptableInputs = new VidData_Type[5];
            acceptableInputs[0] = VidData_Type.BOOL;
            acceptableInputs[1] = VidData_Type.LIST;
            acceptableInputs[2] = VidData_Type.DATABASE_COL;
            acceptableInputs[3] = VidData_Type.DATABASE_CALUSE;
            acceptableInputs[4] = VidData_Type.STRING;
    }

    public override string ToString() 
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(string.Format("{0," + TabTool.numberOfSpaces() + "}","WHERE "));
        if (inputs.getInput_atIndex(0) != null) {
            if(inputs.getInput_atIndex(0).output_dataType == VidData_Type.DATABASE_COL) {
                sb.Append(inputs.getInput_atIndex(0).ToString());
                if (inFlag) {
                    sb.Append(" IN \n");
                }
                else if (likeFlag) {
                    sb.Append(" LIKE \n");
                }
            }
            else {
                sb.Append("( \n");
                TabTool.tabindex++;
                sb.Append(string.Format("{0," + TabTool.numberOfSpaces() + "}", inputs.getInput_atIndex(0).ToString()));
                TabTool.tabindex--;
                sb.Append(string.Format("{0," + TabTool.numberOfSpaces() + "}", ") \n"));
            }
        }
        else {
            if (isEXISTS) {
                sb.AppendLine("EXISTS");
            }
            else {
                sb.AppendLine("NOT EXISTS");
            }
        }
        if(inputs.getInput_atIndex(1) != null) {
            sb.AppendLine(string.Format("{0," + TabTool.numberOfSpaces() + "}", "("));
            TabTool.tabindex++;
            sb.AppendLine(string.Format("{0," + TabTool.numberOfSpaces() + "}", inputs.getInput_atIndex(1).ToString()));
            TabTool.tabindex--;
            sb.Append(string.Format("{0," + TabTool.numberOfSpaces() + "}", ")"));
        }
        return sb.ToString();
    }
    public override bool removeInput(int argumentIndex) {
        if(argumentIndex == 0) {
            inFlag = false;
            base.removeInput(1);
        }
        return base.removeInput(argumentIndex);
    }

    public override bool addInput(Vid_Object obj, int argumentIndex) {
        switch (argumentIndex) {
            case 0:
                if (obj.output_dataType == VidData_Type.BOOL
                    || obj.output_dataType == VidData_Type.DATABASE_COL) {
                    if (obj.output_dataType == VidData_Type.DATABASE_COL) {
                        inFlag = true;
                    }
                    bool b = base.addInput(obj, 0);
                    return b;
                }
                else {
                    return false;
                }
            case 1:
                if (obj.output_dataType == VidData_Type.LIST) {
                    inFlag = true;
                    likeFlag = false;
                    bool b = base.addInput(obj, 1);
                    return b;
                }
                if (obj.output_dataType == VidData_Type.DATABASE_CALUSE) {
                    inFlag = false;
                    likeFlag = false;
                    bool b = base.addInput(obj, 1);
                    return b;
                }
                if (obj.output_dataType == VidData_Type.STRING) {
                    inFlag = false;
                    likeFlag = true;
                    bool b = base.addInput(obj, 1);
                    return b;
                }
                else {
                    return false;
                }
        }
        return false;
    }
}
