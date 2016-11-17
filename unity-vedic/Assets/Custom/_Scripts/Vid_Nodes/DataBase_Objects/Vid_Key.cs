using UnityEngine;
using System.Collections;
using System.Text;

public class Vid_Key : Vid_Object {

    public enum KeyType {
        PRIMARY,
        FOREIGN
    }

    public KeyType keyType = KeyType.PRIMARY; 

    public Vid_Key() {
        output_dataType = VidData_Type.KEY;
    }
    // Use this for initialization
    public override void Awake() {
        inputs = new Vid_ObjectInputs(2);
    }

    public override string ToString() {
        StringBuilder sb = new StringBuilder();
        switch (keyType) {
            case KeyType.PRIMARY:
                if(inputs.getInput_atIndex(1) != null) {
                    sb.Append("PRIMARY (" + inputs.getInput_atIndex(1).ToString() + ")");
                }
                break;
            case KeyType.FOREIGN:
                if (inputs.getInput_atIndex(1) != null) {
                    sb.AppendLine("FOREIGN (" + inputs.getInput_atIndex(1).ToString() + ")");
                    if (inputs.getInput_atIndex(1) != null) {
                        sb.Append(" REFERENCES  " + inputs.getInput_atIndex(1).ToString() +
                                                   " (" + inputs.getInput_atIndex(1).ToString() + ")");
                    }
                    else {
                        sb.Append(" Error:NoTable");
                    }
                }
                break;
        }
        return sb.ToString();
    }

    public override bool addInput(Vid_Object obj) {
        if (obj.output_dataType == VidData_Type.DATABASE_TABLE) {
            return base.addInput(obj, 0);
        }
        else if (obj.output_dataType == VidData_Type.DATABASE_COL) {
            return base.addInput(obj, 1);
        }
        return false;
    }
    public override bool addInput(Vid_Object obj, int argumentIndex) {
        if (obj.output_dataType == VidData_Type.DATABASE_TABLE) {
            return base.addInput(obj, 0);
        }
        else if (obj.output_dataType == VidData_Type.DATABASE_COL) {
            return base.addInput(obj, 1);
        }
        return false;
    }

}
