using UnityEngine;
using UnityEngine.UI;
using System;

public class Controller_BasicLogic : MonoBehaviour {

    public Vid_MySql_BasicLogic node;

    public void changeLogicType(Dropdown d) {
        switch (d.value) {
            case 0:
                node.logicType = BasicLogic.AND;
                break;
            case 1:
                node.logicType = BasicLogic.OR;
                break;
        }
    }
}
