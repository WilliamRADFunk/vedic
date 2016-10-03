using UnityEngine;
using System.Collections.Generic;

public class OutputButton : NodeButton {
    public int outputIndex = 0;
    public Vid_Object vid_obj;
    bool inUse = false;

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
    }
}
