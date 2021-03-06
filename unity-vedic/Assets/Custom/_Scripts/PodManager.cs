﻿using UnityEngine;
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

        //Invoke("Test", 10f);
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
        checkActivePods();

        for(int i = 0; i < podStates.Length; i++)
        {
            if(podStates[i])
            {
                GameObject assembledHarness = ViewAssembler.GenerateViewObject(obj, true, false, -1);
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

    public void SendPod(DatabaseUtilities.Table tab, string name)
    {
        Database test = new Database();
        test.SetName(name);
        test.tables = new List<DatabaseUtilities.Table>();
        test.AddTable(tab);

        BuildPod(test);

        /*DatabaseUtilities.Table t = new DatabaseUtilities.Table();
        t.SetName("Dean");
        t.SetId("T34");
        t.columns = new List<DatabaseUtilities.Column>();

        DatabaseUtilities.Column c = new DatabaseUtilities.Column();
        c.SetName("Sam");
        c.SetId("12");
        c.SetColor(DatabaseBuilder.GetRandomColor());
        c.fields = new List<string>();
        c.AddField("Dogs");

        DatabaseUtilities.Column c2 = new DatabaseUtilities.Column();
        c2.SetName("Sammy");
        c2.SetId("1234");
        c2.SetColor(DatabaseBuilder.GetRandomColor());
        c2.fields = new List<string>();
        c2.AddField("Cats");

        t.AddColumn(c);
        t.AddColumn(c2);

        test.AddTable(t);

    */
    }
}
