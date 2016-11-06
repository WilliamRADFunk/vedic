using UnityEngine;
using Battlehub.UIControls;

public class Controller_TreeViewSpawner : MonoBehaviour {

    public TreeView treeview;
    public Transform parent;

    public void spawnNode() {
        GameObject go = (GameObject)Instantiate((GameObject)treeview.SelectedItem, ((GameObject)treeview.SelectedItem).transform.position, Quaternion.identity);
        go.transform.SetParent(parent);
        go.SetActive(true);
    }
}
