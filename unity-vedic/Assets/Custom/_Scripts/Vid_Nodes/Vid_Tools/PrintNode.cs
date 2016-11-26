using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PrintNode : MonoBehaviour {
    
    public UIMove_Tool ut;
    public Text LobbyText;
    public Text BrowserText;
    public Text QueryText;
    public Text AnalyticText;
    public InputField panelText;

    public void Start() {
        NodePrinter.GetInstance().LobbyText = LobbyText;
        NodePrinter.GetInstance().BrowserText = BrowserText;
        NodePrinter.GetInstance().QueryText = QueryText;
        NodePrinter.GetInstance().AnalyticText = AnalyticText;
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
