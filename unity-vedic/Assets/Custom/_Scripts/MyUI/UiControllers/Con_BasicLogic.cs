using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Con_BasicLogic : MonoBehaviour {
  

    public Vid_MySql_BasicLogic vidObj;
    public Text dataText;

    public void Start() {
        switch (vidObj.logicType) {
            case Vid_MySql_BasicLogic.BasicLogic.AND:
                if (dataText != null) {
                    dataText.text = "AND";
                }
                break;
            case Vid_MySql_BasicLogic.BasicLogic.OR:
                if (dataText != null) {
                    dataText.text = "OR";
                }
                break;
        }
    }

    public void Toggle() {

        switch (vidObj.logicType) {
            case Vid_MySql_BasicLogic.BasicLogic.AND:
                vidObj.logicType = Vid_MySql_BasicLogic.BasicLogic.OR;
                if (dataText != null) {
                    dataText.text = "OR";
                }
                break;
            case Vid_MySql_BasicLogic.BasicLogic.OR:
                vidObj.logicType = Vid_MySql_BasicLogic.BasicLogic.AND;
                if (dataText != null) {
                    dataText.text = "ADD";
                }
                break;
        }
    }
}
