using UnityEngine;
using Battlehub.UIControls;

public class Controller_TreeViewSpawner : MonoBehaviour {

    public TreeView treeview;
    public Transform parent;

    public void spawnNode() {
        GameObject go = (GameObject)Instantiate((GameObject)treeview.SelectedItem, new Vector3(0, 0, 1), Quaternion.identity);
        go.transform.SetParent(parent);
        go.transform.Rotate(go.transform.rotation.x, 180, go.transform.rotation.z);
        go.SetActive(true);
    }
}
