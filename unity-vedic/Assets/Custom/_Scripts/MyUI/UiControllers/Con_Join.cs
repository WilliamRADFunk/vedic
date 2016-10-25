using UnityEngine;
using UnityEngine.UI;
using DatabaseUtilities;
using System.Collections.Generic;

public class Con_Join : MonoBehaviour {

    public Vid_Join vidObj;
    public Text dataText;

    // Use this for initialization
    void Start() {
        if (vidObj != null &&
                dataText != null) {
            dataText.text = PrintJoin();
        }
    }

    public void ToogleRight_ConditionType() {
        Vid_Join.JoinType jointype = vidObj.jointype;
        switch (jointype) {
            case Vid_Join.JoinType.INNER:
                vidObj.jointype = Vid_Join.JoinType.LEFT;
                dataText.text = PrintJoin();
                break;
            case Vid_Join.JoinType.LEFT:
                vidObj.jointype = Vid_Join.JoinType.RIGHT;
                dataText.text = PrintJoin();
                break;
            case Vid_Join.JoinType.RIGHT:
                vidObj.jointype = Vid_Join.JoinType.OUTER;
                dataText.text = PrintJoin();
                break;
            case Vid_Join.JoinType.OUTER:
                vidObj.jointype = Vid_Join.JoinType.NATURAL;
                dataText.text = PrintJoin();
                break;
            case Vid_Join.JoinType.NATURAL:
                vidObj.jointype = Vid_Join.JoinType.INNER;
                dataText.text = PrintJoin();
                break;
        }
    }
    public void ToogleLeft_ConditionType() {
        Vid_Join.JoinType jointype = vidObj.jointype;
        switch (jointype) {
            case Vid_Join.JoinType.INNER:
                vidObj.jointype = Vid_Join.JoinType.NATURAL;
                dataText.text = PrintJoin();
                break;
            case Vid_Join.JoinType.LEFT:
                vidObj.jointype = Vid_Join.JoinType.INNER;
                dataText.text = PrintJoin();
                break;
            case Vid_Join.JoinType.RIGHT:
                vidObj.jointype = Vid_Join.JoinType.LEFT;
                dataText.text = PrintJoin();
                break;
            case Vid_Join.JoinType.OUTER:
                vidObj.jointype = Vid_Join.JoinType.RIGHT;
                dataText.text = PrintJoin();
                break;
            case Vid_Join.JoinType.NATURAL:
                vidObj.jointype = Vid_Join.JoinType.OUTER;
                dataText.text = PrintJoin();
                break;
        }
    }

    public string PrintJoin() {
        Vid_Join.JoinType jointype = vidObj.jointype;
        switch (jointype) {
            case Vid_Join.JoinType.INNER:
                return "INNER";
            case Vid_Join.JoinType.LEFT:
                return "LEFT";
            case Vid_Join.JoinType.RIGHT:
                return "RIGHT";
            case Vid_Join.JoinType.OUTER:
                return "OUTTER";
            case Vid_Join.JoinType.NATURAL:
                return "NATURAL";
        }
        return "INNER";
    }
}
