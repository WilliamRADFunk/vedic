using UnityEngine;
using System.Collections.Generic;
using System.Text;
using UnityEngine.UI;

public class KeyboardController : MonoBehaviour {
    InputField inputField;
    public Text topbar;
    StringBuilder sb = new StringBuilder("");

    public List<GameObject> shiftObj;
    public List<GameObject> normalObj;

    public void SetInputField(InputField inputField) {
        this.inputField = inputField;
        sb = new StringBuilder(inputField.text);
        topbar.text = sb.ToString();
    }
    public void buildString(string s) {
        if(inputField == null) {
            return;
        }
        sb.Append(s, inputField.caretPosition, s.Length);
        inputField.text = sb.ToString();
        topbar.text = sb.ToString();
    }

    public override string ToString() {
        return sb.ToString();
    }



    //SpecalOperations
    public void backSpace() {
        if (inputField == null) {
            return;
        }
        if (sb.Length > 0) {
            sb.Length--;
        }
        inputField.text = sb.ToString();
        topbar.text = sb.ToString();
    }
    public void clearText() {
        sb = new StringBuilder();
    }
    public void Shift(Toggle t) {
        if (t.isOn) {
            foreach(GameObject go in shiftObj) {
                go.SetActive(true);
            }
            foreach (GameObject go in normalObj) {
                go.SetActive(false);
            }
        }
        else {
            foreach (GameObject go in normalObj) {
                go.SetActive(true);
            }
            foreach (GameObject go in shiftObj) {
                go.SetActive(false);
            }
        }
    }

    public InputField getInputField() {
        return inputField;
    }
}
