using UnityEngine;
using System.Collections.Generic;

public class Vid_Data {
    private Vid_Object vid_object;
    private VidData_Type vidData_Type = VidData_Type.CLASS;
    private Dictionary<string, string> metaData = new Dictionary<string, string>();
    private string data;

    public Vid_Data(VidData_Type type)
    {
        this.vidData_Type = type;
    }

    public Vid_Data(VidData_Type type, Vid_Object vid_object)
    {
        this.vidData_Type = type;
        this.vid_object = vid_object;
    }

    public VidData_Type getVidData_type() { return vidData_Type; }

    public Vid_Object getVid_object() { return vid_object; }
    public Dictionary<string,string> getMetaData() { return metaData; }
    public string getData() { return data; }

    public void setData(string data) { this.data = data; }
    public void setVid_object(Vid_Object obj) { this.vid_object = obj; }

}
