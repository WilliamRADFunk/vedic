using UnityEngine;
using Leap.Unity;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class ViewMoveTool : MonoBehaviour {

    public GameObject returnObject;
    JamesV_LeapRTS viewRTS;

    GameObject holdingObj;

    bool holding;

	// Use this for initialization
    void Awake()
    {
        if(viewRTS != null)
        {
            viewRTS.enabled = false;
        }
        else
        {
            viewRTS = gameObject.AddComponent<JamesV_LeapRTS>();
            viewRTS.speed = 5;
            viewRTS.enabled = false;
        }

        holding = false;
    }

	void Start () {
	}


    //Can be passed either the grid or table object to set rts anchor to said object
    public void SetHolding(GameObject incommingObj, bool isTable)
    {
        DeactivateHolder();
        holdingObj = incommingObj;
        SetNewHolder(incommingObj);
        holding = isTable;
    }

    //Sets the parent of the object passed in to this object (RTS ANCHOR)
    private void SetNewHolder(GameObject obj2Set)
    {
        viewRTS.enabled = true;
        obj2Set.transform.SetParent(this.transform);
    }

    //Makes sure that the holder is not currently being occupied, managing the state in which a table is currently being used.
    private void DeactivateHolder()
    {
        viewRTS.enabled = false;
        RemoveTableInstance();
       
    }

    //Checks to see if a table is currently being held to reset if needed
    private void RemoveTableInstance()
    {
        if (holding)
        {
            holdingObj.transform.parent = returnObject.transform;
            holdingObj.GetComponent<Table>().ResetObjectDefault();
        }
    }

    //Allows for activation and deactivation of RTS
    public void RtsSetter(bool state)
    {
        viewRTS.enabled = state;
    }

    //Reset function to ensure that everything returns back to normal
    public void Reset()
    {
        SetHolding(returnObject, false);
        viewRTS.enabled = false;
    }

	// Update is called once per frame
	void Update () {
	
	}
}
