using System.Text;

public class Vid_StatmentListNode : Vid_Statement {

    public override void Awake() {
        base.Awake();
        base.output_dataType = VidData_Type.STATMENT;
        inputs = new Vid_ObjectInputs(1);
        acceptableInputs = new VidData_Type[1];
            acceptableInputs[0] = VidData_Type.STATMENT;
    }

    public override string ToString() {
        StringBuilder sb = new StringBuilder();
        Vid_Object statment = inputs.getInput_atIndex(0);
        if (statment != null ) {
            sb.Append(statment.ToString() + ";");
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
