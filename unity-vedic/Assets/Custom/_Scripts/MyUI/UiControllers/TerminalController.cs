using UnityEngine;
using UnityEngine.UI;

public class TerminalController : MonoBehaviour {

    public GameObject holding;
    public Text terminalText;
    public Vid_Object vidObj;

    public TerminalController() {
       // terminalText = GetComponentInChildren<Text>();
    }

    public void updateText(GameObject go) {
        vidObj = go.GetComponentInChildren<Vid_Object>();
        if(vidObj == null) {
            Debug.Log("this is a test");
        }
        else {
            terminalText.text = vidObj.ToString();
        }
    }

    public void updateText() {
        vidObj = holding.GetComponentInChildren<Vid_Object>();
        terminalText.text = vidObj.ToString();
    }
}
