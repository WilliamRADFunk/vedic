using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class HudUpdater : MonoBehaviour
{
    // System Analytics
    [SerializeField]
    private Text textFrameRate;
    [SerializeField]
    private Text PingLatency;
    [SerializeField]
    private Text StationLocation;
    // Database Analytics
    [SerializeField]
    private Text numOfTables;
    [SerializeField]
    private Text numOfColumns;
    [SerializeField]
    private Text DataCacheType;
    [SerializeField]
    private Text DataCacheContent;
    [SerializeField]
    private Text numOfDatatypes;
    [SerializeField]
    private Teleporter tele;

    [SerializeField]
    private Canvas gui;


    //System Objects
    [SerializeField]
    private DataCache instD;

    public float timedBuffer;
    public int frameBufferLength;

    float deltaTime = 0.0f;
    int delayedBuffer = 5;
    bool pristine = true;

    private string frameRate;



    // Use this for initialization
    void Start() {
        if(timedBuffer < 1.0f)
        {
            timedBuffer = 1f;
        }

        if(frameBufferLength < 3)
        {
            frameBufferLength = 5;
        }
    }

    // Update is called once per frame
    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        DisplayGameInfo();
        if(pristine)
        {
            StartCoroutine(secondBuffer());
            pristine = false;
        }
        if (delayedBuffer == 0)
        {
            delayedBuffer = frameBufferLength;
            //Do delayed updaters
        }
        else
        {
            delayedBuffer--;
        }
    }

    void DisplayGameInfo()
    {
        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;
        frameRate = string.Format("{0:0.}", fps);
        SendFrameOuput();
    }

    void SendFrameOuput()
    {
        textFrameRate.text = "FPS: " + frameRate;
    }

    IEnumerator secondBuffer()
    {

        Ping p = new Ping("8.8.8.8");
        string pingSpeed = "Ping Latency: Unknown";
        int pingSpeedTime = -1;
        int refreshCounter = 5;

        while (true)
        {
            if (refreshCounter == 0)
            {
                p = new Ping("8.8.8.8");
                refreshCounter = 5;
            }
            else
            {
                refreshCounter--;
            }
            if (p.isDone)
            {
                pingSpeedTime = p.time;
                pingSpeed = "Ping Latency: " + pingSpeedTime + " ms";
                PingLatency.text = "Ping Latency: " + pingSpeedTime + " ms";
            }
            else
            {
                PingLatency.text = pingSpeed;
            }

            int stationNumber = tele.getCurrentStation();
            string stationLoc = stationTypeReturn(stationNumber);
            StationLocation.text = "Station: " + stationLoc;


            //Pull cache information
            int tempCacheType = instD.ReadPingType();
            string cacheType = cacheTypeReturn(tempCacheType);
            string cacheName = instD.ReadCachename();

            DataCacheType.text = "Cached Type: " + cacheType;
            DataCacheContent.text = "Cached Content: " + cacheName;

            //Delayed Dispay stuff here
            if (!DatabaseUtilities.VedicDatabase.isDatabaseNull)
            {
                numOfTables.text = "Tables: " + DatabaseUtilities.VedicDatabase.db.tables.Count;
                numOfColumns.text = "Columns: " + DatabaseUtilities.VedicDatabase.GetNumOfColumns();
                Dictionary<string, int>.KeyCollection keyColl = DatabaseUtilities.VedicDatabase.dataTypeDic.Keys;
                numOfDatatypes.text = "";
                foreach (string s in keyColl)
                {
                    numOfDatatypes.text += s + " columns: " + DatabaseUtilities.VedicDatabase.dataTypeDic[s] + "\n";
                }
            }
            
            yield return new WaitForSeconds(timedBuffer);
        }
    }

    private string cacheTypeReturn(int type)
    {
        string returnTyper = "Other";

        if(type == 1)
        {
            returnTyper = "Table";
        }
        else if(type == 2)
        {
            returnTyper = "Column";
        }

        return returnTyper;
    }

    private string stationTypeReturn(int type)
    {
        string stationId = "Misc";

        if(type == 0)
        {
            stationId = "Lobby";
        }
        else if(type == 1)
        {
            stationId = "Browser";
        }
        else if(type == 2)
        {
            stationId = "Query";
        }
        else if(type == 3)
        {
            stationId = "Analytics";
        }

        return stationId;
    }

    public void ToggleHUD()
    {
        if (gui.enabled == false)
        {
            gui.enabled = true;
        }
        else
        {
            gui.enabled = false;
        }
    }
}
    