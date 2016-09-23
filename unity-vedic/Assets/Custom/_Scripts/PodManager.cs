using UnityEngine;
using System.Collections;
using DatabaseUtilities;
using System.Collections.Generic;

public class PodManager : MonoBehaviour {

    GameObject[] pods;
    bool[] podStates;
	// Use this for initialization
	void Start () {
        //Pull in initial Pods currently in scene
        GameObject[] tempPods = GameObject.FindGameObjectsWithTag("Pod");
        pods = tempPods;

        podStates = new bool[pods.Length];

        //Invoke("Test", 30f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void DestroyAllPodInstances()
    {
        foreach(GameObject pod in pods)
        {
            pod.GetComponent<Pod>().KillHarness();
        }
    }

    public void DestroyPods(int[] entry)
    {
        if(entry.Length <= 0)
        {
            for(int i = 0; i < entry.Length; i++)
            {
                pods[entry[i]].GetComponent<Pod>().KillHarness();
            }
        }
    }

    public void DestroyPodsAuto()
    {
        checkActivePods();
        for(int i = 0; i < pods.Length; i++)
        {
            if(podStates[i])
            {
                pods[i].GetComponent<Pod>().KillHarness();
            }
        }
    }

    public string RetrievePodName(int entry)
    {
        if(entry < 0 || entry >= pods.Length)
        {
            Debug.Log("Entry number provided is out of bounds from pod containment.");
            return "";
        }
        else
        {
            string name = pods[entry].GetComponent<Pod>().GetDbName();
            return name;
        }
    }

    public void BuildPod(Database obj)
    {
        GameObject assembledHarness = ViewAssembler.GenerateViewObject(obj, true);

        checkActivePods();

        for(int i = 0; i < podStates.Length; i++)
        {
            if(podStates[i])
            {
                pods[i].GetComponent<Pod>().AllocateTableHarness(assembledHarness);
            }
        }
    }

    public void ActivatePod(int entry)
    {
        pods[entry].GetComponent<Pod>().SetState(true);
    }

    public void DeactivatePod(int entry)
    {
        pods[entry].GetComponent<Pod>().SetState(false);
    }

    private void checkActivePods()
    {
        for(int i = 0; i < pods.Length; i++)
        {
            podStates[i] = pods[i].GetComponent<Pod>().GetState();
        }
    }

    private void Test()
    {
        Debug.Log("STARTING TEST...");

        Database test = new Database();

        DatabaseUtilities.Column c = new DatabaseUtilities.Column();
        c.SetName("Sam");
        c.SetId("12");
        c.SetColor("FFFFFF");
        c.fields = new List<string>();
        c.AddField("Dogs");

        DatabaseUtilities.Table t = new DatabaseUtilities.Table();
        t.SetId("34");
        t.SetName("Dean");
        t.columns = new List<DatabaseUtilities.Column>();
        t.AddColumn(c);

        test.tables =  new List<DatabaseUtilities.Table>();
        test.AddTable(t);

        BuildPod(test);

    }
}
