using UnityEngine;
using System.Collections;

public class Teleporter : MonoBehaviour
{

    Vector3 mainEnviormentLocation;
    Transform currentPosition;

    GameObject[] arrayOfJumps;
    Transform[] teleLocations;

    int curStation;

    int index;
    bool virgin = true;

    // Use this for initialization
    void Start()
    {

        GameObject tempEnviorment = GameObject.FindGameObjectWithTag("enviorment");
        currentPosition = tempEnviorment.transform;
        mainEnviormentLocation = currentPosition.transform.localPosition;
        curStation = 0;
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
        GameObject[] tempArrayOfJumps = GameObject.FindGameObjectsWithTag("tele");
        Transform[] jumpLocations = new Transform[tempArrayOfJumps.Length];

        arrayOfJumps = new GameObject[tempArrayOfJumps.Length];

        for (int x = 0; x < arrayOfJumps.Length; x++)
        {
            for (int i = 0; i < arrayOfJumps.Length; i++)
            {
                if(tempArrayOfJumps[i].GetComponent<TeleportLocation>().stationInt == x)
                {
                    jumpLocations[x] = tempArrayOfJumps[i].transform;
                    arrayOfJumps[x] = tempArrayOfJumps[i];
                    break;
                }
                
            }
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
        curStation = station;


        Fader.Instance.FadeIn(null);
    }

    public void OnHoverTele(int station)
    {
        arrayOfJumps[station].GetComponent<TeleportLocation>().Reveal();
    }

    public void OffHoverTele(int station)
    {
        arrayOfJumps[station].GetComponent<TeleportLocation>().Unreveal();
    }

    public int getCurrentStation()
    {
        return curStation;
    }

}
