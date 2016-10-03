using System.Text;

public class Vid_DeclarationVar : Vid_Object {

    public override void Awake() {
        base.Awake();
        base.output_dataType = VidData_Type.DECLAR_VAR;
        inputs = new Vid_ObjectInputs(1);
        acceptableInputs = new VidData_Type[1];
            acceptableInputs[0] = VidData_Type.IDENT;
    }

    public override string ToString() {
        StringBuilder sb = new StringBuilder();
        Vid_Object ident = inputs.getInput_atIndex(0);
        if (ident != null ) {
            sb.Append("var" + ident.ToString() + ";");
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
        return false;
    }
}