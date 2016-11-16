using UnityEngine;
using System.Collections;
using System.Text;

public class Vid_Assignment : Vid_Object {

    public Vid_Assignment() {
        output_dataType = VidData_Type.ASSINMENT;
    }

    public override void Awake() {
        inputs = new Vid_ObjectInputs(2);
    }

    public override string ToString() {
        StringBuilder sb = new StringBuilder();
        if (inputs.getInput_atIndex(0) == null) {
            sb.Append("NoInput at index 0");
        }
        else if (inputs.getInput_atIndex(1) == null) {
            sb.Append("NoInput at index 1");
        }
        else {
            sb.Append(inputs.getInput_atIndex(0).ToString() + " = " + inputs.getInput_atIndex(1).ToString());
        }
        return sb.ToString();
    }

    public override bool addInput(Vid_Object obj, int index) {
        bool b = base.addInput(obj, index);
        return b; 
    }

}
