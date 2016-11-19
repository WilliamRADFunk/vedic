using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HudUpdater : MonoBehaviour {

    public Text textFrameRate;
    public float timedBuffer;
    public int frameBufferLength;

    float deltaTime = 0.0f;
    int delayedBuffer = 5;

    private string frameRate;



    // Use this for initialization
    void Start() {
        if(timedBuffer < 1.0f)
        {
            timedBuffer = 1f;
        }

        if(frameBufferLength < 3)
        {
            frameBufferLength = 5;
        }
    }

    // Update is called once per frame
    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        DisplayGameInfo();

        if (delayedBuffer == 0)
        {
            delayedBuffer = frameBufferLength;
            //Do delayed updaters
        }
        else
        {
            delayedBuffer--;
        }
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

    IEnumerator secondBuffer()
    {
        while(true)
        {
            //Delayed Dispay stuff here

            yield return new WaitForSeconds(timedBuffer);
        }
    }
}
    