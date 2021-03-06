﻿using UnityEngine;
using UnityEngine.UI;
using DatabaseUtilities;
using System.Collections.Generic;

public class Con_MultiInput : Con_Con {
    public Vid_MultiInput vidObj;
    public Text dataText;


    // Use this for initialization
    void Start() {
        base.Start();
        if (vidObj != null &&
                dataText != null) {
            dataText.text = PrintCondition();
        }
    }

    public void ToogleRight_DataType() {
        VidData_Type output_dataType = vidObj.output_dataType;
        switch (output_dataType) {
            case VidData_Type.ASSINMENT:
                dataText.text = "COL";
                vidObj.output_dataType = VidData_Type.DATABASE_COL;
                for(int i =0; i< vidObj.inputSize; i++) {
                    ConnectionChecker_NOTEQU(i, VidData_Type.DATABASE_COL);
                }
                break;
            case VidData_Type.DATABASE_COL:
                vidObj.output_dataType = VidData_Type.STRING;
                for (int i = 0; i < vidObj.inputSize; i++) {
                    ConnectionChecker_NOTEQU(i, VidData_Type.STRING);
                }
                dataText.text = "STRING";
                break;
            case VidData_Type.STRING:
                vidObj.output_dataType = VidData_Type.NUM;
                dataText.text = "NUM";
                for (int i = 0; i < vidObj.inputSize; i++) {
                    ConnectionChecker_NOTEQU(i, VidData_Type.NUM);
                }
                break;
            case VidData_Type.NUM:
                vidObj.output_dataType = VidData_Type.LIST;
                dataText.text = "LIST";
                break;
            case VidData_Type.LIST:
                vidObj.output_dataType = VidData_Type.ASSINMENT;
                dataText.text = "ASSINMENT";
                for (int i = 0; i < vidObj.inputSize; i++) {
                    ConnectionChecker_NOTEQU(i, VidData_Type.ASSINMENT);
                }
                break;
        }
    }
    public void ToogleLeft_DataType() {
        VidData_Type output_dataType = vidObj.output_dataType;
        switch (output_dataType) {
            case VidData_Type.ASSINMENT:
                dataText.text = "LIST";
                vidObj.output_dataType = VidData_Type.LIST;
                break;
            case VidData_Type.DATABASE_COL:
                vidObj.output_dataType = VidData_Type.ASSINMENT;
                dataText.text = "ASSINMENT";
                for (int i = 0; i < vidObj.inputSize; i++) {
                    ConnectionChecker_NOTEQU(i, VidData_Type.ASSINMENT);
                }
                break;
            case VidData_Type.STRING:
                vidObj.output_dataType = VidData_Type.DATABASE_COL;
                dataText.text = "COL";
                for (int i = 0; i < vidObj.inputSize; i++) {
                    ConnectionChecker_NOTEQU(i, VidData_Type.DATABASE_COL);
                }
                break;
            case VidData_Type.NUM:
                vidObj.output_dataType = VidData_Type.STRING;
                dataText.text = "STRING";
                for (int i = 0; i < vidObj.inputSize; i++) {
                    ConnectionChecker_NOTEQU(i, VidData_Type.STRING);
                }
                break;
            case VidData_Type.LIST:
                vidObj.output_dataType = VidData_Type.NUM;
                dataText.text = "NUM";
                for (int i = 0; i < vidObj.inputSize; i++) {
                    ConnectionChecker_NOTEQU(i, VidData_Type.NUM);
                }
                break;
        }
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
        VidData_Type output_dataType = vidObj.output_dataType;
        switch (output_dataType) {
            case VidData_Type.ASSINMENT:
                return "ASSINMENT"; 
            case VidData_Type.DATABASE_COL:
                return "COL";
            case VidData_Type.STRING:
                return "STRING";
            case VidData_Type.NUM:
                return "NUM";
            case VidData_Type.LIST:
                return "LIST";
        }
        return "";
    }
}
