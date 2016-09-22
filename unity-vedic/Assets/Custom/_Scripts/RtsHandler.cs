using UnityEngine;
using System.Collections;

public class RtsHandler : MonoBehaviour {

    GameObject tableHarnessInstance;

    Leap.Unity.LeapRTS rtsInstance;
    bool virgin;
    bool toggled;
    bool initialized;

	// Use this for initialization
	void Start () {
        virgin = true;
        toggled = false;
        initialized = false;
        rtsInstance = gameObject.GetComponent<Leap.Unity.LeapRTS>();
	}
	
	// Update is called once per frame
	void Update () {

        if(virgin)
        {
            virgin = false;
            rtsInstance.enabled = false;
        }

        if(initialized)
        {
            if (!toggled)
            {
                rtsInstance.enabled = false;
            }
            else
            {
                rtsInstance.enabled = true;
            }
        }
	}

    public void AllocateTableHarness(GameObject harnessInstance)
    {
        tableHarnessInstance = harnessInstance;
        initialized = true;
    }

    public void InteractOn()
    {

        if(initialized)
        {
            tableHarnessInstance.GetComponent<TableHarness>().BringToUser();
            rtsInstance.enabled = true;
        }
        else
        {
            Debug.Log("InteractOn cannot be reliably called because tableharness is initialized to its observed object.");
        }
        
    }

    public void InteractOff()
    {

        if(initialized)
        {
            rtsInstance.enabled = false;
            tableHarnessInstance.GetComponent<TableHarness>().ResetToDefault();
            
        }
        else
        {
            Debug.Log("InteractOff cannot be reliably called because tableharness is initialized to its observed object.");
        }
    }




      
}
