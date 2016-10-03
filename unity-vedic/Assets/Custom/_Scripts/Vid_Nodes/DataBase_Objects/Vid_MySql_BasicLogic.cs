using UnityEngine;
using System.Collections;
using System.Text;

public class Vid_MySql_BasicLogic : Vid_Object {

    public BasicLogic logicType = BasicLogic.AND;

    public Vid_MySql_BasicLogic() {
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
            switch (logicType) {
                case BasicLogic.AND:
                    sb.Append(string.Format("{0," + TabTool.numberOfSpaces() + "}", inputs.getInput_atIndex(0).ToString()));
                    sb.Append(" ADD " + inputs.getInput_atIndex(1).ToString() + " ");
                    break;
                case BasicLogic.OR:
                    sb.Append(string.Format("{0," + TabTool.numberOfSpaces() + "}", inputs.getInput_atIndex(0).ToString()));
                    sb.Append(" OR " + inputs.getInput_atIndex(1).ToString() + " ");
                    break;
                default:
                    break;
            }
        }
        return sb.ToString();
    }
    /*Builder functions*/
    public override bool addInput(Vid_Object obj, int index) {
        if (obj.output_dataType == VidData_Type.BOOL) {
            return base.addInput(obj, index);
        }
        return false;
    }
}
