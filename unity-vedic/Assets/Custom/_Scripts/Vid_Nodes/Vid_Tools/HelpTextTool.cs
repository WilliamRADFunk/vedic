using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HelpTextTool  {
    public static HelpTextTool instance;

    public Text textInfo;
    private HelpTextTool() {}

    public static HelpTextTool GetInstance() {
        if(instance == null) {
            instance = new HelpTextTool();
        }
        return instance;
    }

    public void setText(string s) {
        if(textInfo != null) {
            textInfo.text = s;
        }
    }

}
