using UnityEngine;
using System.Collections.Generic;

public abstract class NodeButton : MonoBehaviour {

    public ConnectionTool ct;

    public NodeButton() {

    }
    public virtual void Awake() {
        ct = ConnectionTool.getInstance();
    }

    public abstract void buttonPressed();
    public virtual void buttonReleased() { }

}
