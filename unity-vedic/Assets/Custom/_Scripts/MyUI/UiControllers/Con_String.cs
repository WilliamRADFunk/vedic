using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class Con_String : Con_Con {

    public Vid_String vidObj;
    public Text dataText;
    public InputField inField_Value;

    // Use this for initialization
    void Start () {
        base.Start();
        if (vidObj != null && inField_Value != null
                              && dataText != null) {
            inField_Value.text = vidObj.data;
            dataText.text = inField_Value.text;
        }

    }


    public void SetValue(InputField inField) {
        vidObj.data = inField.text;
        dataText.text = inField.text;
    }
}
