using UnityEngine;
using System.Collections;

public class TeleportLocation : MonoBehaviour
{

    string id;
    Vector3 localPos;
    Quaternion localRot;

    // Use this for initialization
    void Start () {
        localPos = gameObject.transform.localPosition;
        localRot = gameObject.transform.localRotation;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
