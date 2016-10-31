using UnityEngine;
using System.Collections;

public class SelectorOverseer : MonoBehaviour {

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
        if(!occupied)
        {
            occupied = true;
            return true;
        }
        else
        {
            return false;
        }
    }
}
