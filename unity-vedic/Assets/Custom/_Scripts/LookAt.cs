using UnityEngine;
using System.Collections;

public class LookAt : MonoBehaviour {

    public Transform objectViewAnchor;
    bool anchored;
    

	// Use this for initialization
	void Start () {

        anchored = false;

	    if(objectViewAnchor == null)
        {
            Debug.Log("Object View Anchor is not instantiated...");
        }
        else
        {
            anchored = true;
        }
	}
	
	// Update is called once per frame
	void Update () {

        if (anchored)
        {
            gameObject.transform.LookAt(objectViewAnchor);
        }
                
	}

    public void UpdateAnchorPoint(Transform temp, bool state)
    {
        UpdateLookTransform(temp);
        AnchorState(state);
    }

    public void SetAnchorState(bool state)
    {
        AnchorState(state);
    }

    private void UpdateAnchorPoint(Transform temp)
    {
        UpdateLookTransform(temp);
    }

    private void AnchorState(bool activation)
    {
        anchored = activation;
    }

    private void UpdateLookTransform(Transform temp)
    {
        if (temp != null)
        {
            objectViewAnchor = temp;
        }
    }
}
