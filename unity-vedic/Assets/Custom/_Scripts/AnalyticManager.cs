using UnityEngine;
using System.Collections;
using DatabaseUtilities;

public class AnalyticManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void BuildAnalytics()
    {
        BuildAnalytic1();
    }

    private void BuildAnalytic1()
    {
        Database db1 = VedicDatabase.SortTablesByColumnQuantity();
        GameObject assembledHarness = ViewAssembler.GenerateViewObject(db1, false, true, 0);
        assembledHarness.transform.SetParent(gameObject.transform);
    }
}
