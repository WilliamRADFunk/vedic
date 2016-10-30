using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Table : MonoBehaviour, ViewObj
{
    public GameObject tempPrefabReference; //To be passed in by parent?
    List<GameObject> columns = new List<GameObject>();
    Vector3 location;

    Transform rtsMinor;
    Leap.Unity.LeapRTS rtsInstanceMinor;

    Vector3 initialWorldPos;
    Vector3 initialLocalPos;
    Vector3 initialLocalRtsPos;
    Quaternion initialLocalRotation;
    Quaternion initialLocalRtsRotation;
    Vector3 initialLocalScale;
    Vector3 initialLocalRtsScale;

    GameObject TactileText;
    TactileText t;

    BoxCollider areaOfEffect;

    string ID;
    int tableHeight;

    bool initialized = false;
    bool virgin = true;
    bool activated;
    bool triggered;
    int timer;

    // Use this for initialization
    void Start()
    {
        timer = -1;
        activated = false;
        triggered = false;
        areaOfEffect = gameObject.GetComponent<BoxCollider>();
        TactileText = GameObject.FindGameObjectWithTag("DynamicText");

        t = TactileText.GetComponent<TactileText>();


    }

    // Update is called once per frame
    void Update()
    {
        if (virgin)
        {
            InvokeRepeating("CountDown", 0.1f, 0.1f);
            virgin = false;
            areaOfEffect.size = new Vector3(1.5f, tableHeight * 2, 1.5f);

            rtsInstanceMinor = gameObject.AddComponent<Leap.Unity.LeapRTS>();
            //rtsInstanceMinor.PinchDetectorA = GameObject.FindGameObjectWithTag("LeftPinch").GetComponent<Leap.Unity.PinchDetector>();
            //rtsInstanceMinor.PinchDetectorB = GameObject.FindGameObjectWithTag("RightPinch").GetComponent<Leap.Unity.PinchDetector>();
            rtsMinor = gameObject.transform.parent;
            rtsInstanceMinor.enabled = false;
        }

        if(timer == 0)
        {
            for (int i = 0; i < columns.Count; i++)
            {
                columns[i].GetComponent<Column>().columnTriggered(false);
            }
            triggered = false;
            t.UpdateText(ID, false); 
        }
    }

    public bool initialization(string name, GameObject[] columnObjects, Transform father)
    {
        if (!initialized)
        {
            initialize(columnObjects, father);
            initialized = true;
            ID = name;
            return true;
        }
        else
        {
            return false;
        }
    }

    void initialize(GameObject[] columnObjects, Transform father)
    {
        tableHeight = columnObjects.Length;

        if (columnObjects == null || columnObjects.Length <= 0)
        {
            /*Do not Construct, possibly update error */
        }
        else
        {
            for (int i = 0; i < columnObjects.Length; i++)
            {
                columns.Add(columnObjects[i]);
            }
        }
        ParentObject(father);
        ResetObjectDefault();
    }


    //Interface functions for Viewable Objects :: Table
    public void ResetObjectDefault()
    {
        gameObject.transform.localPosition = location;
    }

    public void ParentObject(Transform parent)
    {
        gameObject.transform.parent = parent;
    }

    private void BringToActive()
    {
        initialWorldPos = gameObject.transform.position;
        initialLocalPos = gameObject.transform.localPosition;
        initialLocalRotation = gameObject.transform.localRotation;
        initialLocalScale = gameObject.transform.localScale;

        initialLocalRtsPos = rtsMinor.transform.localPosition;
        initialLocalRtsRotation = rtsMinor.transform.localRotation;
        initialLocalRtsScale = rtsMinor.transform.localScale;
    }

    private void ResetToDefault()
    {
        rtsMinor.transform.localPosition = initialLocalRtsPos;
        rtsMinor.transform.localRotation = initialLocalRtsRotation;
        rtsMinor.transform.localScale = initialLocalRtsScale;

        gameObject.transform.localRotation = initialLocalRotation;
        gameObject.transform.localScale = initialLocalScale;
        gameObject.transform.position = initialWorldPos;
    }

    private void InteractOn()
    {
        //Possibly Signal RtsHandler to turn off its respective rts
        rtsInstanceMinor.enabled = true;
    }

    private void InteractOff()
    {
        ResetToDefault();
        rtsInstanceMinor.enabled = false;

    }

    /*
    public void OnTriggerEnter(Collider other)
    {
        if (!activated)
        {
            for (int i = 0; i < columns.Count; i++)
            {
                columns[i].GetComponent<Column>().columnTriggered(true);
            }
        }
        activated = true;
    }

    public void OnTriggerExit(Collider other)
    {
        if(activated)
        {
            for (int i = 0; i < columns.Count; i++)
            {
                columns[i].GetComponent<Column>().columnTriggered(false);
            }
        }
        activated = false;
    }
    */

    public void OnTriggerStay(Collider other)
    {
        timer = 5;

        if(!triggered)
        {
            triggered = true;
            for (int i = 0; i < columns.Count; i++)
            {
                columns[i].GetComponent<Column>().columnTriggered(true);
            }
            t.UpdateText(ID, true);
        }
    }

    private void CountDown()
    {
        timer--;
    }

    public void AltActivation()
    {
        timer = 5;

        if(!triggered)
        {
            triggered = true;
            for (int i = 0; i < columns.Count; i++)
            {
                columns[i].GetComponent<Column>().columnTriggered(true);
            }
            t.UpdateText(ID, true);
        }
    }

}
