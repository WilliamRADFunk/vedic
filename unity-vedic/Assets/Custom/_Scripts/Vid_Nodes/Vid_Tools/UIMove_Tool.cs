using UnityEngine;
using Leap.Unity;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class UIMove_Tool : MonoBehaviour {
    List<GameObject> holdingV2;

    public JamesV_LeapRTS rts;
    public GameObject parentObj;

    void Awake() {
        holdingV2 = new List<GameObject>();
        if (rts != null) {
            rts.enabled = false;
        }
        else {
            rts = gameObject.AddComponent<JamesV_LeapRTS>();
            rts.speed = 5;
            rts.enabled = false;
        }
    }

    public GameObject GetRoot() {
        if(holdingV2 != null) {
            if(holdingV2.Count > 0) {
                return holdingV2[0];
            }
        }
        return null;
    }
    public void setholding(GameObject obj2hold) {
        if (holdingV2.Contains(obj2hold)) {
            holdingV2.Remove(obj2hold);
            deActivateHolder(obj2hold);
        }
        else {
            holdingV2.Add(obj2hold);
            setNewHolder(obj2hold);
        }
    }

    private void setNewHolder(GameObject obj2hold) {
        rts.enabled = true;
        obj2hold.transform.SetParent(this.transform);
    }

    private void deActivateHolder(GameObject obj2hold) {
        if (holdingV2.Count == 0) {
            rts.enabled = false;
            obj2hold.transform.parent = parentObj.transform;
        }
        else {
            obj2hold.transform.parent = parentObj.transform;
        }
    }

    public void CopyNodes() {
        foreach(GameObject go in holdingV2) {
            GameObject g = (GameObject)Instantiate(go, go.transform.position + new Vector3(.5f,.5f,0), go.transform.rotation);
        }

    }
}
