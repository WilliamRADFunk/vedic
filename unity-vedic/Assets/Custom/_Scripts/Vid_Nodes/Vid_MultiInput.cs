using UnityEngine;
using System.Collections.Generic;
using System.Text;
using System;

public class Vid_MultiInput : Vid_Object {

    public int inputSize = 1;

    public Vid_MultiInput(){
        output_dataType = VidData_Type.NUM;
    }


    public override void Awake() {
        base.Awake();
        acceptableInputs = new VidData_Type[1];
            acceptableInputs[0] = output_dataType;
        inputs = new Vid_ObjectInputs(inputSize);
    }

    public override string ToString() {
        return writeInputs();
    }

    /*Builder functions*/
    public override bool addInput(Vid_Object obj, int index) {
        if (output_dataType == VidData_Type.LIST) {
            return base.addInput(obj, index);
        }
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
        StringBuilder sb = new StringBuilder("");
        for (int i =0; i<inputs.getSize();i++) {
            Vid_Object obj = inputs.getInput_atIndex(i);
            if (obj != null) {
                sb.Append(obj.ToString());
                if (i < inputs.getSize() - 1) {
                    sb.Append(", ");
                }
            }
        }
        return sb.ToString();
    }

}
