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
                string textBoxData = reply.Substring(0, reply.IndexOf("##SelectTable##"));
                string podData = reply.Substring(reply.IndexOf("##SelectTable##:{")+17);
                // This Table ID should be unlike original import
                // It should consist of a combo dbname it came from, and select query random unique hash
                DatabaseUtilities.SelectTable sTable = new DatabaseUtilities.SelectTable(podData, "Test123", "FunkSelectTable");
                DatabaseUtilities.Table t = sTable.GetTable();
                podManager.SendPod(t, dbname + "-" + t.GetName());
                Debug.Log(textBoxData);
                Output.text = textBoxData;
            }
            // For non-Select Queries
            else
            {
                Debug.Log(reply);
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
