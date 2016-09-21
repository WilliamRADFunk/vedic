using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Table : MonoBehaviour, ViewObj
{
    public GameObject tempPrefabReference; //To be passed in by parent?
    List<GameObject> columns = new List<GameObject>();
    Vector3 location;

    BoxCollider areaOfEffect;

    string ID;
    int tableHeight;

    bool initialized = false;
    bool virgin = true;
    bool activated;

    // Use this for initialization
    void Start()
    {
        activated = false;
        areaOfEffect = gameObject.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (virgin)
        {
            virgin = false;
            areaOfEffect.size = new Vector3(1.5f, tableHeight * 2, 1.5f);
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

    public void OnTriggerEnter(Collider other)
    {
        if (!activated)
        {
            for (int i = 0; i < columns.Count; i++)
            {
                columns[i].GetComponent<Column>().columnTriggered();
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
                columns[i].GetComponent<Column>().columnTriggered();
            }
        }
        activated = false;
    }

}
