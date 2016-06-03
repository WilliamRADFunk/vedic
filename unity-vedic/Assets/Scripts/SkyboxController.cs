using UnityEngine;
using System.Collections;

public class SkyboxController : MonoBehaviour {

    //Declare materials for skybox
    public Material[] skyTransition;
    public Material sky1;
    public Material sky2;
    bool once;

	// Use this for initialization
	void Start ()
    {
        once = true;
        skyTransition = Resources.LoadAll<Material>("Skybox");
        
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(once)
        {
            once = false;
            StartCoroutine("Changer");
        }
	}

    IEnumerator Changer()
    {
        if(RenderSettings.skybox == sky1)
        {
            for (int i = 0; i < skyTransition.Length; i++)
            {
                RenderSettings.skybox = skyTransition[i];
                yield return new WaitForSeconds(0.02f);
            }
        }
        else
        {
            for(int i = skyTransition.Length; i >= 0; i--)
            {
                RenderSettings.skybox = skyTransition[i];
                yield return new WaitForSeconds(0.02f);
            }
            RenderSettings.skybox = sky2;
            
        }
    }
}
