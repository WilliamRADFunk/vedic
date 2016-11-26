using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PrintNode : MonoBehaviour {
    
    public UIMove_Tool ut;
    public Text text;
    public InputField panelText;

    public void Start() {
        NodePrinter.GetInstance().text = text;
        NodePrinter.GetInstance().panelText = panelText;
    }

    public void SetMySQLQuery() {
        if (ut == null) { return; }
        Vid_Object vidObj;
        GameObject go =  ut.LastObj();
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
