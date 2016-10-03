using UnityEngine;
using System.Collections;
using System.Text;

public class Vid_Where_Condition : Vid_Object {

    public Condition_Type conditionType = Condition_Type.EQU;

    public Vid_Where_Condition() {
        output_dataType = VidData_Type.BOOL;
    }

    public override void Awake() {
        inputs = new Vid_ObjectInputs(2);
        acceptableInputs = new VidData_Type[3];
            acceptableInputs[0] = VidData_Type.DATABASE_COL;
            acceptableInputs[1] = VidData_Type.NUM;
            acceptableInputs[2] = VidData_Type.STRING;
    }

    public override string ToString() {
        StringBuilder sb = new StringBuilder();
        if (inputs.getInput_atIndex(0) == null
                || inputs.getInput_atIndex(1) == null) {
            sb.Append("");
        }
        else {
            switch (conditionType) {
                case Condition_Type.LESS:
                    sb.Append(" " + inputs.getInput_atIndex(0).ToString());
                    sb.Append(" <" + inputs.getInput_atIndex(1).ToString() + " ");
                    break;
                case Condition_Type.LESS_EQU:
                    sb.Append(" " + inputs.getInput_atIndex(0).ToString());
                    sb.Append(" <=" + inputs.getInput_atIndex(1).ToString() + " ");
                    break;
                case Condition_Type.GREATER:
                    sb.Append("( " + inputs.getInput_atIndex(0).ToString());
                    sb.Append(" >" + inputs.getInput_atIndex(1).ToString() + " ");
                    break;
                case Condition_Type.GREATER_EQU:
                    sb.Append(" " + inputs.getInput_atIndex(0).ToString());
                    sb.Append(" >=" + inputs.getInput_atIndex(1).ToString() + " ");
                    break;
                case Condition_Type.EQU:
                    sb.Append(" " + inputs.getInput_atIndex(0).ToString());
                    sb.Append(" =" + inputs.getInput_atIndex(1).ToString() + " ");
                    break;
                case Condition_Type.NOT_EQU:
                    sb.Append(" " + inputs.getInput_atIndex(0).ToString());
                    sb.Append(" !=" + inputs.getInput_atIndex(1).ToString() + " ");
                    break;
                default:
                    break;
            }
        }
        sb.AppendLine();
        return sb.ToString();
    }

    /*Builder functions*/
    public override bool addInput(Vid_Object obj, int index) {
        if (obj.output_dataType == VidData_Type.DATABASE_COL) {
            return base.addInput(obj, index);
        }
        else {
            if (obj.output_dataType == VidData_Type.DATABASE_TABLE
                    || obj.output_dataType == VidData_Type.NUM
                    || obj.output_dataType == VidData_Type.STRING
                    || obj.output_dataType == VidData_Type.BOOL) {
                if (index == 1) {
                    return base.addInput(obj, index);
                }
                else {
                    return false;
                }
            }
            return false;
        }
    }
}
