using UnityEngine;
using System.Collections;
using DatabaseUtilities;
using System.Collections.Generic;

public class TableHarness : MonoBehaviour {

    public float NodeDivisions;
    public float scaleSize;
    public float segments;

    int tableCount;

    Vector3[] tableSlots;



	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
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
    }

    private void SetPositionMatrix()
    {
        int matrixSize = Mathf.CeilToInt(Mathf.Sqrt(tableCount));
        tableSlots = new Vector3[tableCount];
        int counter = 0;

        for(int i = 0; i < matrixSize; i++)
        {
            for(int j = 0; j < matrixSize; j++)
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
}
