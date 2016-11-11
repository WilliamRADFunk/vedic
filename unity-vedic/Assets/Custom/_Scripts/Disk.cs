using UnityEngine;
using System.Collections;
using System;
using System.Globalization;

public class Disk : MonoBehaviour, ViewObj
{

    public float triggeredColumnOffset; // Currently 0.5
    public int offsetFrameCount; // Currently 7

    float finalTriggeredHeight;
    float triggerMovementOffset;
    float distance;
    float finalHeight;

    string typeName;
    string ID;
    float colHeight;
    int timer;
    Material objMesh;
    Color instanceColor;

    Vector3 initialLocalPos;
    Vector3 initialLocalScale;
    Quaternion initialLocalRot;

    bool virgin;
    bool triggered;
    bool touched;
    bool changeable;
    bool runnable;

    //External Elements
    private GameObject TactileText;
    private WindowTextController t;
    private DataCache dCache;


    // Use this for initialization
    void Start()
    {

        runnable = false;
        changeable = true;
        triggered = false;
        virgin = true;
        touched = false;
        objMesh = gameObject.GetComponent<Renderer>().material;
        gameObject.layer = 14;
        TactileText = GameObject.FindGameObjectWithTag("DynamicText");
        t = TactileText.GetComponent<WindowTextController>();
        dCache = GameObject.FindGameObjectWithTag("DataCache").GetComponent<DataCache>();
    }

    // Update is called once per frame
    void Update()
    {
        if (virgin)
        {
            InvokeRepeating("CountDown", 0.1f, 0.1f);
            virgin = false;
            objMesh.color = instanceColor;
        }

        if (runnable)
        {
            runnable = false;
            changeable = false;
            StartCoroutine(triggerTransition(triggered));
        }

        if (timer == 0)
        {
            t.UpdateInfo(ID, false);
            touched = false;
        }
    }

    public void Initialize(int key, Transform father, string cName, string identification, string hexColor)
    {
        //OLD FUNCTION

        //Initialize the positional vectors to keep track up for simple y vector movements.
        //colHeight = key;
        //finalTriggeredHeight = colHeight + (triggeredColumnOffset * colHeight);
        //distance = finalTriggeredHeight - key;
        //triggerMovementOffset = distance / offsetFrameCount;
        //finalHeight = colHeight + distance + 0.5f;

        typeName = cName;
        ID = identification;
        instanceColor = HexToColor(hexColor);
        ParentObject(father);
        ResetObjectDefault();
    }

    public void Initialize(DatabaseUtilities.Column diskInfo, string dataString, Transform father, string hexColor)
    {
        //NEW INTIALIZATION FUNCTION

        //Convert string to float
        //float scaleSize;
        float scaleSize = 0;
        gameObject.transform.localScale = new Vector3(0, scaleSize, 0);
        typeName = dataString;
        instanceColor = HexToColor(hexColor);
        ParentObject(father);
    }

    public void ResetObjectDefault()
    {
        gameObject.transform.localPosition = initialLocalPos;
    }

    public void ParentObject(Transform parentTransform)
    {
        gameObject.transform.parent = parentTransform;
    }

    private static Color HexToColor(string hexColor)
    {

        Color color = new Color();

        //Remove # if present
        if (hexColor.IndexOf('#') != -1)
            hexColor = hexColor.Replace("#", "");

        int red = 0;
        int green = 0;
        int blue = 0;

        if (hexColor.Length == 6)
        {
            //#RRGGBB
            red = int.Parse(hexColor.Substring(0, 2), NumberStyles.AllowHexSpecifier);
            green = int.Parse(hexColor.Substring(2, 2), NumberStyles.AllowHexSpecifier);
            blue = int.Parse(hexColor.Substring(4, 2), NumberStyles.AllowHexSpecifier);
        }
        else if (hexColor.Length == 3)
        {
            //#RGB
            red = int.Parse(hexColor[0].ToString() + hexColor[0].ToString(), NumberStyles.AllowHexSpecifier);
            green = int.Parse(hexColor[1].ToString() + hexColor[1].ToString(), NumberStyles.AllowHexSpecifier);
            blue = int.Parse(hexColor[2].ToString() + hexColor[2].ToString(), NumberStyles.AllowHexSpecifier);
        }


        float newR = red / 100.0f;
        float newG = green / 100.0f;
        float newB = blue / 100.0f;

        color.r = newR;
        color.g = newG;
        color.b = newB;

        return color;

    }

    public void columnTriggered(bool state)
    {
        StartCoroutine(triggerTransition(state));
    }

    private IEnumerator triggerHandler()
    {
        float startPoint = gameObject.transform.position.y;
        float endPointTop = colHeight + distance;
        float endPointBottom = colHeight;

        if (triggered)
        {
            triggered = false;
            yield return StartCoroutine(triggerTransition(false));
        }
        else
        {
            triggered = true;
            yield return StartCoroutine(triggerTransition(true));
        }

    }

    private IEnumerator triggerTransition(bool temp)
    {
        Vector3 currentPosition = gameObject.transform.localPosition;

        if (temp)
        {
            while (currentPosition.y < finalHeight)
            {
                currentPosition.y = currentPosition.y + triggerMovementOffset;
                gameObject.transform.localPosition = currentPosition;
                yield return new WaitForSeconds(0.02f);
            }

            currentPosition.y = finalHeight;
            gameObject.transform.localPosition = currentPosition;
        }
        else
        {
            while (currentPosition.y > colHeight + 0.5)
            {
                currentPosition.y = currentPosition.y - triggerMovementOffset;
                gameObject.transform.localPosition = currentPosition;
                yield return new WaitForSeconds(0.01f);
            }

            ResetObjectDefault();
        }
        changeable = true;
        yield break;
    }

    public string GetName()
    {
        return typeName;
    }

    public void OnTriggerStay(Collider other)
    {
        timer = 5;

        if (!touched)
        {
            touched = true;
            t.UpdateInfo(typeName, true);
            dCache.PingCache(ID, 2);
        }
    }

    private void CountDown()
    {
        timer--;
    }

    public void SaveOrigin()
    {
        initialLocalPos = gameObject.transform.localPosition;
        colHeight = gameObject.transform.localPosition.y;

        //Initialize the positional vectors to keep track up for simple y vector movements.
        finalTriggeredHeight = colHeight + (triggeredColumnOffset * colHeight);
        distance = finalTriggeredHeight - colHeight;
        triggerMovementOffset = distance / offsetFrameCount;
        finalHeight = finalTriggeredHeight;   //OLD VERSION :: colHeight + distance + 0.5f; //Could create issues with different scaled pieces
    }
}
