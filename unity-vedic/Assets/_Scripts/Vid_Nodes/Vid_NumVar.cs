using UnityEngine;
using System.Collections;
using System;

public class Vid_NumVar : Vid_Variable {

    public Vid_Number vid_number;
    public String varName;

    public Vid_NumVar(String name) :base()
    {
        this.varName = name;
        this.vid_number = new Vid_Number();
        vid_number.setUpOutput(varName);
        vid_number.setSequence(null);
    }

    public override void stringify()
    { }

    public String getName() { return varName; }
    public void setName(String name) { this.varName = name; }

    public Vid_Number getVid_number() { return vid_number; }
    public void reSetVid_number(String name)
    {
        this.varName = name;
        this.vid_number = new Vid_Number();
        vid_number.setUpOutput(name);
        vid_number.setSequence(null);
    }

    public Vid_Data getOutput() { return vid_number.getOutput(); }
    
    public Vid_Object getSequence() { return base.getSequence(); }
    
    public void setSequence(Vid_Object sequence) { base.setSequence(sequence); }


    public override bool addInput(Vid_Data data)
    {
        return false;
    }

}
