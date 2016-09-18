using UnityEngine;
using System.Collections;

public class Teleporter : MonoBehaviour
{

    Vector3 mainEnviormentLocation;
    Transform currentPosition;

    Transform[] teleLocations;

    int index;
    bool virgin = true;

    // Use this for initialization
    void Start()
    {

        GameObject tempEnviorment = GameObject.FindGameObjectWithTag("enviorment");
        currentPosition = tempEnviorment.transform;
        mainEnviormentLocation = currentPosition.transform.localPosition;
        index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (virgin)
        {
            virgin = false;
            teleLocations = initializeJumpLocations();
        }

        if (Input.GetKeyDown("space"))
        {
            jumpSwitch(1);
        }
    }

    private Vector3 newCoordinate(Vector3 telePosition)
    {
        Vector3 endLocation = mainEnviormentLocation;
        float x = endLocation.x - telePosition.x;
        float z = endLocation.z - telePosition.z;
        Vector3 finalEnd = new Vector3(x, endLocation.y, z);
        return finalEnd;
    }

    private void resetPosition()
    {
        currentPosition.transform.localPosition = mainEnviormentLocation;
    }

    private Transform[] initializeJumpLocations()
    {
        GameObject[] arrayOfJumps = GameObject.FindGameObjectsWithTag("tele");
        Transform[] jumpLocations = new Transform[arrayOfJumps.Length];

        for (int i = 0; i < arrayOfJumps.Length; i++)
        {
            jumpLocations[i] = arrayOfJumps[i].transform;
        }

        return jumpLocations;
    }

    public void jumpSwitch(int station)
    {
        if(station > teleLocations.Length)
        {
            Debug.Log("Incorrect teleportation integer inputted");
            return;
        }

        Fader.Instance.FadeOut(null);

        currentPosition.localPosition = newCoordinate(teleLocations[station].localPosition);

        Fader.Instance.FadeIn(null);
    }
}
