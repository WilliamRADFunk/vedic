﻿using UnityEngine;
using System.Collections.Generic;

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
                Debug.Log("BOB:" + inUse);
                setIsUse(true);
                ct.setOutputButton(this);
            }
        }
        else {
            Debug.Log("nknkn:" + inUse);
            setIsUse(false);
            ct.setOutputButton(null);
        }
    }

    public void setIsUse(bool b)
    {
        this.inUse = b;
    }
}
