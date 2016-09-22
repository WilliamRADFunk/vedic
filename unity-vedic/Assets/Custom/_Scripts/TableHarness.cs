﻿using UnityEngine;
using System.Collections;
using DatabaseUtilities;
using System.Collections.Generic;

public class TableHarness : MonoBehaviour
{
    public float NodeDivisions;
    public float scaleSize;
    public float segments;
    public float scaleBaseDecrease;

    Vector3 initialLocalPos;
    Vector3 initialLocalScale;

    int tableCount;

    Vector3[] tableSlots;

    // Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
        //gameObject.transform.localPosition = Vector3.zero;
	}

    public void Initialize(GameObject[] tables)
    {
        int count = tables.Length;
        tableCount = count;
        SetPositionMatrix();  
        
        for(int i = 0; i < tableCount; i++)
        {
            tables[i].transform.localPosition = tableSlots[i];
        }

        GameObject ped = GameObject.FindGameObjectWithTag("Pedestal");
        gameObject.transform.parent = ped.transform;
        gameObject.transform.localPosition = Vector3.zero;
        initialLocalPos = Vector3.zero;
    }

    private void SetPositionMatrix()
    {
        int matrixSize = Mathf.CeilToInt(Mathf.Sqrt(tableCount));
        float newScale = 1 - (scaleBaseDecrease * (matrixSize - 2));
        initialLocalScale = new Vector3(newScale, newScale, newScale);
        gameObject.transform.localScale = initialLocalScale;
        
        tableSlots = new Vector3[tableCount];
        int counter = 0;

        if(matrixSize % 2 == 1)
        {
            int ceiling = Mathf.FloorToInt(matrixSize / 2);
            int floor = ceiling * -1;

            for (int i = floor; i <= ceiling; i++)
            {
                for(int j = floor; j <= ceiling; j++)
                {
                    float x = (j * segments) + (NodeDivisions * j);
                    float z = (i * segments) + (NodeDivisions * i);

                    if ((i + j) > tableSlots.Length || counter >= tableCount)
                    {
                        break;
                    }
                    tableSlots[counter] = new Vector3(x, 0, z);
                    counter++;
                }

                if (counter >= tableCount)
                {
                    break;
                }
            }
        }
        
        else
        {
            int ceiling = Mathf.FloorToInt(matrixSize / 2);
            int floor = ceiling * -1;

            for (int i = floor; i < ceiling; i++)
            {
                for (int j = floor; j < ceiling; j++)
                {
                    float x = (j * segments) + (NodeDivisions * j) + NodeDivisions * ceiling;
                    float z = (i * segments) + (NodeDivisions * i) + NodeDivisions * ceiling;

                    if ((i + j) > tableSlots.Length || counter >= tableCount)
                    {
                        break;
                    }
                    tableSlots[counter] = new Vector3(x, 0, z);
                    counter++;
                }

                if (counter >= tableCount)
                {
                    break;
                }
            }
        }

        gameObject.GetComponentInParent<RtsHandler>().AllocateTableHarness(gameObject);       
    }

    public void Deconstruct()
    {
        GameObject.Destroy(gameObject);
    }

    public void ResetToDefault()
    {
        gameObject.transform.localPosition = initialLocalPos;
        gameObject.transform.localScale = initialLocalScale;
    }

    public void BringToUser()
    {
        Transform tempCamLocation = GameObject.FindGameObjectWithTag("MainCamera").transform;
        Vector3 cameRelative = gameObject.transform.InverseTransformPoint(tempCamLocation.position);
        cameRelative.x += 1;

        gameObject.transform.localPosition = cameRelative;
        //Write Transform function that brings table to the user, scaling it to its appropriate size.
        Vector3 scalar = new Vector3(0.5f, 0.5f, 0.5f);
        gameObject.transform.localScale.Scale(scalar);
    }
}
