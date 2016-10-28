using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Con_Key : MonoBehaviour {

    public Vid_Key vidObj;
    public Text dataText;

    // Use this for initialization
    void Start() {
        switch (vidObj.keyType) {
            case Vid_Key.KeyType.PRIMARY:
                if (dataText != null) {
                    dataText.text = "PRIMARY";
                }
                break;
            case Vid_Key.KeyType.FOREIGN:
                if (dataText != null) {
                    dataText.text = "FOREIGN";
                }
                break;
        }
    }
    public void Toggle() {
        switch (vidObj.keyType) {
            case Vid_Key.KeyType.PRIMARY:
                if (dataText != null) {
                    dataText.text = "FOREIGN";
                }
                vidObj.keyType = Vid_Key.KeyType.FOREIGN;
                break;
            case Vid_Key.KeyType.FOREIGN:
               if (dataText != null) {
                    dataText.text = "PRIMARY";
                }
                vidObj.keyType = Vid_Key.KeyType.PRIMARY;
                break;
        }
    }
}
