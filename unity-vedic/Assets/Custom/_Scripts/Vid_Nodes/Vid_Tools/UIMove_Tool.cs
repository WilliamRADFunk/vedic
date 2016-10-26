using UnityEngine;
using UnityEngine.UI;
using Leap.Unity;
using UnityEngine.EventSystems;

public class UIMove_Tool : MonoBehaviour {
    GameObject holding;

    public JamesV_LeapRTS rts;
    public GameObject fileObj;
    public Vid_Object vidObj;

    void Awake() {
        if (rts != null) {
            rts.enabled = false;
        }
        else {
            rts = gameObject.AddComponent<JamesV_LeapRTS>();
            rts.speed = 5;
            rts.enabled = false;
        }
    }

    public GameObject GetHolding() {
        return holding;
    }
    public void setholding(GameObject obj2hold) {
        if (holding == null) {
            setNewHolder(obj2hold);
        }
        else {
            deActivateHolder();
            if (obj2hold != holding) {
                setNewHolder(obj2hold);
            }
            else {
                holding = null;
            }
        }
    }

    private void setNewHolder(GameObject obj2hold) {
        Vid_ObjContainer com;
        rts.enabled = true;
        rts.transform.position = obj2hold.transform.position;
        rts.transform.rotation = obj2hold.transform.rotation;
        holding = obj2hold;
        holding.transform.SetParent(this.transform);
        //com = holding.GetComponent<Vid_ObjContainer>();
        //if (com == null) { return; }

        ////Text t = com.getText();
        ////t.text = "Active";
        //Image i = com.selectButton_background;
        //if (i != null) {
        //    i.color = Color.green;
        //}
    }

    private void deActivateHolder() {
        Vid_ObjContainer com;
        rts.enabled = false;
        if (fileObj.Equals(holding)) {
            holding.transform.parent = null;
        }
        else {
            holding.transform.parent = fileObj.transform;
        }
        //com = holding.GetComponent<Vid_ObjContainer>();
        //if (com == null) {
        //    holding = null;
        //    return;
        //}
        ////Text t = com.getText();
        ////t.text = "Select";
        //Image i = com.selectButton_background;
        //if (i != null) {
        //    i.color = Color.white;
        //}
    }
}
