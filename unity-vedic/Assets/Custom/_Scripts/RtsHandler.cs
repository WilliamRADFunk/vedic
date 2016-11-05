using UnityEngine;
using System.Collections;

public class RtsHandler : MonoBehaviour {

    public ViewMoveTool deposit;
    public SelectorOverseer tableManager;

    GameObject tableHarnessInstance;
    Transform rtsMain;

    Vector3 initialWorldPos;
    Vector3 initialLocalPos;
    Vector3 initialLocalRtsPos;
    Quaternion initialLocalRotation;
    Quaternion initialLocalRtsRotation;
    Vector3 initialLocalScale;
    Vector3 initialLocalRtsScale;

    Leap.Unity.JamesV_LeapRTS rtsInstance;
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
        rtsInstance = gameObject.GetComponent<Leap.Unity.JamesV_LeapRTS>();
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
                //rtsInstance.enabled = true;
                BringToUser();
                //SWAP :: ViewMoveTool.SetHolding(gameObject);
            }
            else
            {
                deposit.RtsSetter(true);
            }
            
            //SWAP :: ViewMoveTool.RtsSetter(true);
            //rtsInstance.enabled = true;
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
            //SWAP :: Call ViewMoveTool RtsSetter(false)
            deposit.RtsSetter(false);
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

            //SWAP :: Uncecessary reference to rtsInstance. Make sure to call Reset function on ViewMoveTool
            rtsInstance.enabled = false;

            ResetToDefault();
        }
        
    }

    private void ResetToDefault()
    {
        gameObject.transform.SetParent(rtsMain.transform);
        deposit.ResetAnchor();

        rtsMain.transform.localPosition = initialLocalRtsPos;
        rtsMain.transform.localRotation = initialLocalRtsRotation;
        rtsMain.transform.localScale = initialLocalRtsScale;

        gameObject.transform.localRotation = initialLocalRotation;
        gameObject.transform.localScale = initialLocalScale;
        gameObject.transform.position = initialWorldPos;
    }

    private void BringToUser()
    {
        Transform tempCamLocation = GameObject.FindGameObjectWithTag("MainCamera").transform;
        Vector3 camWorldVector = tempCamLocation.position;

        camWorldVector.x = camWorldVector.x - 2;
        camWorldVector.y--;

        initialWorldPos = gameObject.transform.position;
        initialLocalPos = gameObject.transform.localPosition;
        initialLocalRotation = gameObject.transform.localRotation;
        initialLocalScale = gameObject.transform.localScale;

        initialLocalRtsPos = rtsMain.transform.localPosition;
        initialLocalRtsRotation = rtsMain.transform.localRotation;
        initialLocalRtsScale = rtsMain.transform.localScale;

        deposit.SetHolding(gameObject);

        //gameObject.transform.position = camWorldVector;
        gameObject.transform.localScale -= new Vector3(0.7f, 0.7f, 0.7f);
    }

    public void SetInitializedBool(bool temp)
    {
        initialized = temp;
    }

    public bool GetInitializedBool()
    {
        return initialized;
    }

    public void KillHarness()
    {
        SetInitializedBool(false);
        tableManager.DeleteAllPrevious();
        GameObject.Destroy(tableHarnessInstance);
    }
}
