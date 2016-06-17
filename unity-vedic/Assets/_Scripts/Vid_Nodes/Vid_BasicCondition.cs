using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
using System.Text;

public class Vid_BasicCondition : Vid_Object {

    Condition_Type conditionType = Condition_Type.LESS;

    public Vid_BasicCondition() : base()
    {
        vidObject_SetUP();
    }

    public new void Awake()
    {
        base.Awake();
        vidObject_SetUP();
    }

    public override void stringify()
    {
        StringBuilder sb = new StringBuilder();
        switch (conditionType)
        {
            case Condition_Type.LESS:
                sb.Append("( " + inputs.getInput_atIndex(0).getData());
                sb.Append(" <" + inputs.getInput_atIndex(1).getData() + " )");
                break;
            case Condition_Type.LESS_EQU:
                sb.Append("( " + inputs.getInput_atIndex(0).getData());
                sb.Append(" <=" + inputs.getInput_atIndex(1).getData() + " )");
                break;
            case Condition_Type.GREATER:
                sb.Append("( " + inputs.getInput_atIndex(0).getData());
                sb.Append(" >" + inputs.getInput_atIndex(1).getData() + " )");
                break;
            case Condition_Type.GREATER_EQU:
                sb.Append("( " + inputs.getInput_atIndex(0).getData());
                sb.Append(" >=" + inputs.getInput_atIndex(1).getData() + " )");
                break;
            case Condition_Type.EQU:
                sb.Append("( " + inputs.getInput_atIndex(0).getData());
                sb.Append(" ==" + inputs.getInput_atIndex(1).getData() + " )");
                break;
            case Condition_Type.NOT_EQU:
                sb.Append("( " + inputs.getInput_atIndex(0).getData());
                sb.Append(" !=" + inputs.getInput_atIndex(1).getData() + " )");
                break;
            default:
                break;
        }
        output.setData(sb.ToString());
        if (sequence != null)
        {
            sequence.stringify();
        }
    }

    public override bool addInput(Vid_Data data)
    {
        Debug.Log(data == null);
        if (data.getVidData_type() == VidData_Type.BOOL
                || data.getVidData_type() == VidData_Type.NUM)
        {
            return inputs.setInput(data);
        }
        return false;
    }

    private void vidObject_SetUP()
    {
        inputs = new Vid_ObjectInputs(2);
        output = new Vid_Data(VidData_Type.BOOL, this);
        output.setData("null");
    }

}
