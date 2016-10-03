using UnityEngine;
using System.Collections;
using System.Text;

public class Vid_StatementReg : Vid_Statement {

    public override void Awake() {
        base.Awake();
        base.output_dataType = VidData_Type.STATMENT;
        inputs = new Vid_ObjectInputs(3);
        acceptableInputs = new VidData_Type[3];
        acceptableInputs[0] = VidData_Type.DECLAR_CON;
            acceptableInputs[1] = VidData_Type.IDENT;
            acceptableInputs[2] = VidData_Type.EXPRESSION;
    }

    public override string ToString() {
        StringBuilder sb = new StringBuilder();
        Vid_Object ident = inputs.getInput_atIndex(0);
        Vid_Object expression = inputs.getInput_atIndex(1);
        if (ident != null &&
            expression != null) {
            sb.Append(ident.ToString() + "= " + expression.ToString());
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
        if (obj.output_dataType == VidData_Type.EXPRESSION) {
            base.addInput(obj, 1);
        }
        return false;
    }
}