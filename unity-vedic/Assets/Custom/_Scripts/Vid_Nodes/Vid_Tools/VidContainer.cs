using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class VidContainer : MonoBehaviour {

    public List<InputButton> lines;
    public ToggleColorChanger colorChanger;

    public void ResetColor() {
        colorChanger.ChangeColor(false);
    }
    public void DisableLines() {
        if(lines == null ) {
            return;
        }
        for (int i=0; i< lines.Count;i++) {
            if(lines[i] != null) { 
                Debug.Log(lines[i].ToString());
                lines[i].drawline = false;
            }
        }
    }

    public void ReConnectData() {
        if (lines == null) {
            return;
        }
        for (int i = 0; i < lines.Count; i++) {
            if (lines[i] != null) {
                lines[i].RemakeConnetion();
            }
        }
    }

    public void Deselect() {
        Toggle t = colorChanger.gameObject.GetComponent<Toggle>();
        if(t == null) {
            return;
        }
        t.isOn = false;
    }
    public void Select() {
        Toggle t = colorChanger.gameObject.GetComponent<Toggle>();
        if (t == null) {
            return;
        }
        t.isOn = true;
    }
}
