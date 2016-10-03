using UnityEngine;
using UnityEngine.UI;

public class Controller_DescToggle : MonoBehaviour {
    public int index;
    public Vid_OrderBy node;

    public void setIndex(Toggle t) {
        if(node.isDesc != null) {
            node.isDesc[index] = t.isOn;
        }
    }
}
