using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Table : MonoBehaviour, ViewObj
{
    public GameObject tempPrefabReference; //To be passed in by parent?
    List<GameObject> columns = new List<GameObject>();
    Vector3 location;

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

}
