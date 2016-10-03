using UnityEngine;
using UnityEngine.UI;
using System;

public class Controller_String : MonoBehaviour {
    public Vid_String node;

    public void setStringName(InputField t) {
        node.data = t.text;
    }
}
