using System.Text;

public class Vid_StatmentWhile : Vid_Statement {

    public override void Awake() {
        base.Awake();
        base.output_dataType = VidData_Type.STATMENT;
        inputs = new Vid_ObjectInputs(2);
        acceptableInputs = new VidData_Type[2];
            acceptableInputs[0] = VidData_Type.CONDITION;
            acceptableInputs[1] = VidData_Type.STATMENT;
    }

    public override string ToString() {
        StringBuilder sb = new StringBuilder();
        Vid_Object condirion = inputs.getInput_atIndex(0);
        Vid_Object statment = inputs.getInput_atIndex(0);
        if (condirion != null &&
                statment != null) {
            sb.Append("while( " + condirion.ToString() + " { " + statment.ToString() + " }");
        }
        else {
            sb.Append("");
        }
        return sb.ToString();
    }

    public override bool addInput(Vid_Object obj, int argumentIndex) {
        if (obj.output_dataType == VidData_Type.CONDITION) {
            base.addInput(obj, 0);
        }
        if (obj.output_dataType == VidData_Type.STATMENT) {
            base.addInput(obj, 1);
        }
        return false;
    }
}
