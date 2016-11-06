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
    bool isAnalytic;
    int analyticType;
   
    GameObject uiHead;

    // Use this for initialization
	void Start ()
    {
        virgin = true;
        isAnalytic = false;
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

    public void Initialize(GameObject[] tables, bool isAnalytics, int typeOfAnalytic)
    {
        int count = tables.Length;
        tableCount = count;
        isAnalytic = isAnalytics;
        analyticType = typeOfAnalytic;
        SetPositionMatrix();

        for (int i = 0; i < tableCount; i++)
        {
            tables[i].transform.localPosition = tableSlots[i];
        }


        string type = "";

        if (!isAnalytic)
        {
            Debug.Log("Something went wrong making the analytic object (boolean not set)");
        }
        else
        {
            if (analyticType == 0)
            {
                type = "ana1";
                return;
            }
        }
    }

    public void Initialize(GameObject[] tables, bool isTypePod)
    {
        int count = tables.Length;
        tableCount = count;
        SetPositionMatrix();  
        
        for(int i = 0; i < tableCount; i++)
        {
            tables[i].transform.localPosition = tableSlots[i];
        }


        string type = "";

        if(isTypePod)
        {
            type = "Pod";
            return;
        }
        else
        {
            type = "Pedestal";
        }

        GameObject ped = GameObject.FindGameObjectWithTag(type);
        gameObject.transform.parent = ped.transform;
        gameObject.transform.localPosition = Vector3.zero;
        initialLocalPos = Vector3.zero;

        if (gameObject.GetComponentInParent<RtsHandler>().GetInitializedBool())
        {
            gameObject.GetComponentInParent<RtsHandler>().KillHarness();
        }
        gameObject.GetComponentInParent<RtsHandler>().AllocateTableHarness(gameObject);
        
    }

    private void SetPositionMatrix()
    {
        if(!isAnalytic)
        {
            if (tableCount <= 1)
            {
                tableSlots = new Vector3[1];
                tableSlots[0] = new Vector3(0, 0, 0);
                float newScale = 1f;
                initialLocalScale = new Vector3(newScale, newScale, newScale);
                gameObject.transform.localScale = initialLocalScale;
            }
            else
            {
                int matrixSize = Mathf.CeilToInt(Mathf.Sqrt(tableCount));
                float newScale = 1 - (scaleBaseDecrease * (matrixSize - 2));
                initialLocalScale = new Vector3(newScale, newScale, newScale);
                gameObject.transform.localScale = initialLocalScale;

                tableSlots = new Vector3[tableCount];
                int counter = 0;

                if (matrixSize % 2 == 1)
                {
                    int ceiling = Mathf.FloorToInt(matrixSize / 2);
                    int floor = ceiling * -1;

                    for (int i = floor; i <= ceiling; i++)
                    {
                        for (int j = floor; j <= ceiling; j++)
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
        }
        else
        {
            if(analyticType == 0)
            {
                tableSlots = new Vector3[tableCount];
                Vector3 start = new Vector3(0, 0, 0);
                for(int i = 0; i < tableSlots.Length; i++)
                {
                    tableSlots[i] = start;
                    start += new Vector3 (1,0,0);
                }
            }
        }
        
    }

    public void Deconstruct()
    {
        //gameObject.GetComponentInParent<RtsHandler>().SetInitializedBool(false);
        gameObject.GetComponentInParent<RtsHandler>().KillHarness();
        //GameObject.Destroy(gameObject);
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
}
