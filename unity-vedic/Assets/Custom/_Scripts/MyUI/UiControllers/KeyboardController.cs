using UnityEngine;
using System.Collections;
using System.Text;
using UnityEngine.UI;

public class KeyboardController : MonoBehaviour {
    InputField inputField;
    public Text topbar;
    StringBuilder sb = new StringBuilder("");


    public void SetInputField(InputField inputField) {
        this.inputField = inputField;
        StringBuilder sb = new StringBuilder(inputField.text);
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

    public InputField getInputField() {
        return inputField;
    }
}
