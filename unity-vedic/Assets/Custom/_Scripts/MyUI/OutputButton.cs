using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class OutputButton : NodeButton {
    public int outputIndex = 0;
    public Vid_Object vid_obj;
    public bool inUse = false;
    public InputButton inButton;

    public override void buttonPressed()
    {
        if (!inUse)
        {
            if (!ct.currentSeleted())
            {
                setIsUse(true);
                ct.setOutputButton(this);
            }
        }
        else {
            setIsUse(false);
            ct.setOutputButton(null);
        }
    }

    public void setIsUse(bool b)
    {
        this.inUse = b;
        Image i = GetComponent<Image>();
        if (i == null) { return; }
        
        if (b) {
            i.color = Color.yellow;
        }
        else {

            i.color = Color.white;
        }
    }

}
