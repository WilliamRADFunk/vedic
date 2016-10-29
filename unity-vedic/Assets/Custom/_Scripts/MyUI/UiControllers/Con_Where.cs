using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Con_Where : MonoBehaviour {

    public Vid_MySql_Where vidObj;
    public Text dataText;

    public void Start() {
        if(dataText == null) { return; }
        ChangeText();
    }

    public void ToggleR() {
        if (vidObj.IsInFlag()) {
            vidObj.setInFlag(false);
            vidObj.isEXISTS = true;
            dataText.text = "EXISTS";
        }
        else if (vidObj.isEXISTS) {
            vidObj.isEXISTS = false;
            vidObj.setInFlag(false);
            dataText.text = "NOT EXISTS";
        }
        else {
            vidObj.setInFlag(true);
            dataText.text = "IN";
        }
    }
    public void ToggleL() {
        if (vidObj.IsInFlag()) {
            vidObj.setInFlag(false);
            vidObj.isEXISTS = false;
            dataText.text = "NOT EXISTS";
        }
        else if (vidObj.isEXISTS) {
            vidObj.setInFlag(true);
            dataText.text = "IN";
        }
        else {
            vidObj.isEXISTS = true;
            vidObj.setInFlag(false);
            dataText.text = "EXISTS";
        }
    }

    public void ChangeText() {
        if (vidObj.IsInFlag()) {
            dataText.text = "IN";
        }
        else if (vidObj.isEXISTS) {
            dataText.text = "EXISTS";
        }
        else {
            dataText.text = "NOT EXISTS";
        }
    }
}
