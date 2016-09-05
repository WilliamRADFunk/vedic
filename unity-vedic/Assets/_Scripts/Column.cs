using UnityEngine;
using System.Collections;
using System;

public class Column : MonoBehaviour, ViewObj {

    
    Material objMesh;
    int colHeight;


	// Use this for initialization
	void Start () {

        objMesh = gameObject.GetComponent<Renderer>().material;

	}
	
	// Update is called once per frame
	void Update () {
	    
	}


    public void Initialize(int key, Transform father)
    {
        this.colHeight = key;

        ParentObject(father);
        ResetObjectDefault();
    }

    public void ResetObjectDefault()
    {
        gameObject.transform.localPosition = new Vector3(0, colHeight, 0);
    }

    public void ParentObject(Transform parentTransform)
    {
        gameObject.transform.parent = parentTransform;
    }
}
