using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Controller_Panel : MonoBehaviour {
    public Transform oldParent;
    public Transform newParent;

    public void moveToParent(Button b) {
        transform.SetParent(newParent);
        transform.position = newParent.position;
        transform.rotation = newParent.rotation;
        gameObject.SetActive(true);
        //else {
        //    transform.SetParent(oldParent);
        //    transform.position = oldParent.position;
        //    transform.rotation = oldParent.rotation;
        //    gameObject.SetActive(b.isOn);
        //}
    }

}
