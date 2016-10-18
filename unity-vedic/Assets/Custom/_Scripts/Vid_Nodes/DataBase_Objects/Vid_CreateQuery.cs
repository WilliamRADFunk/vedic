using System.Collections.Generic;
using System.Text;

public class Vid_CreateQuery : Vid_Query
{
    string primaryKey;

    public override void Awake() {
        base.Awake();
        base.output_dataType = VidData_Type.DATABASE_TABLE;
        inputs = new Vid_ObjectInputs(4);
        acceptableInputs = new VidData_Type[4];
            acceptableInputs[0] = VidData_Type.DATABASE_TABLE;
            acceptableInputs[1] = VidData_Type.DATABASE_COL;
            acceptableInputs[2] = VidData_Type.DATABASE_COL;
            acceptableInputs[3] = VidData_Type.LIST;
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
        if (inputs.getInput_atIndex(3) != null) {
            sb.Append("FOREIGN KEY(" + inputs.getInput_atIndex(3).ToString() + ")" + "REFERENCES (" +""+")");
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
                    Vid_DB_Col col = (Vid_DB_Col)obj;
                    col.colMode = Vid_DB_Col.ColState.DATA;
                    bool b = base.addInput(obj, 1);
                    return b;
                }
                if (obj.output_dataType == VidData_Type.LIST) {
                    Vid_MultiInput multiIn = (Vid_MultiInput)obj;
                    Vid_Object[] list = multiIn.getInputs().inputs;
                    foreach(Vid_Object o in list) {
                        if(o.output_dataType == VidData_Type.DATABASE_COL) {
                            Vid_DB_Col col = (Vid_DB_Col)o;
                            col.colMode = Vid_DB_Col.ColState.DATA;
                        } 
                    }
                    bool b = base.addInput(obj, 1);
                    return b;
                }
                else {
                    return false;
                }
            case 2:
                if (obj.output_dataType == VidData_Type.DATABASE_COL) {
                    bool b = base.addInput(obj, 2);
                    Vid_DB_Col col = (Vid_DB_Col)obj;
                    col.colMode = Vid_DB_Col.ColState.DATA;
                    return b;
                }
                else {
                    return false;
                }
            case 3:
                if (obj.output_dataType == VidData_Type.DATABASE_COL) {
                    bool b = base.addInput(obj, 2);
                    Vid_DB_Col col = (Vid_DB_Col)obj;
                    col.colMode = Vid_DB_Col.ColState.DATA;
                    return b;
                }
                else {
                    return false;
                }
        }
        return false;
    }
}