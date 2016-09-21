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
            if ((toggle.name).Contains("Database"))
            {
                GameObject[] dbTogglers = GameObject.FindGameObjectsWithTag("DatabaseToggle");
                for (int i = 0; i < dbTogglers.Length; i++)
                {
                    Toggle otherToggle = dbTogglers[i].GetComponent<Toggle>();
                    if (!string.Equals(dbTogglers[i].name, gameObject.name) && otherToggle.isOn)
                    {
                        otherToggle.isOn = false;
                        dbTogglers[i].GetComponent<ToggleToggler>().text.color = new Color(0.3f, 0.3f, 0.3f);
                        dbTogglers[i].GetComponent<ToggleToggler>().image.color = OffColor;
                    }
                }
            }
            if ((toggle.tag).Contains("MusicToggle"))
            {
                GameObject[] musicTogglers = GameObject.FindGameObjectsWithTag("MusicToggle");
                for (int i = 0; i < musicTogglers.Length; i++)
                {
                    Toggle otherToggle = musicTogglers[i].GetComponent<Toggle>();
                    if (!string.Equals(musicTogglers[i].name, gameObject.name) && otherToggle.isOn)
                    {
                        otherToggle.isOn = false;
                        musicTogglers[i].GetComponent<ToggleToggler>().text.color = new Color(0.3f, 0.3f, 0.3f);
                        musicTogglers[i].GetComponent<ToggleToggler>().image.color = OffColor;
                    }
                }
            }
            if (toggle.isOn)
            {
                if ((toggle.tag).Contains("MuteToggle"))
                {
                    text.text = "On";
                }
                image.color = OnColor;
            }
            else
            {
                if ((toggle.tag).Contains("MusicToggle"))
                {
                    GameObject source = GameObject.FindGameObjectWithTag("MusicSource");
                    source.GetComponent<MusicController>().PlaylistStop();
                }
                else if ((toggle.tag).Contains("MuteToggle"))
                {
                    text.text = "Off";
                }
                image.color = OffColor;
            }
        }
    }
}