using UnityEngine;
using System.Collections;
using System;
using System.Globalization;

public class Column : MonoBehaviour, ViewObj {

    public float triggeredColumnOffset;
    public int offsetFrameCount;

    float finalTriggeredHeight;
    float triggerMovementOffset;

    string ID;
    int colHeight;
    Material objMesh;
    Color instanceColor;

    bool virgin;
    bool triggered;


	// Use this for initialization
	void Start () {

        triggered = false;
        virgin = true;
        objMesh = gameObject.GetComponent<Renderer>().material;


    }
	
	// Update is called once per frame
	void Update () {
	    if(virgin)
        {
            virgin = false;
            objMesh.color = instanceColor;
        }

        if(Input.GetKeyDown("space"))
        {
            
            columnTriggered();
        }
	}

    


    public void Initialize(int key, Transform father, string identification, string hexColor)
    {
        //Initialize the positional vectors to keep track up for simple y vector movements.
        colHeight = key;
        finalTriggeredHeight = colHeight + (triggeredColumnOffset * colHeight);
        float distance = finalTriggeredHeight - key;
        triggerMovementOffset = distance / offsetFrameCount;

        Debug.Log(triggerMovementOffset);
        
        ID = identification;
        instanceColor = HexToColor(hexColor);
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

    void columnTriggered()
    {
        StartCoroutine(triggerHandler());
    }

    private IEnumerator triggerHandler()
    {
        float startPoint = gameObject.transform.position.y;
        float endPointTop = finalTriggeredHeight;
        float endPointBottom = colHeight;

        if(triggered)
        { 
            triggered = false;
            yield return StartCoroutine(triggerTransition(startPoint, endPointBottom));
        }
        else
        {
            triggered = true;
            yield return StartCoroutine(triggerTransition(startPoint, endPointTop));
        }
        
    }

    private IEnumerator triggerTransition(float start, float end)
    {
        Vector3 currentPosition = gameObject.transform.localPosition;


        if(start < end)
        {
            for(int i = 0; i < offsetFrameCount; i++)
            {
                if(!triggered)
                {
                    yield break;
                }
                currentPosition.y = currentPosition.y + triggerMovementOffset;
                gameObject.transform.localPosition = currentPosition;
                yield return new WaitForSeconds(0.02f);
            }
        }
        else
        {
            ResetObjectDefault();

        }
        
    }
}
