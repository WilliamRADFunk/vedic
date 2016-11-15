using UnityEngine;
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
                break;
            case VidData_Type.DATABASE_COL:
                vidObj.output_dataType = VidData_Type.STRING;
                dataText.text = "STRING";
                break;
            case VidData_Type.STRING:
                vidObj.output_dataType = VidData_Type.NUM;
                dataText.text = "NUM";
                break;
            case VidData_Type.NUM:
                vidObj.output_dataType = VidData_Type.LIST;
                dataText.text = "LIST";
                break;
            case VidData_Type.LIST:
                vidObj.output_dataType = VidData_Type.ASSINMENT;
                dataText.text = "ASSINMENT";
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
                break;
            case VidData_Type.STRING:
                vidObj.output_dataType = VidData_Type.DATABASE_COL;
                dataText.text = "COL";
                break;
            case VidData_Type.NUM:
                vidObj.output_dataType = VidData_Type.STRING;
                dataText.text = "STRING";
                break;
            case VidData_Type.LIST:
                vidObj.output_dataType = VidData_Type.NUM;
                dataText.text = "NUM";
                break;
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
