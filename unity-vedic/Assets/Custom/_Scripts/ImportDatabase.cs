using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

using System;
using System.Collections;


using DatabaseUtilities;
using System.Collections.Generic;

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

        // Get the database data
        if (www.isError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Received Database");

            GetColumnTypes("SELECT DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS", www.downloadHandler.text);

            Debug.Log("This happened now.");
        }
    }
    public void GetOldDatabase()
    {
        if(!VedicDatabase.isDatabaseNull)
        {
            ViewAssembler.GenerateViewObject(VedicDatabase.db, false, false, -1);
        }
    }
    // Use this for getting column variable types
    public void GetColumnTypes(String input, string baseData)
    {
        StartCoroutine(GetColumnTypesQ(input, baseData));
    }
    IEnumerator GetColumnTypesQ(String input, string baseData)
    {
        string url = "http://www.williamrobertfunk.com/applications/vedic/actions/query.php";
        WWWForm form = new WWWForm();
        form.AddField("dbname", dbname.text);
        form.AddField("hostname", hostname.text);
        form.AddField("username", username.text);
        form.AddField("password", password.text);
        form.AddField("query", input);

        UnityWebRequest www = UnityWebRequest.Post(url, form);
        yield return www.Send();

        if (www.isError)
        {
            Debug.Log(www.error);
        }
        else
        {
            VedicDatabase.db = DatabaseBuilder.ConstructDB(dbname.text, baseData);
            VedicDatabase.isDatabaseNull = false;

            string reply = www.downloadHandler.text;
            
            string textBoxData = reply.Substring(0, reply.IndexOf("##SelectTable##"));
            string podData = reply.Substring(reply.IndexOf("##SelectTable##:{") + 17);
            // This Table ID sould be unlike original import
            // It should consist of a combo db name it came from, and select query random unique hash
            SelectTable sTable = new SelectTable(podData, "Test123", "FunkSelectTable");
            DatabaseUtilities.Table t = sTable.GetTable();
            List<string> colTypes = new List<string>();
            for (int i = 0; i < t.columns[0].fields.Count; i++)
            {
                colTypes.Add(t.columns[0].fields[i]);
            }
            int numOfColumns = VedicDatabase.GetNumOfColumns();
            List<string> colTypes2 = new List<string>();
            for (int i = (colTypes.Count - numOfColumns); i < colTypes.Count; i++)
            {
                colTypes2.Add(colTypes[i]);
            }
            int counter = 0;
            for (int j = 0; j < VedicDatabase.db.tables.Count; j++)
            {
                for(int k = 0; k < VedicDatabase.db.tables[j].columns.Count && counter < colTypes2.Count; k++, counter++)
                {
                    VedicDatabase.db.tables[j].columns[k].SetColor(VariableColorTable.GetVariableColor(colTypes2[counter]));
                }
            }
            GameObject.FindGameObjectWithTag("Analytics").GetComponent<AnalyticManager>().BuildAnalytics();

            ViewAssembler.GenerateViewObject(VedicDatabase.db, false, false, -1);
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
