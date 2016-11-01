using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class VidContainer : MonoBehaviour {

    public List<LineRenderer> lines;
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
                lines[i].enabled = false;
            }
        }
    }

}
