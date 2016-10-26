using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PrintNode : MonoBehaviour {
    
    public UIMove_Tool ut;
    public Text text;

    public void Start() {
        NodePrinter.GetInstance().text = text;
    }

    public void SetMySQLQuery() {
        if (ut == null) { return; }
        Vid_Object vidObj = ut.gameObject.GetComponent<Vid_Object>();
        if (vidObj != null) {
            NodePrinter.GetInstance().vidObj = vidObj;
        }
    }
}
