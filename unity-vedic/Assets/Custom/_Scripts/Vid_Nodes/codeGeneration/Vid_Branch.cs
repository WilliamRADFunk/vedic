using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
using System.Text;

public class Vid_Branch : Vid_SequenceableObject {

    public override void Awake() 
    {
        base.Awake();
        acceptableInputs = new VidData_Type[1];
            acceptableInputs[0] = VidData_Type.BOOL;
        inputs = new Vid_ObjectInputs(1);
        sequence = new Vid_SequenceableObject[2];
    }

    public override string ToString() {
        StringBuilder sb = new StringBuilder();
        if (inputs.getInput_atIndex(0) == null) {
            sb.Append("null");
        }
        else {
            sb.Append("if( " + inputs.getInput_atIndex(0).ToString() + " ){ \n");
            //sb.Append(tokenFactory.generateToken() + "\n");
            sb.Append("} \n");


            sb.Append("else{ \n");
            //sb.Append(tokenFactory.generateToken() + "\n");
            sb.Append("} \n");
        }
        return sb.ToString();
    }

    /*Builder functions*/
    public override bool addInput(Vid_Object obj, int index)
    {
        if (obj.output_dataType == VidData_Type.BOOL)
        {
            return inputs.setInput_atIndex(obj, index);
        }
        return false;
    }
    public override void stringify(StringBuilder targetString) {
        //// personal text
        //StringBuilder sb = new StringBuilder();

        //sb.Append("if( " + inputs.getInput_atIndex(0).ToString() + " ){ \n");
        ////sb.Append(tokenFactory.generateToken() + "\n");
        //sb.Append("} \n");


        //sb.Append("else{ \n");
        ////sb.Append(tokenFactory.generateToken() + "\n");
        //sb.Append("} \n");

        ////String token = tokenFactory.popToken();
        //// Add To the file text.

        //Debug.Log(targetString.ToString());
        //targetString.Replace(token, sb.ToString());

    }

}
