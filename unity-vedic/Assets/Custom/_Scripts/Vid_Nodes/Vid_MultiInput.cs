using UnityEngine;
using System.Collections.Generic;
using System.Text;
using System;

public class Vid_MultiInput : Vid_Object {
    public int inputSize = 1;

    public Vid_MultiInput(){
        output_dataType = VidData_Type.LIST;
    }

    public override void Awake() {
        base.Awake();
        inputs = new Vid_ObjectInputs(inputSize);
    }

    public override string ToString() {
        return writeInputs();
    }
    /*Builder functions*/
    public override bool addInput(Vid_Object obj, int index) {
        //if output_dataType == VidData_Type.LIST i don't cair what type of input it holds
        if (output_dataType == VidData_Type.LIST) {
            return base.addInput(obj, index);
        }
        // output_dataType != VidData_Type.LIST I do cair about it's inputs.
        else if (obj.output_dataType == output_dataType) {
            return base.addInput(obj, index);
        }
        return false;
    }
    public virtual void incromentInputs() {
        inputSize++;
        Vid_ObjectInputs newInputs = new Vid_ObjectInputs(inputSize);
        for(int i =0; i < inputSize - 1; i++) {
            newInputs.setInput_atIndex(inputs.getInput_atIndex(i), i);
        }
        inputs = newInputs;
    }
    public virtual void decromentInputs() {
        if(inputSize > 1) {
            inputSize--;
        }
        Vid_ObjectInputs newInputs = new Vid_ObjectInputs(inputSize);
        for (int i = 0; i < inputSize; i++) {
            newInputs.setInput_atIndex(inputs.getInput_atIndex(i), i);
        }
        inputs = newInputs;
    }
    /*Helper functions*/
    private string writeInputs() {
        StringBuilder sb = new StringBuilder();
        for (int i =0; i<inputs.getSize();i++) {
            Vid_Object obj = inputs.getInput_atIndex(i);
            if (obj != null) {
                if (i == 0) {
                    sb.Append(obj.ToString());
                    if (0 < inputs.getSize()) {
                        sb.AppendLine(",");
                    }
                }
                else {
                    sb.Append(TabTool.TabCount() + obj.ToString());
                    if (i < inputs.getSize() - 1) {
                        sb.AppendLine(",");
                    }
                }
            }
        }
        return sb.ToString();
    }
}
