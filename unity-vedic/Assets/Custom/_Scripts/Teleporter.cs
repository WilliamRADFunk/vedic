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

        GameObject tempEnviorment = GameObject.FindGameObjectWithTag("Pedestal");
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

        for (int i = 0; i < arrayOfJumps.Length; i++)
        {
            jumpLocations[i] = arrayOfJumps[i].transform;
        }

        return jumpLocations;
    }

    private void jumpSwitch()
    {
        currentPosition.localPosition = newCoordinate(teleLocations[index].localPosition);
        index++;
        if (index > teleLocations.Length - 1)
        {
            index = 0;
        }
    }
}
