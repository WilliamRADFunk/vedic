using UnityEngine;
using System.Collections;

public class RtsHandler : MonoBehaviour {

    GameObject tableHarnessInstance;
    Transform rtsMain;

    Vector3 initialLocalPos;
    Vector3 initialLocalScale;

    Leap.Unity.LeapRTS rtsInstance;
    bool virgin;
    bool toggled;
    bool initialized;
    bool firstTime;

	// Use this for initialization
	void Start () {
        virgin = true;
        toggled = false;
        initialized = false;
        firstTime = true;
        rtsInstance = gameObject.GetComponent<Leap.Unity.LeapRTS>();
	}
	
	// Update is called once per frame
	void Update () {

        if(virgin)
        {
            virgin = false;
            rtsMain = gameObject.transform.parent;

            initialLocalPos = gameObject.transform.localPosition;
            initialLocalScale = gameObject.transform.localScale;

            rtsInstance.enabled = false;
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
            if(firstTime)
            {
                firstTime = false;
                BringToUser();
            }
            
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
        }
        else
        {
            Debug.Log("InteractOff cannot be reliably called because tableharness is initialized to its observed object.");
        }
    }

    public void ResetRig()
    {
        if(initialized)
        {
            firstTime = true;
            rtsInstance.enabled = false;
            ResetToDefault();
        }
        
    }

    private void ResetToDefault()
    {
        gameObject.transform.position = initialLocalPos;
    }

    private void BringToUser()
    {
        Transform tempCamLocation = GameObject.FindGameObjectWithTag("MainCamera").transform;

        initialLocalPos = gameObject.transform.position;
        initialLocalScale = gameObject.transform.localScale;

        gameObject.transform.position = tempCamLocation.position;
        //Write Transform function that brings table to the user, scaling it to its appropriate size.
        Vector3 scalar = new Vector3(0.5f, 0.5f, 0.5f);
        //gameObject.transform.localScale.Scale(scalar);
    }
}
