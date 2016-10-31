using UnityEngine;
using System.Collections;

public class SelectorOverseer : MonoBehaviour {

    public ViewMoveTool tableDeposit;

    GameObject tableSelected;
    bool occupied;

    void Start()
    {
        tableSelected = null;
        occupied = false;
    }

    public void InputTable(GameObject inputtedTable)
    {
        tableSelected = inputtedTable;
    }

    public bool Release(GameObject temp)
    {
        if(temp.GetInstanceID() == tableSelected.GetInstanceID() && !occupied)
        {
            occupied = true;
            tableDeposit.SetHolding(temp);
            return true;
        }
        else
        {
            return false;
        }
    }
}
