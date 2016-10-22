using UnityEngine;
using System.Collections;

public class Vid_MySQL_Pattern: Vid_Object {

    public bool isRegxp = true;
    public bool likeType = true;


    public string data = "defaultString";

    public override void Awake() {
        base.Awake();
        output_dataType = VidData_Type.STRING;
    }

    public override string ToString() {
        if (isRegxp) {
            return "REGXP \'" + data + "\'";
        }
        else {
            if (likeType) {
                return "LIKE \'" + data + "\'";
            }
            else {
                return "NOT LIKE \'" + data + "\'";
            }
        }
    }
}
