using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

using System;
using System.Collections;
using System.Collections.Generic;

public class SendQuery : MonoBehaviour
{
    private int index = 0;
    public PodManager podManager; 
    public Text Output;
    public Text dbname;
    public Text hostname;
    public Text username;
    public Text password;

    public List<string> pastQueries = new List<string>();

    // Called from Send --- Makes it asynchronous
    IEnumerator SendQ(InputField input)
    {
        string url = "http://www.williamrobertfunk.com/applications/vedic/actions/query.php";
        WWWForm form = new WWWForm();
        form.AddField("dbname", dbname.text);
        form.AddField("hostname", hostname.text);
        form.AddField("username", username.text);
        form.AddField("password", password.text);
        form.AddField("query", input.text);

        pastQueries.Add(input.text);
        index = pastQueries.Count;

        UnityWebRequest www = UnityWebRequest.Post(url, form);
        yield return www.Send();

        if (www.isError)
        {
            Debug.Log(www.error);
        }
        else
        {
            string reply = www.downloadHandler.text;

            // For SELECT queries only
            if (reply.IndexOf("##SelectTable##") > -1 )
            {
                Debug.Log("It was a select");
                string textBoxData = reply.Substring(0, reply.IndexOf("##SelectTable##"));
                string podData = reply.Substring(reply.IndexOf("##SelectTable##:{")+17);
                // This Table ID sould be unlike original import
                // It should consist of a combo db name it came from, and select query random unique hash
                DatabaseUtilities.SelectTable sTable = new DatabaseUtilities.SelectTable(podData, "Test123", "FunkSelectTable");
                DatabaseUtilities.Table t = sTable.GetTable();
                podManager.SendPod(t, dbname + "-" + t.GetName());
                for (int i = 0; i < t.columns.Count; i++)
                {
                    Debug.Log("Name: " + t.columns[i].GetName() + "   ID: " + t.columns[i].GetId() + "   Color: " + t.columns[i].GetColor());
                    for (int j = 0; j < t.columns[i].fields.Count; j++)
                    {
                        Debug.Log("Field" + j + ": " + t.columns[i].fields[j]);
                    }
                }
                Output.text = textBoxData;
            }
            // For non-Select Queries
            else
            {
                Output.text = reply;
            }
        }
    }
    // Use this for initialization
    public void Send(InputField input)
    {
        StartCoroutine(SendQ(input));
    }
    // Moves backward through the pastQueries List
    public void Backward(InputField input)
    {
        if(index > 0 && pastQueries.Count > 0)
        {
            index = index - 1;
            input.text = pastQueries[index];
        }
        else if (index == 0 && pastQueries.Count > 0)
        {
            input.text = pastQueries[index];
        }
    }
    // Moves forward through the pastQueries List
    public void Forward(InputField input)
    {
        if (index < (pastQueries.Count - 1) && pastQueries.Count > 0)
        {
            index++;
            input.text = pastQueries[index];
        }
        else if (index == (pastQueries.Count - 1) && pastQueries.Count > 0)
        {
            input.text = pastQueries[index];
        }
    }
}
