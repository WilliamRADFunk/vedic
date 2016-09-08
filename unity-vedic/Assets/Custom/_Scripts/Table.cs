using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Table : MonoBehaviour, ViewObj {

    public GameObject tempPrefabReference; //To be passed in by parent?

    Vector3 location;
    List<GameObject> columns = new List<GameObject>();

    bool initialized = false;

    // Use this for initialization
	void Start () {
     
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public bool initialization(GameObject[] columnObjects, Transform father)
    {
        if(!initialized)
        {
            initialize(columnObjects, father);
            initialized = true;
            return true;
        }
        else
        {
            return false;
        }
    }

    void initialize(GameObject[] columnObjects, Transform father)
    {
        
        if(columnObjects == null || columnObjects.Length <= 0)
        {
            /*Do not Construct, possibly update error */
        }
        else
        {
            Debug.Log(columnObjects.Length);

            for(int i = 0; i < columnObjects.Length;i++)
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
}
