using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Net;
using System.Text;

using Database;

public class ImportDatabase : MonoBehaviour
{
    public Text dbname;
    public Text hostname;
    public Text username;
    public Text password;

    // Use this for initialization
    public void Send()
    {
        MyWebRequest mwr = new MyWebRequest("http://www.williamrobertfunk.com/applications/vedic/actions/import.php", "POST", "dbname=" + dbname.text + "&hostname=" + hostname.text + "&username=" + username.text + "&password=" + password.text);
        string reply = mwr.GetResponse();
        Debug.Log("Received Database");

        VedicDatabase.db = DatabaseBuilder.ConstructDB(dbname.text, reply);
        /* For database import verfification purposes only.
        Debug.Log(reply);
        for(int i = 0; i < VedicDatabase.db.tables.Count; i++)
        {
            Debug.Log(VedicDatabase.db.tables[i].name + "\n");
            for (int j = 0; j < VedicDatabase.db.tables[i].columns.Count; j++)
            {
                Debug.Log("     " + VedicDatabase.db.tables[i].columns[j].name + "\n");
                for (int k = 0; k < VedicDatabase.db.tables[i].columns[j].fields.Count; k++)
                {
                    Debug.Log("          " + VedicDatabase.db.tables[i].columns[j].fields[k] + "\n");
                }
            }
        }
        */
        gameObject.SetActive(false);
    }
    public class MyWebRequest
    {
        private WebRequest request;
        private Stream dataStream;

        private string status;

        public String Status
        {
            get { return status; }
            set { status = value; }
        }

        public MyWebRequest(string url) { request = WebRequest.Create(url); }

        public MyWebRequest(string url, string method)
            : this(url)
        {

            if (method.Equals("GET") || method.Equals("POST")) { request.Method = method; }
            else { throw new Exception("Invalid Method Type"); }
        }

        public MyWebRequest(string url, string method, string data)
            : this(url, method)
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
