using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WindowTextController : MonoBehaviour {

    public Text Window1;
    public Text Window2;
    public Text Window3;
    public Text Window4;

    public Text Error1;
    public Text Error2;
    public Text Error3;
    public Text Error4;

    void Start()
    {
        SwitchBoard(0);
    }
    //Input from Teleporter signaling to activate one of the windows Text component
    public void SwitchBoard(int stationNumber)
    {
        DeactivateAll();

        if (stationNumber == 0)
            ActivateWindow(Window1, Error1);
        else if (stationNumber == 1)
            ActivateWindow(Window2, Error2);
        else if (stationNumber == 2)
            ActivateWindow(Window3, Error3);
        else if (stationNumber == 3)
            ActivateWindow(Window4, Error4);

    }

    //Deactivates visibility of all Text objects on the Main Windows in scene
    private void DeactivateAll()
    {
        Window1.enabled = false;
        Window2.enabled = false;
        Window3.enabled = false;
        Window4.enabled = false;
        Error1.enabled = false;
        Error2.enabled = false;
        Error3.enabled = false;
        Error4.enabled = false;
    }

    public void UpdateTexter(string message, bool active)
    {
        Debug.Log("Made it to UpdateInfo...");

        //if (!active)
        //{
        //    if (Window1.text == message)
        //    {
        //        Window1.text = "";
        //        Window2.text = "";
        //        Window3.text = "";
        //        Window4.text = "";
        //    }
        //}
        //else
        //{
        //    Window1.text = message;
        //    Window2.text = message;
        //    Window3.text = message;
        //    Window4.text = message;
        //}
    }

    //Process of activating a Text object to be viewable in scene
    private void ActivateWindow(Text temp, Text errorTemp)
    {
        if(temp == null || errorTemp == null)
        {
            return;
        }

        temp.enabled = true;
        errorTemp.enabled = true;       
    }



    
}
