using UnityEngine;
using System.Collections;
using System;
using System.Globalization;

public class Column : MonoBehaviour, ViewObj {

    public float triggeredColumnOffset;
    public int offsetFrameCount;

    float finalTriggeredHeight;
    float triggerMovementOffset;
    float distance;
    float finalHeight;

    string ID;
    int colHeight;
    int timer;
    Material objMesh;
    Color instanceColor;

    bool virgin;
    bool triggered;
    bool touched;
    bool changeable;
    bool runnable;

    private GameObject TactileText;
    private WindowTextController t;


    // Use this for initialization
    void Start () {

        runnable = false;
        changeable = true;
        triggered = false;
        virgin = true;
        touched = false;
        objMesh = gameObject.GetComponent<Renderer>().material;
        gameObject.layer = 14;
        TactileText = GameObject.FindGameObjectWithTag("DynamicText");
        t = TactileText.GetComponent<WindowTextController>();

    }
	
	// Update is called once per frame
	void Update () {
	    if(virgin)
        {
            InvokeRepeating("CountDown", 0.1f, 0.1f);
            virgin = false;
            objMesh.color = instanceColor;
        }

        if(runnable)
        {
            runnable = false;
            changeable = false;
            StartCoroutine(triggerTransition(triggered));
        }

        if(timer == 0)
        {
            t.UpdateInfo(ID, false);
            touched = false;
        }
	}

    


    public void Initialize(int key, Transform father, string identification, string hexColor)
    {
        //Initialize the positional vectors to keep track up for simple y vector movements.
        colHeight = key;
        finalTriggeredHeight = colHeight + (triggeredColumnOffset * colHeight);
        distance = finalTriggeredHeight - key;
        triggerMovementOffset = distance / offsetFrameCount;
        finalHeight = colHeight + distance + 0.5f;
 
        ID = identification;
        instanceColor = HexToColor(hexColor);
        ParentObject(father);
        ResetObjectDefault();

    }

    public void ResetObjectDefault()
    {
        gameObject.transform.localPosition = new Vector3(0, colHeight + 0.5f, 0);
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
        /*if(changeable)
        {
            triggered = state;
            runnable = true;
        }
        */

        StartCoroutine(triggerTransition(state));
    }

    private IEnumerator triggerHandler()
    {
        float startPoint = gameObject.transform.position.y;
        float endPointTop = colHeight + distance;
        float endPointBottom = colHeight;

        if(triggered)
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

        if(temp)
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
            while(currentPosition.y > colHeight + 0.5)
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
        return ID;
    }

    public void OnTriggerStay(Collider other)
    {
        timer = 5;

        if (!touched)
        {
            touched = true;
            t.UpdateInfo(ID, true);
        }
    }

    private void CountDown()
    {
        timer--;
    }
}
