using UnityEngine;
using System.Collections;

public abstract class Vid_Return : Vid_Object {
    public Vid_Prefix.Vid_returnValue returnType = Vid_Prefix.Vid_returnValue.VOID;
    FunctionTool ft;

    public override void Awake() {
        base.Awake();
        ft = FunctionTool.getInstance();
    }

}
