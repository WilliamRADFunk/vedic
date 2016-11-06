using UnityEngine;
using Leap.Unity;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using Leap;

public class ViewMoveTool : MonoBehaviour {

    [SerializeField]
    private JamesV_LeapRTS viewRTS;

    private PanelController panelInstance;

    GameObject currentHolding;
    Transform moveToolRts;

    public Transform dumpingGrounds;

    Vector3 initialLocalPos;
    Quaternion initialLocalRotation;
    Vector3 initialLocalScale;

    Vector3 initialLocalRtsPos;
    Quaternion initialLocalRtsRotation;
    Vector3 initialLocalRtsScale;

    bool virgin;
    bool secondFrame;
    bool isTableDeposit;
    bool rtsHarnessActive;
    
    [SerializeField]
    private HandModel leftHandMod;
    [SerializeField]
    private HandModel rightHandMod;

    private Hand lHand;
    private Hand rHand;

	// Use this for initialization
    void Awake()
    {
        if(viewRTS != null)
        {
            viewRTS.enabled = true;
        }
    }

    void Start()
    {
        rtsHarnessActive = false;
        isTableDeposit = false;
        virgin = true;
        secondFrame = false;
        if(leftHandMod != null)
        {
            lHand = leftHandMod.GetLeapHand();
        }
        if(rightHandMod != null)
        {
            rHand = rightHandMod.GetLeapHand();
        }

        if(dumpingGrounds != null)
        {
            isTableDeposit = true;
        }

        if(!isTableDeposit)
        {
            panelInstance = GameObject.FindGameObjectWithTag("UserInterface").GetComponent<PanelController>();
        }
    }

    void Update()
    {
        if(virgin)
        {
            virgin = false;
            SaveOrigin();
            secondFrame = true;
        }
        if(secondFrame)
        {
            secondFrame = false;
            viewRTS.enabled = false;
        }
        
        if(isTableDeposit)
        {
            if(currentHolding != null)
            {
                if(CheckForThumbsUp())
                {
                    SendToDumpingGrounds();
                }
            }
        }
        else
        {
            if (viewRTS.enabled == true)
            {
                if (currentHolding != null)
                {
                    if (CheckForThumbsUp())
                    {
                        panelInstance.ToggleRts();
                        panelInstance.ToggleOff("RtsToggle");
                    }
                }
            }
        }     
    }

    //Private Thumbs up detection
    private bool CheckForThumbsUp()
    {
        lHand = leftHandMod.GetLeapHand();
        rHand = rightHandMod.GetLeapHand();

        if(lHand != null)
        {
            if(lHand.GrabStrength == 1)
            {
                List<Leap.Finger> fingers = lHand.Fingers;
                Finger thumb = fingers[0];

                if(thumb != null)
                {
                    if(thumb.IsExtended)
                    {
                        return true;
                    }
                }
            }
        }
        if(rHand != null)
        {
            if (rHand.GrabStrength == 1)
            {
                List<Leap.Finger> fingers = rHand.Fingers;
                Finger thumb = fingers[0];
       
                if (thumb != null)
                {
                    if (thumb.IsExtended)
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }

    //Can be passed either the grid or table object to set rts anchor to said object
    public void SetHolding(GameObject incommingObj)
    {
        SetNewHolder(incommingObj);       
    }

    public void SetHoldingEasy(GameObject incommingObj)
    {
        SetNewHolderEasy(incommingObj);
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
        currentHolding = obj2Set;
    }

    private void SetNewHolderEasy(GameObject obj2Set)
    {
        viewRTS.enabled = true;
        obj2Set.transform.SetParent(this.transform);
        currentHolding = obj2Set;
    }

    //Allows for activation and deactivation of RTS
    public void RtsSetter(bool state)
    {
        viewRTS.enabled = state;
    }

    public void SendToDumpingGrounds()
    {
        if(currentHolding != null)
        {
            if(dumpingGrounds != null)
            {
                currentHolding.transform.SetParent(dumpingGrounds);
                currentHolding = null;

            }
        }
    }
}
