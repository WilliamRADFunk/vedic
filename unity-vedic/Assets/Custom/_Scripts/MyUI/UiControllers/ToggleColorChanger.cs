using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ToggleColorChanger : MonoBehaviour {
    //UIMove_Tool moveTool;
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

    public void ChangeColor(bool b) {
        Image i = GetComponent<Image>();
        Toggle t = GetComponent<Toggle>();
        //if (b) {
        //    i.color = Color.green;
        //}
        //else {

        //    i.color = Color.white;
        //}
    }
}
