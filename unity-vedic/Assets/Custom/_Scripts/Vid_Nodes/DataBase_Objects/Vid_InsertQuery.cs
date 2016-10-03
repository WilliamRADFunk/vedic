using System.Text;
using UnityEngine;

public class Vid_InsertQuery : Vid_Query
{
    public bool valuesFlage = true;

    public override void Awake() {
        base.Awake();
        base.output_dataType = VidData_Type.DATABASE;
        inputs = new Vid_ObjectInputs(3);
        acceptableInputs = new VidData_Type[3];
            acceptableInputs[0] = VidData_Type.DATABASE_TABLE;
            acceptableInputs[1] = VidData_Type.DATABASE_COL;
            acceptableInputs[2] = VidData_Type.LIST;
    }

    public override string ToString() {
        StringBuilder sb = new StringBuilder();
        if (inputs.getInput_atIndex(0) == null) {
            sb.Append("INSERT INTO error::NoTable SET ");
        }
        else {
            sb.Append("INSERT INTO " + inputs.getInput_atIndex(0).ToString() + " ");
        }
        if (inputs.getInput_atIndex(1) != null) {
            sb.Append("("+ inputs.getInput_atIndex(1).ToString() + " )");
        }
        if (inputs.getInput_atIndex(2) != null) {
            if (valuesFlage) {
                sb.Append("VALUES (" + inputs.getInput_atIndex(2).ToString() + " )");
            }
            else {
                sb.Append( inputs.getInput_atIndex(2).ToString());
            }
        }
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
                if (obj.output_dataType == VidData_Type.LIST) {
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
