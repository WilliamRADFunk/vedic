using UnityEngine;
using System.Collections;
using System.Text;

public class Vid_Where_Condition : Vid_Object {

    public Condition_Type conditionType = Condition_Type.EQU;
    bool isEXISTS = false;
    bool nothingFlag = false;

    public Vid_Where_Condition() {
        output_dataType = VidData_Type.WHERE_STATMENT;
    }

    public override void Awake() {
        inputs = new Vid_ObjectInputs(2);
    }

    public override string ToString() {
        StringBuilder sb = new StringBuilder();
        Vid_Object obj = inputs.getInput_atIndex(0);
        if (obj == null
                || inputs.getInput_atIndex(1) == null) {
            sb.Append("");
        }
        else {
            if (obj.output_dataType != VidData_Type.DATABASE_COL) {
                if (nothingFlag) {
                    if (isEXISTS) {
                        sb.AppendLine("EXISTS");
                    }
                    else {
                        sb.AppendLine("NOT EXISTS");
                    }
                }
                sb.Append("( " + inputs.getInput_atIndex(1).ToString() + " )");
            }
            else if (obj.output_dataType == VidData_Type.DATABASE_COL) {
                sb.Append(inputs.getInput_atIndex(0).ToString());
                sb.Append(" IN \n");
                sb.AppendLine(TabTool.TabCount() + "(");
                TabTool.incromentCount();
                sb.AppendLine(TabTool.TabCount() + inputs.getInput_atIndex(1).ToString());
                TabTool.deccromentCount();
                sb.AppendLine(TabTool.TabCount() + ")");
            }
            else {
                switch (conditionType) {
                    case Condition_Type.LESS:
                        sb.Append(" " + inputs.getInput_atIndex(0).ToString());
                        sb.Append(" < " + inputs.getInput_atIndex(1).ToString() + " ");
                        break;
                    case Condition_Type.LESS_EQU:
                        sb.Append(" " + inputs.getInput_atIndex(0).ToString());
                        sb.Append(" <= " + inputs.getInput_atIndex(1).ToString() + " ");
                        break;
                    case Condition_Type.GREATER:
                        sb.Append(" " + inputs.getInput_atIndex(0).ToString());
                        sb.Append(" > " + inputs.getInput_atIndex(1).ToString() + " ");
                        break;
                    case Condition_Type.GREATER_EQU:
                        sb.Append(" " + inputs.getInput_atIndex(0).ToString());
                        sb.Append(" >= " + inputs.getInput_atIndex(1).ToString() + " ");
                        break;
                    case Condition_Type.EQU:
                        sb.Append(" " + inputs.getInput_atIndex(0).ToString());
                        sb.Append(" = " + inputs.getInput_atIndex(1).ToString() + " ");
                        break;
                    case Condition_Type.NOT_EQU:
                        sb.Append(" " + inputs.getInput_atIndex(0).ToString());
                        sb.Append(" <> " + inputs.getInput_atIndex(1).ToString() + " ");
                        break;
                    default:
                        break;
                }
            }
        }
        return sb.ToString();
    }

    /*Builder functions*/
    public override bool addInput(Vid_Object obj, int index) {
        if(obj.output_dataType == VidData_Type.Q_SELECT) {
            return base.addInput(obj, 1);
        }
        if (obj.output_dataType == VidData_Type.DATABASE_COL
                    || obj.output_dataType == VidData_Type.NUM
                    || obj.output_dataType == VidData_Type.STRING
                    || obj.output_dataType == VidData_Type.BOOL) {
            return base.addInput(obj, index);
        }
        return false;
    }

}
