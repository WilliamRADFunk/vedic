using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Controller_SpawnNode : MonoBehaviour {

    public Transform parent;
    public List<GameObject> prefabs;

    public void spawnNode(Dropdown d) {
        GameObject go = (GameObject)Instantiate(prefabs[d.value], new Vector3(0, 0, 1), Quaternion.identity);
        go.transform.SetParent(parent);
        go.transform.Rotate(go.transform.rotation.x, 180, go.transform.rotation.z);
        go.SetActive(true);
    }
}
