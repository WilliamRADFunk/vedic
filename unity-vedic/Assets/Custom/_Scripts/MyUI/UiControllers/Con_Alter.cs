﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Con_Alter : MonoBehaviour {

    public Vid_AlterQuery vidObj;
    public Text dataText;

    // Use this for initialization
    void Start () {
        switch (vidObj.state) {
            case Vid_AlterQuery.AlterState.ADD_COL:
                if (dataText != null) {
                    dataText.text = "ADD Col";
                }
                break;
            case Vid_AlterQuery.AlterState.DROP_COL:
                if (dataText != null) {
                    dataText.text = "DROP Col";
                }
                break;
        }
    }
	

}