using UnityEngine;
using System.Collections;

public class TeleportLocation : MonoBehaviour
{
    public GameObject beam;
    public int stationInt;
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

    public Vector3 retrieveLocalPos()
    {
        return localPos;
    }

    public Quaternion retrieveLocalRot()
    {
        return localRot;
    }

    public void Reveal()
    {
        beam.SetActive(true);
    }

    public void Unreveal()
    {
        beam.SetActive(false);
    }
}
