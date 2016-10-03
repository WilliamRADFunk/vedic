using UnityEngine;
using System.Collections.Generic;
using Battlehub.UIControls;

public class TreeViewDemo : MonoBehaviour {

    public TreeView TreeView;
	// Use this for initialization
	void Start () {
        if (!TreeView) {
            Debug.LogError("SetTreeView");
            return;
        }
        List<GameObject> data = new List<GameObject>();
        data.Add(gameObject);
        TreeView.Items = data;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
