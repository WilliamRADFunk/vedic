using UnityEngine;
using System.Collections;

public class TeleportLocation : MonoBehaviour
{

    public string id;
    Vector3 localPos;
    Quaternion localRot;

    // Use this for initialization
    void Start () {

        localPos = gameObject.transform.localPosition;
        localRot = gameObject.transform.localRotation;
        gameObject.tag = "tele";
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public Vector3 retrieveLocalPos()
    {
        return localPos;
    }

    public Quaternion retrieveLocalRot()
    {
        return localRot;
    }

    public string retrieveStastion()
    {
        return id;
    }
}
