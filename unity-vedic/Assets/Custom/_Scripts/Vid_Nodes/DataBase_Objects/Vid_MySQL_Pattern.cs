using UnityEngine;
using System.Collections;

public class Vid_MySQL_REGX : Vid_Object {

    public string data = "defaultString";
    public bool isREGX = false;
    public bool isNOT = false;

    public override void Awake() {
        base.Awake();
        output_dataType = VidData_Type.WHERE_STATMENT;
        acceptableInputs = new VidData_Type[1];
            acceptableInputs[0] = VidData_Type.DATABASE_COL;
    }

    public override bool addInput(Vid_Object obj, int index) {
        if (obj.output_dataType == VidData_Type.DATABASE_COL ) {
            return base.addInput(obj, index);
        }
        return false;
    }

    public override string ToString() {
        Vid_Object index1 = inputs.getInput_atIndex(0);
        if(index1 != null) {
            if (isREGX) {
                return index1.ToString()+ " REGXP \'" + data + "\'";
            }
            else {
                if (isNOT) {
                    return index1.ToString() + "NOT LIKE \'" + data + "\'";
                }
                else {
                    return index1.ToString() + "LIKE \'" + data + "\'";
                }
            }
        }
        return "error";
    }
}
