using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

using System;
using System.Collections;

using DatabaseUtilities;

public class ImportDatabase : MonoBehaviour
{
    public InputField dbname;
    public InputField hostname;
    public InputField username;
    public InputField password;

    private bool pristine = true;

    private string[][] storedDatabases = new string[9][];

    // Called from Update-Pristine --- Makes it asynchronous
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
                storedDatabases[i] = new string[4];
                storedDatabases[i][0] = "";
                storedDatabases[i][1] = "";
                storedDatabases[i][2] = "";
                storedDatabases[i][3] = "";
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
    // Called from Send --- Makes it asynchronous
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
    // Called from SaveDb --- Makes it asynchronous
    IEnumerator SaveDatabaseInfo(int dbNum, string dbname, string hostname, string username, string password)
    {
        string url = "http://www.williamrobertfunk.com/applications/vedic/actions/setDbInfo.php";
        WWWForm form = new WWWForm();
        form.AddField("dbNum", dbNum);
        form.AddField("dbname", dbname);
        form.AddField("hostname", hostname);
        form.AddField("username", username);
        form.AddField("password", password);

        UnityWebRequest www = UnityWebRequest.Post(url, form);
        yield return www.Send();

        if (www.isError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Database Save: " + www.downloadHandler.text);
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
    // Use this to pull database connection info from locally stored cache,
    // and place it in the four panel input fields.
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

            StartCoroutine(SaveDatabaseInfo(dbIndex, dbname.text, hostname.text, username.text, password.text));
        }
    }
    // Use this for importing a single database.
    public void Send()
    {
        StartCoroutine(GetDatabase());
    }
}
