using UnityEngine;
using System.Collections;

public class Vid_String : Vid_Object {

    public string data = "defaultString";

    public Vid_String() {
        data = "defaultString";
    }
    public override void Awake() {
        base.Awake();
        output_dataType = VidData_Type.STRING;
    }

    public override string ToString() {
        return "\'" + data + "\'";

    }
}
