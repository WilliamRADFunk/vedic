using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ToggleColorChanger : MonoBehaviour {
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
}
