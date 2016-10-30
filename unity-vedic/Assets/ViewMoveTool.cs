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

    public void SetHolding(GameObject incommingObj)
    {
        viewRTS.enabled = false;

        if (holding)
        {
            viewRTS.enabled = false;
            holdingObj.transform.parent = returnObject.transform;
            holdingObj.GetComponent<Table>().ResetObjectDefault();   
        }

        holding = false;
        holdingObj = incommingObj;
        SetNewHolder(incommingObj);
        holding = true;
    }

    private void SetNewHolder(GameObject obj2Set)
    {
        viewRTS.enabled = true;
        obj2Set.transform.SetParent(this.transform);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
