using System.Text;

public class MySql_MAX : Vid_Object {

    public MySql_MAX() {
        output_dataType = VidData_Type.NUM;
    }

    public override void Awake() {
        base.Awake();
        inputs = new Vid_ObjectInputs(1);
        acceptableInputs = new VidData_Type[1];
        acceptableInputs[0] = VidData_Type.DATABASE_COL;

    }

    public override string ToString() {
        StringBuilder sb = new StringBuilder();
        if (inputs.getInput_atIndex(0) != null) {
            sb.Append("MAX (" + inputs.getInput_atIndex(0) + " )");
        }
        return sb.ToString();
    }

    public override bool removeInput(int argumentIndex) {
        if (argumentIndex == 0) {
            base.removeInput(1);
        }
        return base.removeInput(argumentIndex);
    }

    public override bool addInput(Vid_Object obj, int argumentIndex) {
        switch (argumentIndex) {
            case 0:
                if (obj.output_dataType == VidData_Type.DATABASE_COL) {
                    bool b = base.addInput(obj, 1);
                    return b;
                }
                else {
                    break;
                }
        }
        return false;
    }
}
