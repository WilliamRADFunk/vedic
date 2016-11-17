using UnityEngine;
using System.Collections;

public class NodeSystemEssentials : MonoBehaviour {

    public static KeyboardController keyboardController;
    public static UIMove_Tool uiMove_Tool;

    void Awake() {
        keyboardController = GameObject.FindGameObjectWithTag("Keyboard").GetComponent<KeyboardController>();
        uiMove_Tool = GameObject.FindGameObjectWithTag("SelectedObj").GetComponent<UIMove_Tool>();
    }
}
