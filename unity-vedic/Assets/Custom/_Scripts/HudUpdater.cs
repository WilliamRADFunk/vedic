using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HudUpdater : MonoBehaviour {

    float deltaTime = 0.0f;

    public Text textFrameRate;

    private string frameRate;

    // Use this for initialization
    void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        DisplayGameInfo();
    }

    void DisplayGameInfo()
    {
        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;
        frameRate = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
    }

    void SendFrameOuput()
    {
        textFrameRate.text = "FPS: " + frameRate;
    }
}
