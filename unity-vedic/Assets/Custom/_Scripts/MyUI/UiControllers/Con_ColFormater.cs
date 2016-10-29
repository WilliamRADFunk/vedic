using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Con_ColFormater : MonoBehaviour {

    public Vid_ColFormater vidObj;
    public List<Toggle> toggles;
  
    // Use this for initialization
    void Start () {
        if(toggles != null) {
            for(int i = 0; i < 4; i++) {
                switch (i) {
                    case 0:
                        toggles[0].isOn = vidObj.notNull;
                        break;
                    case 1:
                        toggles[1].isOn = vidObj.defaultValue;
                        break;
                    case 2:
                        toggles[2].isOn = vidObj.doAutoIncrement;
                        break;
                    case 3:
                        toggles[3].isOn = vidObj.isUNIQUE;
                        break;
                }
            }
        }
	}
	
    public void Toggle(Toggle t) {
        for (int i = 0; i < 4; i++) {
            if (toggles[i].Equals(t)) {
                switch (i) {
                    case 0:
                        vidObj.notNull = toggles[0].isOn;
                        break;
                    case 1:
                        vidObj.defaultValue = toggles[1].isOn;
                        break;
                    case 2:
                        vidObj.doAutoIncrement = toggles[2].isOn;
                        break;
                    case 3:
                        vidObj.isUNIQUE = toggles[3].isOn;
                        break;
                }
            }
        }
    }

}
