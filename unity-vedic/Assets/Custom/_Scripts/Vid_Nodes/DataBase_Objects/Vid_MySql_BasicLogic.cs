using UnityEngine;
using System.Collections;
using System.Text;

public class Vid_MySql_BasicLogic : Vid_Object {

    public enum BasicLogic {
        AND,
        OR
    }
    public BasicLogic logicType = BasicLogic.AND;

    public Vid_MySql_BasicLogic() {
        output_dataType = VidData_Type.WHERE_STATMENT;
    }

    public override void Awake() {
        inputs = new Vid_ObjectInputs(2);
        acceptableInputs = new VidData_Type[1];
            acceptableInputs[0] = VidData_Type.WHERE_STATMENT;
    }

    public override string ToString() {
        StringBuilder sb = new StringBuilder();
        if (inputs.getInput_atIndex(0) == null
                || inputs.getInput_atIndex(1) == null) {
            sb.Append("");
        }
        else {
            switch (logicType) {
                case BasicLogic.AND:
                    sb.AppendLine(inputs.getInput_atIndex(0).ToString());
                    sb.AppendLine(TabTool.TabCount() +" ADD " + inputs.getInput_atIndex(1).ToString() + " ");
                    break;
                case BasicLogic.OR:
                    sb.AppendLine(inputs.getInput_atIndex(0).ToString());
                    sb.AppendLine(TabTool.TabCount() + " OR " + inputs.getInput_atIndex(1).ToString() + " ");
                    break;
                default:
                    break;
            }
        }
        return sb.ToString();
    }
    /*Builder functions*/
    public override bool addInput(Vid_Object obj, int index) {
        if (obj.output_dataType == VidData_Type.WHERE_STATMENT) {
            return base.addInput(obj, index);
        }
        return false;
    }
}
