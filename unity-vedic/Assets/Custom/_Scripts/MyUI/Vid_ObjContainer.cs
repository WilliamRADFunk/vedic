using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Vid_ObjContainer : MonoBehaviour {
    public GameObject selectButton;
    public Image selectButton_background;



    public Text getText()
    {
        if(selectButton == null) { return null; }
        return selectButton.GetComponentInChildren<Text>();
    }

}
