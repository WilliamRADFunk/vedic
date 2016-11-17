using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Con_Number : Con_Con {
    public Vid_Number vidObj;
    public Text dataText;
    public InputField inField_Value;
    public Text numberDataTypeText;


    void Start() {
        base.Start();
        if (vidObj != null && inField_Value != null
                           && dataText != null) {
            inField_Value.text = vidObj.ToString();
            dataText.text = inField_Value.text;
        }
        if (vidObj != null && numberDataTypeText != null) {
            VidNum_Type type = vidObj.type;
            switch (type) {
                case VidNum_Type.INT:
                    numberDataTypeText.text = "int";
                    break;
                case VidNum_Type.FLOAT:
                    numberDataTypeText.text = "float";
                    break;
                case VidNum_Type.DOUBLE:
                    numberDataTypeText.text = "double";
                    break;
                case VidNum_Type.LONG:
                    numberDataTypeText.text = "long";
                    break;
            }
        }
    }

    public void ToogleR() {
        VidNum_Type type = vidObj.type;
        switch (type) {
            case VidNum_Type.INT:
                vidObj.type = VidNum_Type.FLOAT;
                numberDataTypeText.text = "float";
                break;
            case VidNum_Type.FLOAT:
                vidObj.type = VidNum_Type.DOUBLE;
                numberDataTypeText.text = "double";
                break;
            case VidNum_Type.DOUBLE:
                vidObj.type = VidNum_Type.LONG;
                numberDataTypeText.text = "long";
                break;
            case VidNum_Type.LONG:
                vidObj.type = VidNum_Type.INT;
                numberDataTypeText.text = "int";
                break;
        }
    }
    public void ToogleL() {
        VidNum_Type type = vidObj.type;
        switch (type) {
            case VidNum_Type.INT:
                vidObj.type = VidNum_Type.LONG;
                numberDataTypeText.text = "long";
                break;
            case VidNum_Type.FLOAT:
                vidObj.type = VidNum_Type.INT;
                numberDataTypeText.text = "int";
                break;
            case VidNum_Type.DOUBLE:
                vidObj.type = VidNum_Type.FLOAT;
                numberDataTypeText.text = "float";
                break;
            case VidNum_Type.LONG:
                vidObj.type = VidNum_Type.DOUBLE;
                numberDataTypeText.text = "double";
                break;
        }
    }

    public void SetValue(InputField inField) {
        vidObj.setData(inField.text);
        dataText.text = inField.text;
    }
}
