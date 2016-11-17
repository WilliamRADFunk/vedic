using System.Text;
using UnityEngine;

public class Vid_MySql_Where : Vid_Object
{
    public Vid_MySql_Where() {
        output_dataType = VidData_Type.DATABASE_CLAUSE;
    }

    public override void Awake() {
        base.Awake();
        inputs = new Vid_ObjectInputs(2);
    }

    public override string ToString() 
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("WHERE ");
        if (inputs.getInput_atIndex(0) != null) {
            sb.AppendLine("(");
            TabTool.incromentCount();
            sb.Append(TabTool.TabCount() + inputs.getInput_atIndex(0).ToString());
            TabTool.deccromentCount();
            sb.AppendLine(TabTool.TabCount() + ")");
        }
        if (inputs.getInput_atIndex(1) != null) {
            TabTool.incromentCount();
            sb.Append(TabTool.TabCount() + inputs.getInput_atIndex(1).ToString());
            TabTool.deccromentCount();
        }
        return sb.ToString();
    }

    public override bool removeInput(int argumentIndex) {
        return base.removeInput(argumentIndex);
    }

    public override bool addInput(Vid_Object obj) {
        bool b;
        Debug.Log("Here:2");
        switch (obj.output_dataType) {
            case VidData_Type.WHERE_STATMENT:
                b = base.addInput(obj, 0);
                return b;
            case VidData_Type.DATABASE_CLAUSE:
                b = base.addInput(obj, 1);
                return b;
        }
        return false;
    }

    public override bool addInput(Vid_Object obj, int argumentIndex) {
        Debug.Log("Here:2");
        switch (argumentIndex) {
            case 0:
                if (obj.output_dataType == VidData_Type.WHERE_STATMENT) {
                    bool b = base.addInput(obj, 0);
                    return b;
                }
                else {
                    return false;
                }
            case 1:
                if (obj.output_dataType == VidData_Type.DATABASE_CLAUSE) {
                    bool b = base.addInput(obj, 1);
                    return b;
                }
                else {
                    return false;
                }
        }
        return false;
    }

    /*Helper Functions*/
    public override int AcceptedInputIndex(VidData_Type t) {
        switch (t) {
            case VidData_Type.WHERE_STATMENT:
                return 0;
            case VidData_Type.DATABASE_CLAUSE:
                return 1;
        }
        return -1;
    }
}
