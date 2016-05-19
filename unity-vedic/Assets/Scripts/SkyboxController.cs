using UnityEngine;
using System.Collections;

public class SkyboxController : MonoBehaviour {

    //Declare materials for skybox
    public Material sky1;
    public Material sky2;

	// Use this for initialization
	void Start ()
    {
        InvokeRepeating("Changer", 1, 5.0f);
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    void Changer()
    {
        if(RenderSettings.skybox == sky1)
        {
            RenderSettings.skybox = sky2;
            Debug.Log(sky1.HasProperty("Atmoshpere Thickness"));
        }
        else
        {
            RenderSettings.skybox = sky1;
        }
    }
}
