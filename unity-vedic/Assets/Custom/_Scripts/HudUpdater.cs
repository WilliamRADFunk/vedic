﻿using UnityEngine;
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
    private Text UploadSpeed;
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
        while(true)
        {

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
}
    