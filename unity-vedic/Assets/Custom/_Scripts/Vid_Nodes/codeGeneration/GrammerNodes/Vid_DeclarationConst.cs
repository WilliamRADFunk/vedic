using UnityEngine;
using System.Collections;
using System.Text;

public class Vid_DeclarationConst : Vid_Object {

    public override void Awake() {
        base.Awake();
        base.output_dataType = VidData_Type.DECLAR_CON;
        inputs = new Vid_ObjectInputs(2);
        acceptableInputs = new VidData_Type[2];
            acceptableInputs[0] = VidData_Type.IDENT;
            acceptableInputs[1] = VidData_Type.NUM;
    }

    public override string ToString() {
        StringBuilder sb = new StringBuilder();
        Vid_Object ident = inputs.getInput_atIndex(0);
        Vid_Object number = inputs.getInput_atIndex(1);

        if (ident != null &&
            number != null) {
            sb.Append(ident.ToString() + "= " + number.ToString() +";");
        }
        else {
            sb.Append("");
        }

        return sb.ToString();
    }

    public override bool addInput(Vid_Object obj, int argumentIndex) {
        if (obj.output_dataType == VidData_Type.IDENT) {
            base.addInput(obj, 0);
        }
        if (obj.output_dataType == VidData_Type.NUM) {
            base.addInput(obj, 1);
        }
        return false;
    }
}
