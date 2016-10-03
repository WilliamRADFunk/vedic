using UnityEngine;
using System.Text;

public class Vid_BasicCondition : Vid_Object {

    public Condition_Type conditionType = Condition_Type.LESS;

    public override void Awake()
    {
        base.Awake();
        inputs = new Vid_ObjectInputs(2);
        acceptableInputs = new VidData_Type[2];
            acceptableInputs[0] = VidData_Type.BOOL;
            acceptableInputs[1] = VidData_Type.NUM;
        base.output_dataType = VidData_Type.BOOL;
    }

    public override string ToString() {
        StringBuilder sb = new StringBuilder();
        if (inputs.getInput_atIndex(0) == null
                || inputs.getInput_atIndex(1) == null) {
            sb.Append("null");
        }
        else {
            switch (conditionType) {
                case Condition_Type.LESS:
                    sb.Append("( " + inputs.getInput_atIndex(0).ToString());
                    sb.Append(" <" + inputs.getInput_atIndex(1).ToString() + " )");

                    break;
                case Condition_Type.LESS_EQU:
                    sb.Append("( " + inputs.getInput_atIndex(0).ToString());
                    sb.Append(" <=" + inputs.getInput_atIndex(1).ToString() + " )");
                    break;
                case Condition_Type.GREATER:
                    sb.Append("( " + inputs.getInput_atIndex(0).ToString());
                    sb.Append(" >" + inputs.getInput_atIndex(1).ToString() + " )");
                    break;
                case Condition_Type.GREATER_EQU:
                    sb.Append("( " + inputs.getInput_atIndex(0).ToString());
                    sb.Append(" >=" + inputs.getInput_atIndex(1).ToString() + " )");
                    break;
                case Condition_Type.EQU:
                    sb.Append("( " + inputs.getInput_atIndex(0).ToString());
                    sb.Append(" ==" + inputs.getInput_atIndex(1).ToString() + " )");
                    break;
                case Condition_Type.NOT_EQU:
                    sb.Append("( " + inputs.getInput_atIndex(0).ToString());
                    sb.Append(" !=" + inputs.getInput_atIndex(1).ToString() + " )");
                    break;
                default:
                    break;
            }
        }
        return sb.ToString();
    }

    /*Builder functions*/
    public override bool addInput(Vid_Object obj, int index)
    {
        if (obj.output_dataType == VidData_Type.BOOL
                || obj.output_dataType == VidData_Type.NUM)
        {
            base.addInput(obj,index);
        }
        return false;
    }
}
