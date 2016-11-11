using UnityEngine;
using System.Collections;
using DatabaseUtilities;

public class AnalyticManager : MonoBehaviour {

    public ViewMoveTool aDepsoit;

    private GameObject AnalyticObject1;
    private GameObject AnalyticObject2;
    private GameObject AnalyticObject3;
    private GameObject AnaylticObject4;

    int currentObj;

    private Vector3 genuineScale;

	// Use this for initialization
	void Start () {
        //Invoke("BuildAnalytic2", 2.0f);
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    public void BuildAnalytics()
    {
        aDepsoit.SetAsAnalytical();
        BuildAnalytic1();
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

        AnalyticObject2 = ViewAssembler.GenerateAnalyticObject(d, 1);
        AnalyticObject2.transform.SetParent(gameObject.transform);
        AnalyticObject2.transform.localPosition = new Vector3(0, 0, 0);

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
            //Possible reset needed for object here.
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

        aDepsoit.ForceCurrentHoldingNull();
        currentObj = -1;
    }
}
