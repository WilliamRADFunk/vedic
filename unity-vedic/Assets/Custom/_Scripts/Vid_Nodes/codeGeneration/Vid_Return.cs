using UnityEngine;
using System.Collections;

public abstract class Vid_Return : Vid_SequenceableObject {
    public Vid_Prefix.Vid_returnValue returnType = Vid_Prefix.Vid_returnValue.VOID;
    FunctionTool ft;

    public override void Awake() {
        base.Awake();
        ft = FunctionTool.getInstance();
    }
    public override void setSequence(Vid_SequenceableObject s, int index) {
        if (ft.checkReturnType(this)) {
            base.setSequence(s, index);
        }
    }

}
