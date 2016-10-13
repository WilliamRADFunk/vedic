using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TactileText : MonoBehaviour {

    public Text birdo;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void UpdateText(string message, bool active)
    {
        if(!active)
        {
            if(birdo.text == message)
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
