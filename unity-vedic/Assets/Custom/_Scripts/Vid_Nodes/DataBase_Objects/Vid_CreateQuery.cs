using System.Collections.Generic;
using System.Text;

public class Vid_CreateQuery : Vid_Query
{
    string primaryKey;

    public override void Awake() {
        base.Awake();
        base.output_dataType = VidData_Type.DATABASE_TABLE;
        inputs = new Vid_ObjectInputs(3);
        acceptableInputs = new VidData_Type[3];
            acceptableInputs[0] = VidData_Type.DATABASE_TABLE;
            acceptableInputs[1] = VidData_Type.DATABASE_COL;
            acceptableInputs[2] = VidData_Type.DATABASE_COL;
    }

    public override string ToString() {
        StringBuilder sb = new StringBuilder();
        if (inputs.getInput_atIndex(0) == null) {
            sb.Append("CREATE TABLE  error::NoTable (");
        }
        else {
            sb.Append("CREATE TABLE " + inputs.getInput_atIndex(0).ToString() + " ( ");
        }
        if (inputs.getInput_atIndex(1) != null) {
            sb.Append(inputs.getInput_atIndex(1).ToString()+ ", ");
        }
        if (inputs.getInput_atIndex(2) != null) {
            sb.Append("PRIMARY KEY (" + inputs.getInput_atIndex(2).ToString() + ")");
        }
        sb.Append(")");
        return sb.ToString();
    }

    public override bool addInput(Vid_Object obj, int argumentIndex) {
        // Note: don't change, Table=0,COL=1,Where=2 need to be these value.  
        switch (argumentIndex) {
            case 0:
                if (obj.output_dataType == VidData_Type.DATABASE_TABLE) {
                    bool b = base.addInput(obj, 0);
                    return b;
                }
                else {
                    return false;
                }
            case 1:
                if (obj.output_dataType == VidData_Type.DATABASE_COL) {
                    bool b = base.addInput(obj, 1);
                    return b;
                }
                else {
                    return false;
                }
            case 2:
                if (obj.output_dataType == VidData_Type.DATABASE_COL) {
                    bool b = base.addInput(obj, 2);
                    return b;
                }
                else {
                    return false;
                }
        }
        return false;
    }
}