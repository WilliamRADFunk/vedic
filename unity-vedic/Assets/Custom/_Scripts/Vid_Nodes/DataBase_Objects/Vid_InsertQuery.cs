using System.Text;
using UnityEngine;

public class Vid_InsertQuery : Vid_Query
{
    public bool valuesFlage = true;

    public override void Awake() {
        base.Awake();
        base.output_dataType = VidData_Type.DATABASE;
        inputs = new Vid_ObjectInputs(2);
    }

    public override string ToString() {
        StringBuilder sb = new StringBuilder();
        if (inputs.getInput_atIndex(0) == null) {
            sb.Append("INSERT INTO error::NoTable SET ");
        }
        else {
            sb.AppendLine("INSERT INTO " + inputs.getInput_atIndex(0).ToString() + " VALUES");
        }
        if (inputs.getInput_atIndex(1) != null) {
            sb.AppendLine("(");
            TabTool.incromentCount();
            sb.AppendLine(TabTool.TabCount() + inputs.getInput_atIndex(1).ToString());
            TabTool.deccromentCount();
            sb.AppendLine(")");
        }
        return sb.ToString();
    }

    public override bool addInput(Vid_Object obj) {
        // Note: don't change, Table=0,COL=1,Where=2 need to be these value.  
        bool b = false;
        switch (obj.output_dataType) {
            case VidData_Type.DATABASE_TABLE:
                b = base.addInput(obj, 0);
                return b;
            case VidData_Type.LIST:
                b = base.addInput(obj, 1);
                return b;
        }
        return false;
    }

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
                if (obj.output_dataType == VidData_Type.LIST) {
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
            case VidData_Type.DATABASE_TABLE:
                return 0;
            case VidData_Type.LIST:
                return 1;
        }
        return -1;
    }
}
