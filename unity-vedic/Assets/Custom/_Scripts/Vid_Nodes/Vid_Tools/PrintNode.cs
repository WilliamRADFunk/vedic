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
        Vid_Object vidObj;
        GameObject go =  ut.GetRoot();
        if(go != null) {
            vidObj = go.GetComponent<Vid_Object>();
            if (vidObj != null) {
                NodePrinter np = NodePrinter.GetInstance();
                np.vidObj = vidObj;
                np.PrintText();
            }
        }
    }
}
