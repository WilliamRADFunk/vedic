using UnityEngine;
using System.Collections;

public class RtsHandler : MonoBehaviour {

    GameObject tableHarnessInstance;
    Transform rtsMain;

    Vector3 initialWorldPos;
    Vector3 initialLocalPos;
    Vector3 initialLocalRtsPos;
    Quaternion initialLocalRotation;
    Quaternion initialLocalRtsRotation;
    Vector3 initialLocalScale;
    Vector3 initialLocalRtsScale;

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
            rtsInstance.enabled = false;
        }
	}

    public void AllocateTableHarness(GameObject harnessInstance)
    {
        tableHarnessInstance = harnessInstance;
        initialized = true;
    }

    public bool InteractOn()
    {

        if(initialized)
        {
            if(firstTime)
            {
                firstTime = false;
                BringToUser();
            }
            
            rtsInstance.enabled = true;
            return true;
        }
        else
        {
            Debug.Log("InteractOn cannot be reliably called because tableharness is initialized to its observed object.");
            return false;
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
        if(initialized && !firstTime)
        {
            firstTime = true;
            rtsInstance.enabled = false;
            ResetToDefault();
        }
        
    }

    private void ResetToDefault()
    {
        rtsMain.transform.localPosition = initialLocalRtsPos;
        rtsMain.transform.localRotation = initialLocalRtsRotation;
        rtsMain.transform.localScale = initialLocalRtsScale;

        //gameObject.transform.localPosition = initialLocalPos;
        gameObject.transform.localRotation = initialLocalRotation;
        gameObject.transform.localScale = initialLocalScale;
        gameObject.transform.position = initialWorldPos;
    }

    private void BringToUser()
    {
        Transform tempCamLocation = GameObject.FindGameObjectWithTag("MainCamera").transform;
        Vector3 camWorldVector = tempCamLocation.position;

        camWorldVector.x = camWorldVector.x + 1;
        camWorldVector.y--;

        initialWorldPos = gameObject.transform.position;
        initialLocalPos = gameObject.transform.localPosition;
        initialLocalRotation = gameObject.transform.localRotation;
        initialLocalScale = gameObject.transform.localScale;

        initialLocalRtsPos = rtsMain.transform.localPosition;
        initialLocalRtsRotation = rtsMain.transform.localRotation;
        initialLocalRtsScale = rtsMain.transform.localScale;

        gameObject.transform.position = camWorldVector;
        gameObject.transform.localScale -= new Vector3(0.8f, 0.8f, 0.8f);
    }
}
