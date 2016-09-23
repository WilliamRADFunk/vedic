using UnityEngine;
using System.Text;
using UnityEngine.UI;

public class KeyboardController : MonoBehaviour
{
    InputField inputField;
    StringBuilder sb = new StringBuilder("");

    public void SetInputField(InputField inputField)
    {
        this.inputField = inputField;
        StringBuilder sb = new StringBuilder(inputField.text);
    }
    public void BuildString(string s)
    {
        if (inputField == null)
        {
            return;
        }
        sb.Append(s, inputField.caretPosition, s.Length);
        inputField.text = sb.ToString();
    }
    public override string ToString()
    {
        return sb.ToString();
    }
    public void BackSpace()
    {
        if (inputField == null)
        {
            return;
        }
        if (sb.Length > 0)
        {
            sb.Length--;
        }
        inputField.text = sb.ToString();
    }
    public void ClearText()
    {
        sb = new StringBuilder();
    }
    public InputField GetInputField()
    {
        return inputField;
    }
}