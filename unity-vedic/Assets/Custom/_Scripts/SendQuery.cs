using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

using System;
using System.IO;
using System.Net;
using System.Text;
using System.Collections;

public class SendQuery : MonoBehaviour
{
    public Text Output;
    public Text dbname;
    public Text hostname;
    public Text username;
    public Text password;

    // Called from Send --- Makes it asynchronous
    IEnumerator SendQ(Text input)
    {
        string url = "http://www.williamrobertfunk.com/applications/vedic/actions/query.php";
        WWWForm form = new WWWForm();
        form.AddField("dbname", dbname.text);
        form.AddField("hostname", hostname.text);
        form.AddField("username", username.text);
        form.AddField("password", password.text);
        form.AddField("query", input.text);

        UnityWebRequest www = UnityWebRequest.Post(url, form);
        yield return www.Send();

        if (www.isError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Output.text = www.downloadHandler.text;
        }
    }
    // Use this for initialization
    public void Send(Text input)
    {
        StartCoroutine(SendQ(input));
    }
}
