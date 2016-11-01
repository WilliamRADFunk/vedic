using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TactileText : MonoBehaviour
{

    public Text birdo;

    public void UpdateText(string message, bool active)
    {
        if (!active)
        {
            if (birdo.text == message)
            {
                birdo.text = "";
            }
        }
        else
        {
            birdo.text = message;
        }

    }
}