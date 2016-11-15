using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Con_Where_Con : Con_Con {

    public Vid_Where_Condition vidObj;
    public Text dataText;


    // Use this for initialization
    void Start() {
        base.Start();
        if (vidObj != null &&
                dataText != null ) {
            dataText.text = PrintCondition();
        }
    }

    public void ToogleRight_ConditionType() {
        Condition_Type conditionType = vidObj.conditionType;
        switch (conditionType) {
            case Condition_Type.LESS:
                vidObj.conditionType = Condition_Type.LESS_EQU;
                dataText.text = "<=";
                break;
            case Condition_Type.LESS_EQU:
                vidObj.conditionType = Condition_Type.GREATER;
                dataText.text = ">";
                break;
            case Condition_Type.GREATER:
                vidObj.conditionType = Condition_Type.GREATER_EQU;
                dataText.text = ">=";
                break;
            case Condition_Type.GREATER_EQU:
                vidObj.conditionType = Condition_Type.EQU;
                dataText.text = "=";
                break;
            case Condition_Type.EQU:
                vidObj.conditionType = Condition_Type.NOT_EQU;
                dataText.text = "<>";
                break;
            case Condition_Type.NOT_EQU:
                vidObj.conditionType = Condition_Type.LESS;
                dataText.text = "<";
                break;
        }
    }
    public void ToogleLeft_ConditionType() {
    Condition_Type conditionType = vidObj.conditionType;
    switch (conditionType) {
        case Condition_Type.LESS:
            vidObj.conditionType = Condition_Type.NOT_EQU;
            dataText.text = "<>";
            break;
        case Condition_Type.LESS_EQU:
            vidObj.conditionType = Condition_Type.LESS;
            dataText.text = "<";
                break;
        case Condition_Type.GREATER:
            vidObj.conditionType = Condition_Type.LESS_EQU;
            dataText.text = "<=";
                break;
        case Condition_Type.GREATER_EQU:
            vidObj.conditionType = Condition_Type.GREATER;
            dataText.text = ">";
                break;
        case Condition_Type.EQU:
            vidObj.conditionType = Condition_Type.GREATER_EQU;
            dataText.text = ">=";
                break;
        case Condition_Type.NOT_EQU:
            vidObj.conditionType = Condition_Type.EQU;
            dataText.text = "=";
                break;
    }
}

    public string PrintCondition() {
        Condition_Type conditionType = vidObj.conditionType;
        switch (conditionType) {
            case Condition_Type.LESS:
                return "<";
            case Condition_Type.LESS_EQU:
                return "<=";
            case Condition_Type.GREATER:
                return ">";
            case Condition_Type.GREATER_EQU:
                return ">=";
            case Condition_Type.EQU:
                return "=";
            case Condition_Type.NOT_EQU:
                return "<>";
        }
        return "=";
    }
}
