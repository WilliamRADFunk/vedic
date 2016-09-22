using UnityEngine;
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

    bool virgin;

    GameObject uiHead;

    // Use this for initialization
	void Start ()
    {
        virgin = true;
        uiHead = GameObject.FindGameObjectWithTag("UserInterface");
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(virgin)
        {
            virgin = false;
            SendGameObject();
        }
	}

    public void Initialize(GameObject[] tables)
    {
        if(gameObject.GetComponentInParent<RtsHandler>().GetInitializedBool())
        {
            gameObject.GetComponentInParent<RtsHandler>().killHarness();
        }

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

        gameObject.GetComponentInParent<RtsHandler>().AllocateTableHarness(gameObject);
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
    }

    public void Deconstruct()
    {
        gameObject.GetComponentInParent<RtsHandler>().SetInitializedBool(false);
        GameObject.Destroy(gameObject);
    }

    private void ResetToDefault()
    {
        gameObject.transform.localPosition = initialLocalPos;
        gameObject.transform.localScale = initialLocalScale;
    }

    private void BringToUser()
    {
        Transform tempCamLocation = GameObject.FindGameObjectWithTag("MainCamera").transform;
        Vector3 cameRelative = gameObject.transform.InverseTransformPoint(tempCamLocation.position);
        cameRelative.x += 1;

        gameObject.transform.localPosition = cameRelative;
        //Write Transform function that brings table to the user, scaling it to its appropriate size.
        Vector3 scalar = new Vector3(0.5f, 0.5f, 0.5f);
        gameObject.transform.localScale.Scale(scalar);
    }

    private void SendGameObject()
    {
        uiHead.GetComponent<PanelController>().SendTableHarnessManager(gameObject);
    }
}
