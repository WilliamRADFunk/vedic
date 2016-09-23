﻿using UnityEngine;
using System.Collections;
using DatabaseUtilities;

public class PodManager : MonoBehaviour {

    GameObject[] pods;
    bool[] podStates;
	// Use this for initialization
	void Start () {
        //Pull in initial Pods currently in scene
        GameObject[] tempPods = GameObject.FindGameObjectsWithTag("Pod");
        pods = tempPods;

        podStates = new bool[pods.Length];
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
}