using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SelectorOverseer : MonoBehaviour {

    public ViewMoveTool tableDeposit;
    GameObject tableSelected;

    List<GameObject> previouslySelected = new List<GameObject>();

    private Leap.Unity.PinchDetector lPinch;
    private Leap.Unity.PinchDetector rPinch;

    bool occupied;
    bool virgin;

    void Start()
    {
        tableSelected = null;
        occupied = false;
        virgin = true;
    }

    void Update()
    {
        if(virgin)
        {
            virgin = false;
            lPinch = GameObject.FindGameObjectWithTag("Pedestal").GetComponent<Leap.Unity.JamesV_LeapRTS>().PinchDetectorA;
            rPinch = GameObject.FindGameObjectWithTag("Pedestal").GetComponent<Leap.Unity.JamesV_LeapRTS>().PinchDetectorB;
        }

        if(tableSelected != null)
        {
            if(CheckPinch())
            {
                Release();
            }
        }
    }

    public void InputTable(GameObject inputtedTable)
    {
        tableSelected = inputtedTable;
    }

    public void removeTable(GameObject tempTable)
    {
        if(tableSelected != null && tempTable != null)
        {
            if (tempTable.GetInstanceID() == tableSelected.GetInstanceID())
            {
                tableSelected = null;
            }
        }
    }

    private void Release()
    {
        if(!occupied)
        {
            tableDeposit.SetHoldingEasy(tableSelected);

            occupied = true;
            previouslySelected.Add(tableSelected);
        }
        else
        {
            tableDeposit.SendToDumpingGrounds();
            tableDeposit.SetHoldingEasy(tableSelected);
            previouslySelected.Add(tableSelected);
        }
    }

    public void ForceDump(GameObject obj2Dump)
    {
        if (tableSelected != null && obj2Dump != null)
        {
            if (obj2Dump.GetInstanceID() == tableSelected.GetInstanceID())
            {
                tableDeposit.SendToDumpingGrounds();
                tableSelected = null;
            }
        }
    }

    private bool CheckPinch()
    {
        if(lPinch.DidStartPinch || rPinch.DidStartPinch)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void DeleteAllPrevious()
    {
        foreach(GameObject t in previouslySelected)
        {
            if(t != null)
            {
                Destroy(t);
            }
        }

        previouslySelected = new List<GameObject>();
    }
}
