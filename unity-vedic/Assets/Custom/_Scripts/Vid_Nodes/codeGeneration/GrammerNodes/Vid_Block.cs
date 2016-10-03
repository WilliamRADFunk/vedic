using UnityEngine;
using System.Collections;
using System.Text;

public class Vid_Block : Vid_Object {

    public override void Awake() {
        base.Awake();
        base.output_dataType = VidData_Type.BLOCK;
        inputs = new Vid_ObjectInputs(3);
        acceptableInputs = new VidData_Type[3];
            acceptableInputs[0] = VidData_Type.DECLAR_CON;
            acceptableInputs[1] = VidData_Type.DECLAR_VAR;
            acceptableInputs[2] = VidData_Type.STATMENT;
    }

    public override string ToString() {

        StringBuilder sb = new StringBuilder();
        Vid_Object declarationConst = inputs.getInput_atIndex(0);
        Vid_Object declarationVar = inputs.getInput_atIndex(1);
        Vid_Object statment = inputs.getInput_atIndex(2);
        if (declarationConst != null) {
            sb.Append(declarationConst.ToString()+ "\n");
        }
        if (inputs.getInput_atIndex(1) != null) {
            sb.Append(declarationVar.ToString() + "\n");
        }
        if (inputs.getInput_atIndex(2) != null) {
            sb.Append(statment.ToString() + "\n");
        }
        return sb.ToString();
    }

    public override bool addInput(Vid_Object obj, int argumentIndex) {
        if (obj.output_dataType == VidData_Type.DECLAR_CON) {
            base.addInput(obj, 0);
        }
        if (obj.output_dataType == VidData_Type.DECLAR_VAR) {
            base.addInput(obj, 1);
        }
        if(obj.output_dataType == VidData_Type.STATMENT) {
            base.addInput(obj, 2);
        }
        return false;
    }
}
