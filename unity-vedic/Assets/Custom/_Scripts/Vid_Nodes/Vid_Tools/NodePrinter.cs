using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NodePrinter {
    private static NodePrinter instance;
    public Vid_Object vidObj;
    public Text text;
    public InputField panelText;

    private NodePrinter() {}

    public static NodePrinter GetInstance() {
        if(instance == null) {
            instance = new NodePrinter();
        }
        return instance;
    }

    public void PrintText() {
        if(text != null &&
            vidObj != null) {
            text.text = vidObj.ToString();
        }
        if (panelText != null &&
            vidObj != null) {
            panelText.text = vidObj.ToString();
        }
    }


}
