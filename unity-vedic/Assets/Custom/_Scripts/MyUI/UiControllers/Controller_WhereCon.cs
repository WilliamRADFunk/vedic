using UnityEngine;
using UnityEngine.UI;
using System;

public class Controller_WhereCon : MonoBehaviour {
    public Vid_Where_Condition node;

    public void changeConditionType(Dropdown d) {
        switch (d.value) {
            case 0:
                node.conditionType = Condition_Type.LESS;
                break;
            case 1:
                node.conditionType = Condition_Type.LESS_EQU;
                break;
            case 2:
                node.conditionType = Condition_Type.GREATER;
                break;
            case 3:
                node.conditionType = Condition_Type.GREATER_EQU;
                break;
            case 4:
                node.conditionType = Condition_Type.EQU;
                break;
            case 5:
                node.conditionType = Condition_Type.NOT_EQU;
                break;
        }
    }
}
