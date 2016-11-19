using UnityEngine;
using System.Collections;

public class DataCache : MonoBehaviour {

    private GameObject cachedItem;
    private string cachedMessage;
    private string cachedName;
    private int cachedInt;

    public GameObject SignalChange;

    private enum PingType { general, viewTable, viewColumn };
    int cacheParadigm;

	void Awake()
    {
        cachedMessage = "";
        cachedName = "";
        cachedItem = null;
        cachedInt = -1;
        cacheParadigm = 0;
    }

    private void UpdateHandChange()
    {
        if(cacheParadigm == 0)
        {
            SignalChange.GetComponent<Renderer>().material.color = Color.black;
        }
        else if(cacheParadigm == 1)
        {
            SignalChange.GetComponent<Renderer>().material.color = Color.green;
        }
        else if(cacheParadigm == 2)
        {
            SignalChange.GetComponent<Renderer>().material.color = Color.yellow;
        }
        else
        {
            SignalChange.GetComponent<Renderer>().material.color = Color.white;
        }

    }

    void Start()
    {
        //InvokeRepeating("RunCacheAnalysis", 5.0f, 5.0f);
    }


    public void PingCache(GameObject tmp)
    {
        cachedItem = tmp;
    }

    public void PingCache(string tmp, string nameTmp, int type)
    {
        cachedMessage = tmp;
        cachedName = nameTmp;
        cacheParadigm = type;

        UpdateHandChange();
    }

    public void PingCache(int tmp)
    {
        cachedInt = tmp;
    }

    public void RemoveCache(GameObject tmp)
    {
        if (cachedItem != null && tmp != null)
            if (tmp.GetInstanceID() == cachedItem.GetInstanceID())
                tmp = null;
                   
    }

    public void RemoveCache(string tmp)
    {
        if(cachedMessage != null && tmp != null)
            if (cachedMessage == tmp)
                cachedMessage = null;
    }

    public void RemoveCache(int tmp)
    {
        if(cachedInt == tmp)
            cachedInt = -1;
    }

    public GameObject ReadCacheItem()
    {
        return cachedItem;
    }

    public string ReadCacheMessage()
    {
        return cachedMessage;
    }

    public string ReadCachename()
    {
        return cachedName;
    }

    public int ReadCacheInt()
    {
        return cachedInt;
    }

    public int ReadPingType()
    {
        return cacheParadigm;
    }

    public void RunCacheAnalysis()
    {
        Debug.Log("Running Cache Analysis...");
        if(cachedItem != null)
        {
            Debug.Log("Cached Object InstanceID is: " + cachedItem.GetInstanceID());
        }
        else
        {
            Debug.Log("GameObject is not set.");
        }

        if(cachedMessage != null)
        {
            Debug.Log("Cached Message is: " + cachedMessage);
        }
        else
        {
            Debug.Log("Cached Message is not set.");
        }

        if(cachedInt != -1)
        {
            Debug.Log("Cached Int is: " + cachedInt);
        }
        else
        {
            Debug.Log("Cached Int is not set.");
        }
    }
}
