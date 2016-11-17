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
        BuildAnalytic1();
        BuildAnalytic2();
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

    private void BuildAnalytic3()
    {
        string message = cacheHandle.ReadCacheMessage();
        return;
        //Call functions to retrieve column name and table name
        //If it fails to verify, do not build analytic3. Break out of function and return false

        //else, check that AnalyticObject3 is not already null
        //If not null, go ahead and kill the object (We have safely called reset on the system by now
        if (AnalyticObject3 != null)
        {
            GameObject.Destroy(AnalyticObject3);
        }
        
        //Build the object if we got this far with the info proivided by database (Use function to be created)
        Database d = VedicDatabase.SortTablesByColumnQuantity();
        AnalyticObject3 = ViewAssembler.GenerateViewObject(d, false, true, 2);
        AnalyticObject3.transform.SetParent(gameObject.transform);
        AnalyticObject3.transform.localPosition = new Vector3(0, 0, 0);
    }

    private void ResetSystemForNewImport()
    {
        ResetAllAnalytics();

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
            BuildAnalytic3();
            aDepsoit.SetHolding(AnalyticObject3);
            AnalyticObject3.transform.localScale -= new Vector3(0.9f, 0.9f, 0.9f);
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
        AnalyticObject1.transform.SetParent(gameObject.transform);
        AnalyticObject1.transform.localPosition = new Vector3(0, 0, 0);
        AnalyticObject1.transform.localScale = genuineScale;

        AnalyticObject2.transform.SetParent(gameObject.transform);
        AnalyticObject2.transform.localPosition = new Vector3(0, 0, 0);
        AnalyticObject2.transform.localScale = genuineScale;

        //AnalyticObject3.transform.SetParent(gameObject.transform);
        //AnalyticObject3.transform.localPosition = new Vector3(0, 0, 0);
        //AnalyticObject3.transform.localScale = genuineScale;

        aDepsoit.ForceCurrentHoldingNull();
        currentObj = -1;
    }

    private void DestroyAllAnalyticObjects()
    {
        GameObject.Destroy(AnalyticObject1);
        GameObject.Destroy(AnalyticObject2);
        //GameObject.Destroy(AnalyticObject3);
    }
}
