using UnityEngine;
using System.Collections.Generic;

public class OneInputButton : NodeButton {
    public List<InputButton> listInputs;
    public Vid_Object vidObj;
    
    OutputButton output;

    public override void buttonPressed() {
        if(ct.getOutputButton() != null) {
            output = ct.getOutputButton();
            int i = vidObj.AcceptedInputIndex(output.vid_obj.output_dataType);
            
            if (i != -1 && listInputs.Count > i ) {
                listInputs[i].buttonPressed();
            }
            else {
                ct.resetTool();
            }
        }
    }

}
