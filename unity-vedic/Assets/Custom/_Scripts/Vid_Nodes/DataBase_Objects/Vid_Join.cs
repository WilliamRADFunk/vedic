using UnityEngine;
using System.Collections;
using System.Text;

public class Vid_Join : Vid_Object {

    public enum JoinType {
        NORMAL,
        LEFT,
        RIGHT,
        INNER,
        FULL
    }
    public JoinType joinType = JoinType.NORMAL;

    public Vid_Join() {
        output_dataType = VidData_Type.DATABASE_CLAUSE;
    }

    public override void Awake() {
        base.Awake();
        inputs = new Vid_ObjectInputs(2);
        acceptableInputs = new VidData_Type[3];
            acceptableInputs[0] = VidData_Type.DATABASE_TABLE;
            acceptableInputs[1] = VidData_Type.DATABASE_CLAUSE;
            acceptableInputs[2] = VidData_Type.LIST;
    }

    public override string ToString() {
        StringBuilder sb = new StringBuilder();
        switch (joinType) {
            case JoinType.NORMAL:
                sb.Append("JOIN ");
                if (inputs.getInput_atIndex(0) != null) {
                    sb.AppendLine(inputs.getInput_atIndex(0).ToString());
                }
                else {
                    sb.AppendLine("error: noTable");
                }
                if (inputs.getInput_atIndex(1) != null) {
                   sb.AppendLine(TabTool.TabCount() + inputs.getInput_atIndex(2).ToString()); 
                }
                break;
            case JoinType.LEFT:
                sb.Append("LEFT JOIN");
                if (inputs.getInput_atIndex(0) != null) {
                    sb.AppendLine(inputs.getInput_atIndex(0).ToString());
                }
                else {
                    sb.AppendLine("error: noTable");
                }
                if (inputs.getInput_atIndex(1) != null) {
                    if (inputs.getInput_atIndex(1).output_dataType == VidData_Type.ASSIGNMENT) {
                        sb.AppendLine(TabTool.TabCount() + "On " + inputs.getInput_atIndex(2).ToString());
                    }
                    else {
                        sb.AppendLine(TabTool.TabCount() + inputs.getInput_atIndex(1).ToString());
                    }
                }
                break;
            case JoinType.INNER:
                sb.Append("INNER JOIN");
                if (inputs.getInput_atIndex(0) != null) {
                    sb.AppendLine(inputs.getInput_atIndex(0).ToString());
                }
                else {
                    sb.AppendLine("error: noTable");
                }
                if (inputs.getInput_atIndex(1) != null) {
                    if (inputs.getInput_atIndex(1).output_dataType == VidData_Type.ASSIGNMENT) {
                        sb.AppendLine(TabTool.TabCount() + "On " + inputs.getInput_atIndex(2).ToString());
                    }
                    else {
                        sb.AppendLine(TabTool.TabCount() + inputs.getInput_atIndex(1).ToString());
                    }
                }
                break;
        }
        return sb.ToString();
    }

    public override bool removeInput(int argumentIndex) {
        if (argumentIndex == 0) {
            base.removeInput(1);
        }
        return base.removeInput(argumentIndex);
    }

    public override bool addInput(Vid_Object obj, int argumentIndex) {
        switch (argumentIndex) {
            case 0:
                break;
            case 1:
                break;
        }
        return false;
    }
}
