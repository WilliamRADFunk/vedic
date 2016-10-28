using UnityEngine;
using UnityEngine.UI;

public class Controller_HelpText : MonoBehaviour {

    public Text t;
	// Use this for initialization
	void Start () {
        HelpTextTool.GetInstance().textInfo = t;
        //Invoke("Test",5.0f);
	}

    private void Test() {
        HelpTextTool.GetInstance().setText("TEST:1");
    }
	
}
