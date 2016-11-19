using UnityEngine;
using System.Collections;
using DatabaseUtilities;

public class AnalyticManager : MonoBehaviour {

    public ViewMoveTool aDepsoit;

    private GameObject AnalyticObject1;
    private GameObject AnalyticObject2;
    private GameObject AnalyticObject3;
    private GameObject AnaylticObject4;

    private DataCache cacheHandle;

    [SerializeField]
    HudUpdater h;

    int currentObj;

    private Vector3 genuineScale;

	// Use this for initialization
	void Start () {
        //Invoke("BuildAnalytic2", 2.0f);
        cacheHandle = GameObject.FindGameObjectWithTag("DataCache").GetComponent<DataCache>();
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    public void BuildAnalytics()
    {
        aDepsoit.SetAsAnalytical();
        ResetSystemForNewImport();

        BuildAnalytic1();
        BuildAnalytic2();
        BuildAnalytic3(true);

    }

    private void BuildAnalytic1()
    {
        Database db1 = VedicDatabase.SortTablesByColumnQuantity();
        AnalyticObject1 = ViewAssembler.GenerateViewObject(db1, false, true, 0);
        AnalyticObject1.transform.SetParent(gameObject.transform);
        AnalyticObject1.transform.localPosition = new Vector3(0, 0, 0);
        genuineScale = AnalyticObject1.transform.localScale;
    }

    private void BuildAnalytic2()
    {
        /*
        DatabaseUtilities.Table t = new DatabaseUtilities.Table();
        DatabaseUtilities.Column c = new DatabaseUtilities.Column();
        c.fields = new System.Collections.Generic.List<string>();
        c.AddField("0.5");
        c.SetColor("#000000");
        c.SetName("varchar");
        DatabaseUtilities.Column c2 = new DatabaseUtilities.Column();
        c2.fields = new System.Collections.Generic.List<string>();
        c2.AddField("0.5");
        c2.SetColor("#000055");
        c2.SetName("varchar boii");
        t.columns = new System.Collections.Generic.List<DatabaseUtilities.Column>();
        t.AddColumn(c);
        t.AddColumn(c2);
        DatabaseUtilities.Database d = new DatabaseUtilities.Database();
        d.tables = new System.Collections.Generic.List<DatabaseUtilities.Table>();
        d.AddTable(t);
        */
        Database d = VedicDatabase.GetDataTypeDB();

        AnalyticObject2 = ViewAssembler.GenerateAnalyticObject(d, 1);
        AnalyticObject2.transform.SetParent(gameObject.transform);
        AnalyticObject2.transform.localPosition = new Vector3(0, 0, 0);
    }

    private void BuildAnalytic3(bool initial)
    {
        if(initial)
        {
            //Call the initial build of object
            Database dInitial = VedicDatabase.ChangeColorsForKeys("T0", "T0-C0");
            AnalyticObject3 = ViewAssembler.GenerateViewObject(dInitial, false, true, 2);
            AnalyticObject3.transform.SetParent(gameObject.transform);
            AnalyticObject3.transform.localPosition = new Vector3(0, 0, 0);
            return;
        }

        int cacheType = cacheHandle.ReadPingType();
        if(cacheType == 2)
        {
            string message = cacheHandle.ReadCacheMessage();
            //Build db based off of column id retrieval from cache
            if (AnalyticObject3 != null)
            {
                GameObject.Destroy(AnalyticObject3);
                AnalyticObject3 = null;
            }

            string tableId = message.Substring(0, message.IndexOf('-'));
            //Build the object if we got this far with the info proivided by database (Use function to be created)
            Database d = VedicDatabase.ChangeColorsForKeys(tableId, message);
            AnalyticObject3 = ViewAssembler.GenerateViewObject(d, false, true, 2);
            AnalyticObject3.transform.SetParent(gameObject.transform);
            AnalyticObject3.transform.localPosition = new Vector3(0, 0, 0);
        }
        else
        {
            return;
        }
        
        
        
    }

    private void BuildAnalytic4()
    {
        //Toggle HudUpdater
        h.ToggleHUD();
    }

    private void ResetSystemForNewImport()
    {
        ResetAllAnalytics();
        DestroyAllAnalyticObjects();

    }

    public void ActivateAnalytic(int type)
    {
        ResetAllAnalytics();
        aDepsoit.ResetAnchor();

        if(type == 0)
        {
            currentObj = 0;
            aDepsoit.SetHolding(AnalyticObject1);
            AnalyticObject1.transform.localScale -= new Vector3(0.9f, 0.9f, 0.9f);
            AnalyticObject1.transform.localRotation = new Quaternion(0, 0, 0, 0);
        }
        if(type == 1)
        {
            currentObj = 1;
            aDepsoit.SetHolding(AnalyticObject2);
            AnalyticObject2.transform.localScale -= new Vector3(0.9f, 0.9f, 0.9f);
        }
        if(type == 2)
        {
            currentObj = 2;
            BuildAnalytic3(false);
            aDepsoit.SetHolding(AnalyticObject3);
            AnalyticObject3.transform.localScale -= new Vector3(0.9f, 0.9f, 0.9f);
        }
        if(type == 3)
        {
            BuildAnalytic4();
        }
    }

    public void toggleOnAnalyticRts()
    {
        if(currentObj == -1)
        {
            return;
        }
        else if(currentObj == 0)
        {
            aDepsoit.SetHolding(AnalyticObject1);
        }
        else if(currentObj == 1)
        {
            aDepsoit.SetHolding(AnalyticObject2);
        }
    }

    private void ResetAllAnalytics()
    {
        if(AnalyticObject1 != null)
        {
            AnalyticObject1.transform.SetParent(gameObject.transform);
            AnalyticObject1.transform.localPosition = new Vector3(0, 0, 0);
            AnalyticObject1.transform.localScale = genuineScale;
        }
     
        if(AnalyticObject2 != null)
        {
            AnalyticObject2.transform.SetParent(gameObject.transform);
            AnalyticObject2.transform.localPosition = new Vector3(0, 0, 0);
            AnalyticObject2.transform.localScale = genuineScale;
        }

        
        if(AnalyticObject3 != null)
        {
            AnalyticObject3.transform.SetParent(gameObject.transform);
            AnalyticObject3.transform.localPosition = new Vector3(0, 0, 0);
            AnalyticObject3.transform.localScale = genuineScale;
        }

        aDepsoit.ForceCurrentHoldingNull();
        currentObj = -1;
    }

    private void DestroyAllAnalyticObjects()
    {
        if(AnalyticObject1 != null)
        {
            GameObject.Destroy(AnalyticObject1);
            AnalyticObject1 = null;
        }
        
        if(AnalyticObject2 != null)
        {
            GameObject.Destroy(AnalyticObject2);
            AnalyticObject2 = null;
        }
        
        if(AnalyticObject3 != null)
        {
            GameObject.Destroy(AnalyticObject3);
            AnalyticObject3 = null;
        }
    }
}
