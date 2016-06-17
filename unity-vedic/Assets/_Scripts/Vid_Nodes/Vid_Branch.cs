using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
using System.Text;

public class Vid_Branch : Vid_Object {

    public Vid_Object sequence2;

    public Vid_Branch() :base()
    {
        inputs = new Vid_ObjectInputs(1);
    }

    public new void Awake()
    {
        base.Awake();
        inputs = new Vid_ObjectInputs(1);
    }

    public override void stringify()
    {
        // personal text
        StringBuilder sb = new StringBuilder();
        sb.Append("if( " + inputs.getInput_atIndex(0).getData() + " ){ \n");
        sb.Append(tokenFactory.generateToken() + "\n");
        sb.Append("} \n");

        StringBuilder mainStringBuilder = tokenFactory.getStringBuilder();

        sb.Append("else{ \n");
        sb.Append(tokenFactory.generateToken() + "\n");
        sb.Append("} \n");

        String token = tokenFactory.popToken();
        // Add To the file text.
        mainStringBuilder.Replace(token, sb.ToString());
           

        if (sequence != null)
        {
            sequence.stringify();
        }
        if (sequence2 != null)
        {
            sequence.stringify();
        }
    }

    public override bool addInput(Vid_Data data)
    {
        if (data.getVidData_type() == VidData_Type.BOOL)
        {
            return inputs.setInput(data);
        }
        return false;
    }

        public void setSequence2(Vid_Object sequence2) {
        this.sequence2 = sequence2;
    }


}
