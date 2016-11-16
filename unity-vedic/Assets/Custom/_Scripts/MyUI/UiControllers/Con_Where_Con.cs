using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Con_Where_Con : Con_Con {

    public Vid_Where_Condition vidObj;
    public Text dataText;

    bool flag = false;

    // Use this for initialization
    void Start() {
        base.Start();
        if (vidObj != null &&
                dataText != null ) {
            dataText.text = PrintCondition();
        }
    }

    public void ToogleRight_ConditionType() {
        WhereStatment_Type conditionType = vidObj.conditionType;
        switch (conditionType) {
            case WhereStatment_Type.LESS:
                ToggleHelper(WhereStatment_Type.LESS_EQU, "<=", 244);
                break;
            case WhereStatment_Type.LESS_EQU:
                ToggleHelper(WhereStatment_Type.GREATER, ">", 244);
                break;
            case WhereStatment_Type.GREATER:
                ToggleHelper(WhereStatment_Type.GREATER_EQU, ">=", 244);
                break;
            case WhereStatment_Type.GREATER_EQU:
                ToggleHelper(WhereStatment_Type.EQU, "=", 244);
                break;
            case WhereStatment_Type.EQU:
                ToggleHelper(WhereStatment_Type.NOT_EQU, "<>", 244);
                break;
            case WhereStatment_Type.NOT_EQU:
                ToggleHelper(WhereStatment_Type.IN, "IN", 150);
                break;
            case WhereStatment_Type.IN:
                ToggleHelper(WhereStatment_Type.EXISTS, "EXISTS", 75);
                break;
            case WhereStatment_Type.EXISTS:
                ToggleHelper(WhereStatment_Type.NOT_EXISTS, "NOT_EXISTS", 50);
                break;
            case WhereStatment_Type.NOT_EXISTS:
                ToggleHelper(WhereStatment_Type.LESS, "<", 244);
                break;
        }
        CheckInputs();
    }
    public void ToogleLeft_ConditionType() {
    WhereStatment_Type conditionType = vidObj.conditionType;
    switch (conditionType) {
            case WhereStatment_Type.LESS:
                ToggleHelper(WhereStatment_Type.NOT_EXISTS, "NOT_EXISTS", 50);
                break;
            case WhereStatment_Type.LESS_EQU:
                ToggleHelper(WhereStatment_Type.LESS, "<", 244);
                break;
            case WhereStatment_Type.GREATER:
                ToggleHelper(WhereStatment_Type.LESS_EQU, "<=", 244);
                break;
            case WhereStatment_Type.GREATER_EQU:
                ToggleHelper(WhereStatment_Type.GREATER, "<", 244);
                break;
            case WhereStatment_Type.EQU:
                ToggleHelper(WhereStatment_Type.GREATER_EQU, "<=", 244);
                break;
            case WhereStatment_Type.NOT_EQU:
                ToggleHelper(WhereStatment_Type.EQU, "=", 244);
                break;
            case WhereStatment_Type.IN:
                ToggleHelper(WhereStatment_Type.NOT_EQU, "<>", 244);
                break;
            case WhereStatment_Type.EXISTS:
                ToggleHelper(WhereStatment_Type.IN, "IN", 150);
                break;
            case WhereStatment_Type.NOT_EXISTS:
                ToggleHelper(WhereStatment_Type.EXISTS, "EXISTS", 75);
                break;
        }
        CheckInputs();
}

    public void CheckInputs() {
        WhereStatment_Type conditionType = vidObj.conditionType;
        switch (conditionType) {
            case WhereStatment_Type.LESS:
            case WhereStatment_Type.LESS_EQU:
            case WhereStatment_Type.GREATER:
            case WhereStatment_Type.GREATER_EQU:
            case WhereStatment_Type.EQU:
            case WhereStatment_Type.NOT_EQU:
                ConnectionChecker_NOTEQU(0, VidData_Type.DATABASE_COL);
                ConnectionChecker_EQU(1, VidData_Type.Q_SELECT);
                break;
            case WhereStatment_Type.IN:
                ConnectionChecker_NOTEQU(0, VidData_Type.DATABASE_COL);
                ConnectionChecker_NOTEQU(1, VidData_Type.Q_SELECT);
                break;
            case WhereStatment_Type.EXISTS:
            case WhereStatment_Type.NOT_EXISTS:
                ConnectionChecker(0);
                ConnectionChecker_NOTEQU(1, VidData_Type.Q_SELECT);
                break;
        }
    }

    private void ToggleHelper(WhereStatment_Type changeType, string s, int fountSize) {
        vidObj.conditionType = changeType;
        dataText.text = s;
        dataText.fontSize = fountSize;
    }

    private void ConnectionChecker(int index) {
        VidContainer vc = gameObject.GetComponent<VidContainer>();
        if (vidObj.inputs.getInput_atIndex(index) != null) {
            vc.lines[index].BreakConnetion();
        }
    }
    private void ConnectionChecker_NOTEQU(int index, VidData_Type dataType) {
        VidContainer vc = gameObject.GetComponent<VidContainer>();
        if (vidObj.inputs.getInput_atIndex(index) != null) {
            if (vidObj.inputs.getInput_atIndex(index).output_dataType != dataType) {
                vc.lines[index].BreakConnetion();
            }
        }
    }
    private void ConnectionChecker_EQU(int index, VidData_Type dataType) {
        VidContainer vc = gameObject.GetComponent<VidContainer>();
        if (vidObj.inputs.getInput_atIndex(index) != null) {
            if (vidObj.inputs.getInput_atIndex(index).output_dataType == dataType) {
                vc.lines[index].BreakConnetion();
            }
        }
    }

    public string PrintCondition() {
        WhereStatment_Type conditionType = vidObj.conditionType;
        switch (conditionType) {
            case WhereStatment_Type.LESS:
                return "<";
            case WhereStatment_Type.LESS_EQU:
                return "<=";
            case WhereStatment_Type.GREATER:
                return ">";
            case WhereStatment_Type.GREATER_EQU:
                return ">=";
            case WhereStatment_Type.EQU:
                return "=";
            case WhereStatment_Type.NOT_EQU:
                return "<>";
            case WhereStatment_Type.IN:
                return "IN";
            case WhereStatment_Type.EXISTS:
                return "EXISTS";
            case WhereStatment_Type.NOT_EXISTS:
                return "NOT EXISTS";

        }
        return "=";
    }
}
