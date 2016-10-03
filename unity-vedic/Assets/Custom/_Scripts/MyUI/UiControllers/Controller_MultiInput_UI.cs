using UnityEngine;
using UnityEngine.UI;

public class Controller_MultiInput_UI : MonoBehaviour {

    public Vid_MultiInput node;

    public void changeDataType(Dropdown d) {
        switch (d.value) {
            case 0:
                node.output_dataType = VidData_Type.STRING;
                break;
            case 1:
                node.output_dataType = VidData_Type.NUM;
                break;
            case 2:
                node.output_dataType = VidData_Type.DATABASE_COL;
                break;
            case 3:
                node.output_dataType = VidData_Type.BOOL;
                break;
            case 4:
                node.output_dataType = VidData_Type.LIST;
                break;

        }
    }
}
