using UnityEngine;
using System;

public class Vid_Number : Vid_Object
{
    public VidNum_Type type = VidNum_Type.INT;
    public string data;

    public override void Awake()
    {
        base.Awake();
        output_dataType = VidData_Type.NUM;
    }

    public override string ToString() {
        return data;
    }

    public void toggleNum_type() {
        switch (type) {
            case VidNum_Type.INT:
                type = VidNum_Type.FLOAT;
                if (!setData(data)) {
                    type = VidNum_Type.INT;
                }
                break;
            case VidNum_Type.FLOAT:
                type = VidNum_Type.DOUBLE;
                if (!setData(data)) {
                    type = VidNum_Type.FLOAT;
                }
                break;
            case VidNum_Type.DOUBLE:
                type = VidNum_Type.LONG;
                if (!setData(data)) {
                    type = VidNum_Type.DOUBLE;
                }
                break;
            case VidNum_Type.LONG:
                type = VidNum_Type.INT;
                if (!setData(data)) {
                    type = VidNum_Type.LONG;
                }
                break;
            default:
                break;
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
                    data = i.ToString();
                    return true;
                }
                catch (FormatException e) { Debug.Log(e.ToString()); }
                return false;
            case VidNum_Type.FLOAT:
                try
                {
                    float f = float.Parse(value);
                    data = f.ToString() +"F";
                    return true;
                }
                catch (FormatException e) { Debug.Log(e.ToString()); }
                return false;
            case VidNum_Type.DOUBLE:
                try
                {
                    double d = double.Parse(value);
                    data = d.ToString() +"D";
                    return true;
                }
                catch (FormatException e) { Debug.Log(e.ToString()); }
                return false;
            case VidNum_Type.LONG:
                try
                {
                    long l = long.Parse(value);
                    data = l.ToString()+"L";
                    return true;
                }
                catch (FormatException e) { Debug.Log(e.ToString()); }
                return false;
            default:
                try
                {
                    int i = int.Parse(value);
                    data = i.ToString();
                    return true;
                }
                catch (FormatException e) { Debug.Log(e.ToString()); }
                return false;
        }
    }

}
