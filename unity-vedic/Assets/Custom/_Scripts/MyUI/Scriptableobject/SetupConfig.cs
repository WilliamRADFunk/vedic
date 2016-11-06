using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class SetupConfig : MonoBehaviour {

    public List<InputButton> inbuttons;
    public List<OutputButton> outbuttons;
    public List<Toggle> selectButtons;

    public void Update() {
        if(outbuttons != null &&
            inbuttons != null) {
            if(inbuttons.Count == outbuttons.Count) {
                for (int i = 0; i < inbuttons.Count;i++) {
                    outbuttons[i].buttonPressed();
                    inbuttons[i].buttonPressed();
                }
                if(selectButtons != null) {
                    for (int i = 0; i < selectButtons.Count; i++) {
                        selectButtons[i].isOn = true;
                    }
                }
            }
        }
        Destroy(this);
    }
}
