using UnityEngine;
using System.Collections;
using System.Text;

public class Vid_Where_Condition : Vid_Object {

    public WhereStatment_Type conditionType = WhereStatment_Type.EQU;
    public bool isEXISTS = false;
    public bool nothingFlag = false;

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
            switch (conditionType) {
                case WhereStatment_Type.LESS:
                    sb.Append(" " + inputs.getInput_atIndex(0).ToString());
                    sb.Append(" < " + inputs.getInput_atIndex(1).ToString() + " ");
                    break;
                case WhereStatment_Type.LESS_EQU:
                    sb.Append(" " + inputs.getInput_atIndex(0).ToString());
                    sb.Append(" <= " + inputs.getInput_atIndex(1).ToString() + " ");
                    break;
                case WhereStatment_Type.GREATER:
                    sb.Append(" " + inputs.getInput_atIndex(0).ToString());
                    sb.Append(" > " + inputs.getInput_atIndex(1).ToString() + " ");
                    break;
                case WhereStatment_Type.GREATER_EQU:
                    sb.Append(" " + inputs.getInput_atIndex(0).ToString());
                    sb.Append(" >= " + inputs.getInput_atIndex(1).ToString() + " ");
                    break;
                case WhereStatment_Type.EQU:
                    sb.Append(" " + inputs.getInput_atIndex(0).ToString());
                    sb.Append(" = " + inputs.getInput_atIndex(1).ToString() + " ");
                    break;
                case WhereStatment_Type.NOT_EQU:
                    sb.Append(" " + inputs.getInput_atIndex(0).ToString());
                    sb.Append(" <> " + inputs.getInput_atIndex(1).ToString() + " ");
                    break;
                case WhereStatment_Type.IN:
                    sb.Append(inputs.getInput_atIndex(0).ToString());
                    sb.Append(" IN ");
                    sb.AppendLine(TabTool.TabCount() + "(");
                    TabTool.incromentCount();
                    sb.AppendLine(TabTool.TabCount() + inputs.getInput_atIndex(1).ToString());
                    TabTool.deccromentCount();
                    sb.AppendLine(TabTool.TabCount() + ")");
                    break;
                case WhereStatment_Type.EXISTS:
                    if (obj.output_dataType != VidData_Type.DATABASE_COL) {
                        sb.AppendLine("EXISTS");
                        sb.Append("( " + inputs.getInput_atIndex(1).ToString() + " )");
                    }
                    break;
                case WhereStatment_Type.NOT_EXISTS:
                    if (obj.output_dataType != VidData_Type.DATABASE_COL) {
                        sb.AppendLine("NOT EXISTS");
                        sb.Append("( " + inputs.getInput_atIndex(1).ToString() + " )");
                    }
                    break;
                default:
                    break;
            }
        }
        return sb.ToString();
    }

    /*Builder functions*/
    public override bool addInput(Vid_Object obj, int index) {

        if (conditionType == WhereStatment_Type.EXISTS
                    || conditionType == WhereStatment_Type.NOT_EXISTS) {

            if (index == 0) {
                return false;
            }
            if (index == 1 && obj.output_dataType != VidData_Type.Q_SELECT) {
                return false;
            }
            return base.addInput(obj, index);
        }

        else if (conditionType == WhereStatment_Type.IN) {
            if (index == 0 && obj.output_dataType != VidData_Type.DATABASE_COL) {
                return false;
            }
            if (index == 1 && obj.output_dataType != VidData_Type.Q_SELECT) {
                return false;
            }
            return base.addInput(obj, index);
        }

        else if (obj.output_dataType == VidData_Type.DATABASE_COL
                    || obj.output_dataType == VidData_Type.NUM
                    || obj.output_dataType == VidData_Type.STRING
                    || obj.output_dataType == VidData_Type.BOOL) {
            return base.addInput(obj, index);
        }
        return false;
    }

}
