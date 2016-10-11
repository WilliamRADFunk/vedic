using UnityEngine;
using UnityEngine.UI;


using System;
using System.IO;
using System.Net;
using System.Text;
using System.Collections;

using DatabaseUtilities;
using UnityEngine.Networking;

class MyBehavior : MonoBehaviour
{
    
}

public class ImportDatabase : MonoBehaviour
{
    public InputField dbname;
    public InputField hostname;
    public InputField username;
    public InputField password;

    private bool pristine = true;

    private string[][] storedDatabases = new string[9][];

    IEnumerator GetAllDatabases()
    {
        string url = "http://www.williamrobertfunk.com/applications/vedic/actions/getDbInfo.php";
        WWWForm form = new WWWForm();
        form.AddField("password", "");

        UnityWebRequest www = UnityWebRequest.Post(url, form);
        yield return www.Send();

        if (www.isError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("www: " + www.downloadHandler.text);

            string[] splitReply = www.downloadHandler.text.Split(',');
            for (int i = 0; i < storedDatabases.Length; i++)
            {
                int replyBase = i * 4;
                if (replyBase >= splitReply.Length - 1)
                {
                    break;
                }
                else
                {
                    storedDatabases[i] = new string[4];
                    storedDatabases[i][0] = splitReply[replyBase];
                    storedDatabases[i][1] = splitReply[replyBase + 1];
                    storedDatabases[i][2] = splitReply[replyBase + 2];
                    storedDatabases[i][3] = splitReply[replyBase + 3];
                }
            }
        }
    }

    IEnumerator GetDatabase()
    {
        string url = "http://www.williamrobertfunk.com/applications/vedic/actions/import.php";
        WWWForm form = new WWWForm();
        form.AddField("dbname", dbname.text);
        form.AddField("hostname", hostname.text);
        form.AddField("username", username.text);
        form.AddField("password", password.text);

        UnityWebRequest www = UnityWebRequest.Post(url, form);
        yield return www.Send();

        if (www.isError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Received Database");

            VedicDatabase.db = DatabaseBuilder.ConstructDB(dbname.text, www.downloadHandler.text);

            ViewAssembler.GenerateViewObject(VedicDatabase.db, false);
        }
    }

    // Populate previously stored databases
    public void Update()
    {
        if (pristine)
        {
            StartCoroutine(GetAllDatabases());
            
            pristine = false;
        }
    }
    public void LoadDB(int dbIndex)
    {
        bool allOff = true;
        GameObject[] dbTogglers = GameObject.FindGameObjectsWithTag("DatabaseToggle");
        for (int i = 0; i < dbTogglers.Length; i++)
        {
            Toggle otherToggle = dbTogglers[i].GetComponent<Toggle>();
            if (otherToggle.isOn)
            {
                allOff = false;
            }
        }
        if ( !allOff )
        {
            dbname.text = storedDatabases[dbIndex][0];
            hostname.text = storedDatabases[dbIndex][1];
            username.text = storedDatabases[dbIndex][2];
            password.text = storedDatabases[dbIndex][3];
        }
        else
        {
            dbname.text = "";
            hostname.text = "";
            username.text = "";
            password.text = "";
        }
    }
    // Save new DB info into this DB slot.
    public void SaveDB()
    {
        int dbIndex = -1;
        GameObject[] dbTogglers = GameObject.FindGameObjectsWithTag("DatabaseToggle");
        for (int i = 0; i < dbTogglers.Length; i++)
        {
            Toggle otherToggle = dbTogglers[i].GetComponent<Toggle>();
            if (otherToggle.isOn)
            {
                dbIndex = Int32.Parse(dbTogglers[i].name.Substring(dbTogglers[i].name.Length - 1, 1)) - 1;
            }
        }
        Debug.Log(dbIndex);
        if (dbIndex > -1)
        {
            storedDatabases[dbIndex][0] = dbname.text;
            storedDatabases[dbIndex][1] = hostname.text;
            storedDatabases[dbIndex][2] = username.text;
            storedDatabases[dbIndex][3] = password.text;

            MyWebRequest mwr = new MyWebRequest("http://www.williamrobertfunk.com/applications/vedic/actions/setDbInfo.php", "POST",
                "dbNum=" + dbIndex + "&dbname=" + storedDatabases[dbIndex][0] + "&hostname=" + storedDatabases[dbIndex][1] + 
                "&username=" + storedDatabases[dbIndex][2] + "&password=" + storedDatabases[dbIndex][3]);
            string reply = mwr.GetResponse();
            Debug.Log(reply);
        }
    }

    // Use this for initialization
    public void Send()
    {
        StartCoroutine(GetDatabase());
    }
    public class MyWebRequest
    {
        //private UnityWebRequest request;
        private WebRequest request;
        private Stream dataStream;

        private string status;

        public String Status
        {
            get { return status; }
            set { status = value; }
        }

        public MyWebRequest(string url) 
        {
            request = WebRequest.Create(url);
            request.Timeout = 5000;
        }

        public MyWebRequest(string url, string method) : this(url)
        {
            if (method.Equals("GET") || method.Equals("POST")) { request.Method = method; }
            else { throw new Exception("Invalid Method Type"); }
        }

        public MyWebRequest(string url, string method, string data) : this(url, method)
        {
            // Create POST data and convert it to a byte array.
            string postData = data;
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            // Set the ContentType property of the WebRequest.
            request.ContentType = "application/x-www-form-urlencoded";

            // Set the ContentLength property of the WebRequest.
            request.ContentLength = byteArray.Length;

            // Get the request stream.
            dataStream = request.GetRequestStream();

            // Write the data to the request stream.
            dataStream.Write(byteArray, 0, byteArray.Length);

            // Close the Stream object.
            dataStream.Close();
        }

        public string GetResponse()
        {
            // Get the original response.
            WebResponse response = request.GetResponse();

            this.Status = ((HttpWebResponse)response).StatusDescription;

            // Get the stream containing all content returned by the requested server.
            dataStream = response.GetResponseStream();

            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);

            // Read the content fully up to the end.
            string responseFromServer = reader.ReadToEnd();

            // Clean up the streams.
            reader.Close();
            dataStream.Close();
            response.Close();

            return responseFromServer;
        }
    }
}
