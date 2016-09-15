using UnityEngine;
using System.Collections;

public class Teleporter : MonoBehaviour {

    Vector3 mainEnviormentLocation;
    Transform currentPosition;

    Transform[] teleLocations;

    bool virgin = true;
    bool teleported;

	// Use this for initialization
	void Start () {

        GameObject tempEnviorment = GameObject.FindGameObjectWithTag("Pedestal");
        currentPosition = tempEnviorment.transform;
        mainEnviormentLocation = currentPosition.transform.localPosition;
        teleported = false;
	}
	
	// Update is called once per frame
	void Update () {
	    if(virgin)
        {
            virgin = false;
            teleLocations = initializeJumpLocations();

            InvokeRepeating("jumpSwitch", 10, 10);
        }
	}

    private Vector3 newCoordinate(Vector3 telePosition)
    {
        Vector3 endLocation = mainEnviormentLocation;
        endLocation = endLocation - telePosition;
        return endLocation;
    }

    private void resetPosition()
    {
        currentPosition.transform.localPosition = mainEnviormentLocation;
    }

    private Transform[] initializeJumpLocations()
    {
        GameObject[] arrayOfJumps = GameObject.FindGameObjectsWithTag("tele");
        Transform[] jumpLocations = new Transform[arrayOfJumps.Length];

        foreach (GameObject obj in arrayOfJumps)
        {
            if(obj.GetComponent<TeleportLocation>().retrieveStastion() == "default")
            {
                jumpLocations[0] = obj.transform;
            } 
            else
            {
                jumpLocations[1] = obj.transform;
            }
        }

        return jumpLocations;
    }

    private void jumpSwitch()
    {
        if(!teleported)
        {
            currentPosition.localPosition = newCoordinate(teleLocations[1].localPosition);
            teleported = true;
        }
        else
        {
            teleported = false;
            resetPosition();
        }
    }
   
}
