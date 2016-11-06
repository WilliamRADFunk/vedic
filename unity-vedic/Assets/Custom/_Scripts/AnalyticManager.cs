using UnityEngine;
using System.Collections;
using DatabaseUtilities;

public class AnalyticManager : MonoBehaviour {

    public ViewMoveTool aDepsoit;

    private GameObject AnalyticObject1;
    private GameObject AnalyticObject2;
    private GameObject AnalyticObject3;
    private GameObject AnaylticObject4;

    private Vector3 genuineScale;

	// Use this for initialization
	void Start () {
	
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

    public void ActivateAnalytic(int type)
    {
        ResetAllAnalytics();
        aDepsoit.ResetAnchor();

        if(type == 0)
        {
            aDepsoit.SetHolding(AnalyticObject1);
            AnalyticObject1.transform.localScale -= new Vector3(0.7f, 0.7f, 0.7f);
        }
    }

    private void ResetAllAnalytics()
    {
        AnalyticObject1.transform.SetParent(gameObject.transform);
        AnalyticObject1.transform.localPosition = new Vector3(0, 0, 0);
        AnalyticObject1.transform.localScale = genuineScale;

        aDepsoit.ForceCurrentHoldingNull();
    }
}
