using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WindowTextController : MonoBehaviour {

    public Text Window1;
    public Text Window2;
    public Text Window3;
    public Text Window4;

    void Start()
    {
        SwitchBoard(0);
    }
    //Input from Teleporter signaling to activate one of the windows Text component
    public void SwitchBoard(int stationNumber)
    {
        DeactivateAll();

        if (stationNumber == 0)
            ActivateWindow(Window1);
        else if (stationNumber == 1)
            ActivateWindow(Window2);
        else if (stationNumber == 2)
            ActivateWindow(Window3);
        else if (stationNumber == 3)
            ActivateWindow(Window4);

    }

    //Deactivates visibility of all Text objects on the Main Windows in scene
    private void DeactivateAll()
    {
        Window1.enabled = false;
        Window2.enabled = false;
        Window3.enabled = false;
        Window4.enabled = false;
    }

    //Process of activating a Text object to be viewable in scene
    private void ActivateWindow(Text temp)
    {
        if(temp == null)
        {
            return;
        }
        temp.enabled = true;       
    }



    
}
