using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Vid_Number : Vid_Object
{
    public VidNum_Type type = VidNum_Type.INT;
    public string data = "0";

    public Vid_Number() : base()
    {
        setUpOutput(data);
    }
    
    // Use this for initialization
    public new void Awake()
    {
        base.Awake();
        setUpOutput(data);
    }

    public override void stringify()
    {
    }

    public override bool addInput(Vid_Data data)
    {
        return false;
    }
    private string vidNumType_String()
    {
        switch (type)
        {
            case VidNum_Type.INT:
                return "int";
            case VidNum_Type.FLOAT:
                return "float";
            case VidNum_Type.DOUBLE:
                return "double";
            case VidNum_Type.LONG:
                return "long";
            default:
                return "int";
        }
    }
    public bool setData(String value)
    {
        switch (type)
        {
            case VidNum_Type.INT:
                try
                {
                    int i = int.Parse(value);
                    output.setData(i.ToString());
                    return true;
                }
                catch (FormatException e) { }
                return false;
            case VidNum_Type.FLOAT:
                try
                {
                    float f = float.Parse(value);
                    output.setData(f.ToString());
                    return true;
                }
                catch (FormatException e) { }
                return false;
            case VidNum_Type.DOUBLE:
                try
                {
                    double d = double.Parse(value);
                    output.setData(d.ToString());
                    return true;
                }
                catch (FormatException e) { }
                return false;
            case VidNum_Type.LONG:
                try
                {
                    long l = long.Parse(value);
                    output.setData(l.ToString());
                    return true;
                }
                catch (FormatException e) { }
                return false;
            default:
                try
                {
                    int i = int.Parse(value);
                    output.setData(i.ToString());
                    return true;
                }
                catch (FormatException e) { }
                return false;
        }
    }

    public void setUpOutput(String data)
    {
        Vid_Data outputData = new Vid_Data(VidData_Type.NUM, this);
        outputData.setData(data);
        Dictionary<string, string> metaData = outputData.getMetaData();
        metaData.Add("number_type", vidNumType_String());
        output = outputData;
    }
}
