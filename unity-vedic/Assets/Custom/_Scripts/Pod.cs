using UnityEngine;
using System.Collections;

public class Pod : MonoBehaviour {

    GameObject podHarness;
    string dbName;

    bool initialized;
    bool state;

    // Use this for initialization
    void Start() {
        state = false;
        initialized = false;
    }

    // Update is called once per frame
    void Update() {

    }

    public void KillHarness()
    {
        GameObject.Destroy(podHarness);
        initialized = false;
        podHarness = null;
    }

    public string GetDbName()
    {
        return dbName;
    }

    public void AllocateTableHarness(GameObject temp)
    {
        podHarness = temp;
        initialized = true;
    }

    public bool GetInitializedBool()
    {
        return initialized;
    }

    public bool GetState()
    {
        return state;
    }

    public void SetState(bool temp)
    {
        state = temp;
    }

}
