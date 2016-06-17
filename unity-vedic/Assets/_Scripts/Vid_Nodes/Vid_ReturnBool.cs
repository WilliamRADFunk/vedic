using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
using System.Text;

public class Vid_ReturnBool : Vid_Return
{

    public Vid_ReturnBool()
    {
        inputs = new Vid_ObjectInputs(1);
    }

    public override bool addInput(Vid_Data data)
    {
        if (data.getVidData_type() == VidData_Type.BOOL)
        {
            return inputs.setInput(data);
        }
        return false;
    }

    public override void stringify()
    {
        StringBuilder sb = new StringBuilder("return " + inputs.getInput_atIndex(0).getData() + ";");
        StringBuilder mainStringBuilder = tokenFactory.getStringBuilder();

        String token = tokenFactory.popToken();

        /** Should replace theses steps into a function**/
        mainStringBuilder.Replace(token, sb.ToString());
    }
}
