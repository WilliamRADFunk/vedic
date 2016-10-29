using UnityEngine;
using UnityEngine.UI;
using System;

public class Controller_BasicLogic : MonoBehaviour {

    public Vid_MySql_BasicLogic node;

    public void changeLogicType(Dropdown d) {
        switch (d.value) {
            case 0:
                node.logicType = Vid_MySql_BasicLogic.BasicLogic.AND;
                break;
            case 1:
                node.logicType = Vid_MySql_BasicLogic.BasicLogic.OR;
                break;
        }
    }
}
