using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class Table : MonoBehaviour, ViewObj
{
    public GameObject tempPrefabReference; //To be passed in by parent?
    List<GameObject> columns = new List<GameObject>();
    Vector3 location;

    SelectorOverseer selectTool;

    Transform rtsMinor;
    Leap.Unity.JamesV_LeapRTS rtsInstanceMinor;

    Vector3 initialWorldPos;
    Vector3 initialLocalPos;
    Vector3 initialLocalRtsPos;
    Quaternion initialLocalRotation;
    Quaternion initialLocalRtsRotation;
    Vector3 initialLocalScale;
    Vector3 initialLocalRtsScale;


    //External Elements
    GameObject TactileText;
    WindowTextController t;
    DataCache dCache; 

    BoxCollider areaOfEffect;

    string tblName;
    string ID;
    string outPut = "";
    int tableHeight;

    bool initialized = false;
    bool virgin = true;
    bool secondFrame;
    bool activated;
    bool triggered;
    bool newForm;

    int timer;

    // Use this for initialization
    void Start()
    {
        timer = -1;
        activated = false;
        triggered = false;
        secondFrame = false;
        newForm = false;
        areaOfEffect = gameObject.GetComponent<BoxCollider>();

        //Retrieving External Objects...
        TactileText = GameObject.FindGameObjectWithTag("DynamicText");
        t = TactileText.GetComponent<WindowTextController>();
        selectTool = GameObject.FindGameObjectWithTag("DynamicSelect").GetComponent<SelectorOverseer>();
        dCache = GameObject.FindGameObjectWithTag("DataCache").GetComponent<DataCache>();
    }

    // Update is called once per frame
    void Update()
    {
        if (secondFrame)
        {
            secondFrame = false;
            rtsMinor = gameObject.transform.parent;
            rtsInstanceMinor.enabled = false;
        }  
        if (virgin)
        {
            InvokeRepeating("CountDown", 0.1f, 0.1f);
            virgin = false;
            areaOfEffect.size = new Vector3(1.5f, tableHeight * 2, 1.5f);

            rtsInstanceMinor = gameObject.AddComponent<Leap.Unity.JamesV_LeapRTS>();
            rtsInstanceMinor.enabled = true; ;

            secondFrame = true;

            RetrieveColumnNames();
        }

        if(timer == 0)
        {
            for (int i = 0; i < columns.Count; i++)
            {
                columns[i].GetComponent<Column>().columnTriggered(false);
            }
            triggered = false;
            selectTool.removeTable(gameObject);
            t.UpdateInfo(outPut, false); 
        }
    }

    public bool initialization(string tName, string id, GameObject[] columnObjects, Transform father)
    {
        if (!initialized)
        {
            initialize(columnObjects, father);
            initialized = true;
            tblName = tName;
            ID = id;
            outPut = tName + ":\n";

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
        BringToActive();
        timer = 10;
        newForm = true;
    }

    public void InteractOff()
    {
        ResetToDefault();
        gameObject.transform.SetParent(rtsMinor);
        ResetToDefault();
    }

    public void OnTriggerStay(Collider other)
    {
        timer = 5;
        if(newForm)
        {
            timer = 120;
        }

        if(!triggered)
        {
            selectTool.InputTable(gameObject);
            triggered = true;
            for (int i = 0; i < columns.Count; i++)
            {
                columns[i].GetComponent<Column>().columnTriggered(true);
            }
            t.UpdateInfo(outPut, true);
            dCache.PingCache(ID);

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
            selectTool.InputTable(gameObject);
            triggered = true;
            for (int i = 0; i < columns.Count; i++)
            {
                columns[i].GetComponent<Column>().columnTriggered(true);
            }
            t.UpdateInfo(outPut, true);
            dCache.PingCache(ID);
        }
    }

    public void SetGuiState(bool state)
    {
        newForm = state;
    }

    public bool GetGuiState()
    {
        return newForm;
    }

    public void ForceOut()
    {
        selectTool.ForceDump(gameObject);
        SetGuiState(false);
    }

    private void RetrieveColumnNames()
    {
        foreach(GameObject col in columns)
        {
            string tempCol = col.GetComponent<Column>().GetName();
            outPut += "\t" + tempCol + "\n";
        }
    }

}
