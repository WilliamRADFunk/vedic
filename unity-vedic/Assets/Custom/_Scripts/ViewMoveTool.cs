using UnityEngine;
using Leap.Unity;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class ViewMoveTool : MonoBehaviour {

    [SerializeField]
    private JamesV_LeapRTS viewRTS;

    Transform moveToolRts;

    Vector3 initialLocalPos;
    Quaternion initialLocalRotation;
    Vector3 initialLocalScale;

    Vector3 initialLocalRtsPos;
    Quaternion initialLocalRtsRotation;
    Vector3 initialLocalRtsScale;

    bool virgin;

	// Use this for initialization
    void Awake()
    {
        if(viewRTS != null)
        {
            viewRTS.enabled = true;
        }
        else
        {
            //viewRTS = gameObject.AddComponent<JamesV_LeapRTS>();
            //viewRTS.speed = 5;
            //viewRTS.enabled = false;
        }
    }

    void Start()
    {
        virgin = true;  
    }

    void Update()
    {
        if(virgin)
        {
            virgin = false;
            SaveOrigin();
            viewRTS.enabled = false;
        }
    }

    //Can be passed either the grid or table object to set rts anchor to said object
    public void SetHolding(GameObject incommingObj)
    {
        SetNewHolder(incommingObj);       
    }

    public void ResetAnchor()
    {
        viewRTS.enabled = false;
        ResetTransform();
    }

    private void ResetTransform()
    {
        moveToolRts.localPosition = initialLocalRtsPos;
        moveToolRts.localRotation = initialLocalRtsRotation;
        moveToolRts.localScale = initialLocalRtsScale;

        gameObject.transform.localPosition = initialLocalPos;
        gameObject.transform.localRotation = initialLocalRotation;
        gameObject.transform.localScale = initialLocalScale;
    }

    private void SaveOrigin()
    {
        moveToolRts = gameObject.transform.parent;

        initialLocalPos = gameObject.transform.localPosition;
        initialLocalRotation = gameObject.transform.localRotation;
        initialLocalScale = gameObject.transform.localScale;

        initialLocalRtsPos = moveToolRts.localPosition;
        initialLocalRtsRotation = moveToolRts.localRotation;
        initialLocalRtsScale = moveToolRts.localScale;
    }

    //Sets the parent of the object passed in to this object (RTS ANCHOR)
    private void SetNewHolder(GameObject obj2Set)
    {
        viewRTS.enabled = true;
        obj2Set.transform.SetParent(this.transform);
        obj2Set.transform.localPosition = Vector3.zero;
    }



    //Allows for activation and deactivation of RTS
    public void RtsSetter(bool state)
    {
        viewRTS.enabled = state;
    }

    //~~~~~~~~~~~~~~~~~Remenants of Table seperation inclusion inside ViewMoveTool~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    ////Makes sure that the holder is not currently being occupied, managing the state in which a table is currently being used.
    //private void DeactivateHolder()
    //{
    //    viewRTS.enabled = false;
    //    RemoveTableInstance();
    //}

    //Checks to see if a table is currently being held to reset if needed
    //private void RemoveTableInstance()
    //{
    //    if (holding)
    //    {
    //        holdingObj.transform.parent = returnObject.transform;
    //        holdingObj.GetComponent<Table>().ResetObjectDefault();
    //    }
    //}
}
