using UnityEngine;
using System.Collections;
using DatabaseUtilities;
using System.Collections.Generic;

public class DiskHarness : MonoBehaviour
{
    Vector3 initialLocalPos;
    Vector3 initialLocalScale;

    List<GameObject> housedDisks = new List<GameObject>();

    int diskCount;
    int timer;

    Vector3[] diskSlots;

    bool virgin;
    bool triggered;

    GameObject uiHead;

    // Use this for initialization
    void Start()
    {
        timer = -1;
        virgin = true;
        triggered = false;
        uiHead = GameObject.FindGameObjectWithTag("UserInterface");

        InvokeRepeating("CountDown", 0.1f, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (virgin)
        {
            virgin = false;
            //SendGameObject();
        }
    }

    public void Initialize(GameObject[] disks, List<DatabaseUtilities.Column> diskInfo)
    {
        int count = disks.Length;
        diskCount = count;
        SetPositionMatrix(diskInfo);

        for (int i = 0; i < diskCount; i++)
        {
            disks[i].transform.localPosition = diskSlots[i];
            disks[i].GetComponent<Disk>().SaveOrigin();
            housedDisks.Add(disks[i]);
        }
    }

    private void SetPositionMatrix(List<DatabaseUtilities.Column> diskInfo)
    {
        int amountOfDataTypes = diskInfo.Count;
        diskSlots = new Vector3[amountOfDataTypes];

        Vector3 startingPosition = new Vector3(0, 0, 0);
        float previousScale = -1f;
        for(int i = 0; i < amountOfDataTypes; i++)
        {
            DatabaseUtilities.Column currentInfo = diskInfo[i];

            //Convert string in 1st field parameter into float
            float scaleSize = float.Parse(currentInfo.fields[0]);

            if (i == 0)
            {
                diskSlots[i] = startingPosition;
                previousScale = scaleSize;
            }
            else
            {
                float newYUpdate = previousScale + scaleSize;
                startingPosition += new Vector3(0, newYUpdate, 0);
                Debug.Log("diskPosition " + startingPosition);
                previousScale = scaleSize;
                diskSlots[i] = startingPosition;
            }
        }
    }

    public void Deconstruct()
    {

    }

    private void ResetToDefault()
    {
        gameObject.transform.localPosition = initialLocalPos;
        gameObject.transform.localScale = initialLocalScale;
    }

    private void SendGameObject()
    {
        uiHead.GetComponent<PanelController>().SendTableHarnessManager(gameObject);
    }

    public void OnTriggerStay(Collider other)
    {
        timer = 5;

        if (!triggered)
        {
            triggered = true;
            for (int i = 0; i < housedDisks.Count; i++)
            {
                housedDisks[i].GetComponent<Disk>().columnTriggered(true);
            }
        }
    }

    private void CountDown()
    {
        timer--;
    }

    public void AltActivation()
    {
        timer = 5;

        if (!triggered)
        {
            triggered = true;
            for (int i = 0; i < housedDisks.Count; i++)
            {
                housedDisks[i].GetComponent<Disk>().columnTriggered(true);
            }
        }
    }
}
