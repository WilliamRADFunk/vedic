using UnityEngine;
using System.Collections;

public class Pod : MonoBehaviour {

    public Transform PodTransform;
    Vector3 initialPodTransform;
    Vector3 endPodTransform;

    GameObject podHarness;
    string dbName;

    bool initialized;
    bool state;

    // Use this for initialization
    void Start() {
        state = false;
        initialized = false;
        initialPodTransform = PodTransform.localPosition;
        Vector3 increaseOne = new Vector3(0, 2, 0);
        endPodTransform = initialPodTransform + increaseOne;
    }

    // Update is called once per frame
    void Update() {
        if(state)
        {
            PodTransform.localPosition = endPodTransform;
        }
        else
        {
            PodTransform.localPosition = initialPodTransform;
        }
    }

    public void KillHarness()
    {
        if(initialized)
        {
            GameObject.Destroy(podHarness);
            initialized = false;
            podHarness = null;
        }
        
    }

    public string GetDbName()
    {
        return dbName;
    }

    public void AllocateTableHarness(GameObject temp)
    {
        if (initialized)
        {
            KillHarness();
        }

        temp.transform.parent = gameObject.transform;
        temp.transform.localPosition = Vector3.zero;

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
