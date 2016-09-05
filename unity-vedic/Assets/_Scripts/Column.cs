using UnityEngine;
using System.Collections;
using System;
using System.Globalization;

public class Column : MonoBehaviour, ViewObj {

    string ID;
    Material objMesh;
    Color instanceColor;
    int colHeight;

    bool virgin;


	// Use this for initialization
	void Start () {

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
	}


    public void Initialize(int key, Transform father, string identification, string hexColor)
    {
        this.colHeight = key;
        ID = identification;

        instanceColor = HexToColor(hexColor);
        //gameObject.GetComponent<Renderer>().material.SetColor("DATCOLOR",instanceColor);
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
}
