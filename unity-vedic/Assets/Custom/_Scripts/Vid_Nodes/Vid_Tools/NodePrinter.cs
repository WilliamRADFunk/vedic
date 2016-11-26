using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NodePrinter {
    private static NodePrinter instance;
    public Vid_Object vidObj;
    public Text LobbyText;
    public Text BrowserText;
    public Text QueryText;
    public Text AnalyticText;
    public InputField panelText;

    private NodePrinter() {}

    public static NodePrinter GetInstance() {
        if(instance == null) {
            instance = new NodePrinter();
        }
        return instance;
    }

    public void PrintText() {
        if(LobbyText != null &&
            vidObj != null) {
            LobbyText.text = vidObj.ToString();
        }
        if (BrowserText != null &&
            vidObj != null)
        {
            BrowserText.text = vidObj.ToString();
        }
        if (QueryText != null &&
            vidObj != null)
        {
            QueryText.text = vidObj.ToString();
        }
        if (AnalyticText != null &&
            vidObj != null)
        {
            AnalyticText.text = vidObj.ToString();
        }
        if (panelText != null &&
            vidObj != null) {
            panelText.text = vidObj.ToString();
        }
    }


}
