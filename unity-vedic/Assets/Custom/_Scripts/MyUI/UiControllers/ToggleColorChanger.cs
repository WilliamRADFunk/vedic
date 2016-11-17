using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ToggleColorChanger : MonoBehaviour {
    //UIMove_Tool moveTool;

    bool isColored = false;

    public void ChangeColor(Toggle t) {
        Image i = GetComponent<Image>();
        if(i != null) {
            if (t.isOn) {
                i.color = Color.green;
            }
            else {
                i.color = Color.white;
            }
        }
    }

    public void ChangeColor() {
        Image i = GetComponent<Image>();
        if (!isColored) {
            i.color = Color.yellow;
        }
        else {

            i.color = Color.white;
        }
        isColored = isColored ? false : true;
    }
}
