using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Leap.Unity.InputModule
{
    public class ToggleToggler : MonoBehaviour
    {
        public Text text;
        public UnityEngine.UI.Image image;
        public Color OnColor;
        public Color OffColor;

        public void SetToggle(Toggle toggle)
        {
            GameObject[] dbTogglers = GameObject.FindGameObjectsWithTag("DatabaseToggle");
            for(int i = 0; i < dbTogglers.Length; i++)
            {
                Toggle otherToggle = dbTogglers[i].GetComponent<Toggle>();
                if (!string.Equals(dbTogglers[i].name, gameObject.name) && otherToggle.isOn)
                {
                    otherToggle.isOn = false;
                    dbTogglers[i].GetComponent<ToggleToggler>().text.color = new Color(0.3f, 0.3f, 0.3f);
                    dbTogglers[i].GetComponent<ToggleToggler>().image.color = OffColor;
                }
            }
            if (toggle.isOn)
            {
                // text.text = "On"; // Removed to keep db save toggles from changing.
                text.color = Color.white;
                image.color = OnColor;
            }
            else
            {
                // text.text = "Off"; // Removed to keep db save toggles from changing.
                text.color = new Color(0.3f, 0.3f, 0.3f);
                image.color = OffColor;
            }
        }
    }
}