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
        obj2hold.transform.SetParent(rts.gameObject.transform);
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


    /*UX Actions*/
    public void CopyNodes() {
        if(holdingV2.Count < 0) { return; }

        List<GameObject> copyGame = new List<GameObject>();
        for(int i = 0; i< holdingV2.Count; i++) {
            GameObject g = (GameObject)Instantiate(holdingV2[i], new Vector3(holdingV2[i].transform.position.x + .5f, holdingV2[i].transform.position.y + .5f, .3f), holdingV2[i].transform.rotation);
            copyGame.Add(g);
        }
        for(int i =0; i<copyGame.Count; i++) {
            setholding(copyGame[i]);
            VidContainer container = copyGame[i].GetComponent<VidContainer>();
            if (container != null) {
                container.DisableLines();
            }
        }
    }
    public void NewCopy() {
        GameObject g = (GameObject)Instantiate(rts.gameObject, new Vector3(rts.gameObject.transform.position.x,
                                                                           rts.gameObject.transform.position.y + .5f, 
                                                                           rts.gameObject.transform.position.z), 
                                               rts.gameObject.transform.rotation);
        List<GameObject> copyGameObj = new List<GameObject>();
        for (int i = 0; i< g.transform.childCount; i++) {
            copyGameObj.Add(g.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < copyGameObj.Count; i++) {
            setholding(copyGameObj[i]);
            VidContainer container = copyGameObj[i].GetComponent<VidContainer>();
            if (container != null) {
                container.Deselect();
                container.Select();
                container.ReConnectData();
            }
        }

        Destroy(g);
    }

    private void RemakeConnections() {

    }

    public void SelectGroup() {
        if ((holdingV2.Count - 1) > -1) {
            SelectGroup(holdingV2[holdingV2.Count - 1]);
        }
    }
    public void SelectGroup(GameObject go) {
        if (go == null) {
            return;
        }
        Vid_Object vidObj = go.GetComponent<Vid_Object>();
        Vid_ObjectInputs inputs = vidObj.GetInputs();
        VidContainer container = go.GetComponent<VidContainer>();
        container.Select();
        if (inputs == null) {
            return;
        }
        for (int i = 0; i < inputs.inputs.Length; i++) {
            Debug.Log(i + ":here");
            if (inputs.inputs[i] == null) {

            }
            else {
                SelectGroup(inputs.inputs[i].gameObject);
            }
        }
    }

    public void DeselectGroup() {
        if((holdingV2.Count -1) > -1) {
            DeselectGroup(holdingV2[holdingV2.Count - 1]);
        }
    }
    public void DeselectGroup(GameObject go) {
        if(go == null) {
            return;
        }
        Vid_Object vidObj = go.GetComponent<Vid_Object>();
        Vid_ObjectInputs inputs = vidObj.GetInputs();
        VidContainer container = go.GetComponent<VidContainer>();
        container.Deselect();
        if(inputs == null) {
            return;
        }
        for (int i = 0; i<inputs.inputs.Length; i++) {
            Debug.Log(i + ":here");
            if(inputs.inputs[i] == null ) {

            }
            else {
                DeselectGroup(inputs.inputs[i].gameObject);
            }
        }

    }

    public void DeselectAll() {
        GameObject[] items = holdingV2.ToArray();
        if(items == null) {
            return;
        }
        foreach(GameObject g in items) {
            VidContainer container = g.GetComponent<VidContainer>();
            container.Deselect();
        } 
    }
}
