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

    public void SetHolding(GameObject incommingObj, bool isTable)
    {
        DeactivateHolder();
        holdingObj = incommingObj;
        SetNewHolder(incommingObj);
        holding = isTable;
    }

    private void SetNewHolder(GameObject obj2Set)
    {
        viewRTS.enabled = true;
        obj2Set.transform.SetParent(this.transform);
    }

    private void DeactivateHolder()
    {
        viewRTS.enabled = false;

        if (holding)
        {
            holdingObj.transform.parent = returnObject.transform;
            holdingObj.GetComponent<Table>().ResetObjectDefault();
        }
    }

    public void RtsSetter(bool state)
    {
        if(!state)
        {
            if (holding)
            {
                viewRTS.enabled = false;
                holdingObj.transform.parent = returnObject.transform;
                holdingObj.GetComponent<Table>().ResetObjectDefault();
            }

            //SWAP :: Call RtsHandler.ResetRig() to put grid back on table after returning its state back to normal
        }
        else
        {
            //SET this object as parent to the Grid, namely...
            // SetHolding(tableHarnessGrid)
        }
        viewRTS.enabled = state;
    }

	// Update is called once per frame
	void Update () {
	
	}
}
